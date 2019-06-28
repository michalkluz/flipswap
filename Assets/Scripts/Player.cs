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
    private bool isWorldFlipped = false;
    World world;

    private void Awake()
    {
        world = FindObjectOfType<World>();
    }

    void Update()
    {
        GatherInput();
        Move();
        Rotate();
        LowerOrRaiseBlock();
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

        Block occupyingBlock;
        var isBlockFound = world.FindBlockInPosition(newPosition, out occupyingBlock);


        if (isBlockFound)
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
                var occupyingShape = occupyingBlock.parentShape;
                var pushDirection = occupyingBlock.transform.position - transform.position;
                var shapePushed = occupyingShape.PushShape(pushDirection);
                if (shapePushed)
                {
                    transform.position = newPosition;
                }
                else
                {
                    //donothing
                }
            }
        }
        else
        {
            transform.position = new Vector3(newPosition.x, 0.5f, newPosition.z);
            currentBlock = null;
           
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




}
