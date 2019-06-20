using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool goUp;
    private bool goDown;
    private bool goLeft;
    private bool goRight;
    private Blocks allBlocks;

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
    }

    private void GatherInput()
    {
        goUp = Input.GetKeyDown(KeyCode.UpArrow);
        goDown = Input.GetKeyDown(KeyCode.DownArrow);
        goLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        goRight = Input.GetKeyDown(KeyCode.RightArrow);
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
        }
        else
        {
            if (IsMovementLegal(occupyingBlock))
            {
                transform.position = new Vector3(
                    newPosition.x,
                    occupyingBlock.capPosition,
                    newPosition.z);
            }
            else
            {
                //
            }
        }
    }

    private bool IsMovementLegal(Block occupyingBlock)
    {
        return Math.Abs(transform.position.y - occupyingBlock.capPosition) <= 1;
    }
}
