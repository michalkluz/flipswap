using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Block currentBlock;

    private bool goForward;
    private bool goBackward;
    private bool turnLeft;
    private bool turnRight;
    private bool pushUp;
    private bool pushDown;
    private Blocks allBlocks;
    private bool isWorldFlipped = false;

    private void Awake()
    {
        allBlocks = FindObjectOfType<Blocks>();
    }

    void Update()
    {
        GatherInput();
        Move();
        Rotate();
        LowerOrRaiseBlock();
    }
    private void Move()
    {
        if (!(goForward || goBackward)) return;
        var newPosition = new Vector3();
        if (goForward)
        {
            newPosition = transform.position + transform.forward;
        }

        else if (goBackward)
        {
            newPosition = transform.position - transform.forward;
        }

        var occupyingBlock = allBlocks.GetOccupyingBlock(newPosition);
        if (occupyingBlock == null)
        {
            transform.position = new Vector3(newPosition.x, 0.5f, newPosition.z);
            currentBlock = null;
        }
        else
        {
            if (IsMovementLegal(occupyingBlock))
            {
                transform.position = new Vector3(
                    newPosition.x,
                    occupyingBlock.topPoint,
                    newPosition.z);
                currentBlock = occupyingBlock;
            }
            else
            {
                //trytopushblock
                var blockPushed = occupyingBlock.PushBlock(transform.position);
                if (blockPushed)
                {
                    transform.position = newPosition;
                }
                else
                {
                    //donothing
                }

            }
        }
    }

    private bool IsMovementLegal(Block occupyingBlock)
    {
        return Math.Abs(transform.position.y - occupyingBlock.topPoint) <= 0; // 0 for same-y-position travel
    }

    private void Rotate()
    {
        if (turnLeft && !isWorldFlipped || (turnRight && isWorldFlipped))
        {
            transform.Rotate(0, -90, 0);
        }

        if (turnRight && !isWorldFlipped || (turnLeft && isWorldFlipped))
        {
            transform.Rotate(0, 90, 0);
        }
    }

    private void LowerOrRaiseBlock()
    {
        if (pushDown && currentBlock != null)
        {
            var blockYMoved = currentBlock.LowerBlock();
            if (blockYMoved)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            }
        }

        if (pushUp && currentBlock != null)
        {
            var blockYMoved = currentBlock.RaiseBlock();
            if (blockYMoved)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            }
        }
    }

    public bool IsWorldFlipped
    {
        get { return isWorldFlipped; }
        set { isWorldFlipped = !isWorldFlipped; }
    }

    private void GatherInput()
    {
        goForward = Input.GetKeyDown(KeyCode.UpArrow);
        goBackward = Input.GetKeyDown(KeyCode.DownArrow);
        turnLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        turnRight = Input.GetKeyDown(KeyCode.RightArrow);
        pushUp = Input.GetKeyDown(KeyCode.A);
        pushDown = Input.GetKeyDown(KeyCode.Z);
    }


}
