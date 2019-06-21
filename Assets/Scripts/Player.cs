using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool goUp;
    private bool goDown;
    private bool goLeft;
    private bool goRight;
    private bool pushDown;
    private Blocks allBlocks;
    public Block currentBlock;

    private void Awake()
    {
        allBlocks = FindObjectOfType<Blocks>();
    }
    void Update()
    {
        GatherInput();
        if (goUp || goDown || goLeft || goRight)
        {
            Move();
        }

        if (pushDown && currentBlock != null)
        {
            var blockLowered = currentBlock.LowerBlock();
            if (blockLowered)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            }
        }
    }

    private void GatherInput()
    {
        goUp = Input.GetKeyDown(KeyCode.UpArrow);
        goDown = Input.GetKeyDown(KeyCode.DownArrow);
        goLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        goRight = Input.GetKeyDown(KeyCode.RightArrow);
        pushDown = Input.GetKeyDown(KeyCode.Z);
    }

    private void Move()
    {
        var newPosition = new Vector3();
        if (goUp)
        {
            newPosition = new Vector3(
                transform.position.x,
                0,
                transform.position.z + 1f);
        }

        else if (goDown)
        {
            newPosition = new Vector3(
                transform.position.x,
                0,
                transform.position.z - 1f);
        }
        else if (goLeft)
        {
            newPosition = new Vector3(
                transform.position.x - 1f,
                0,
                transform.position.z);
        }
        else if (goRight)
        {
            newPosition = new Vector3(
                transform.position.x + 1f,
                0,
                transform.position.z);
        }

        var occupyingBlock = allBlocks.GetOccupyingBlock(newPosition);
        if (occupyingBlock == null)
        {
            transform.position = newPosition;
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
                //donothing
            }
        }
    }

    private bool IsMovementLegal(Block occupyingBlock)
    {
        return Math.Abs(transform.position.y - occupyingBlock.topPoint) <= 1;
    }
}
