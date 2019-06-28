using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Block currentBlock;

    bool goForward;
    bool goBackward;
    bool goLeft;
    bool goRight;
    bool pushUp;
    bool pushDown;
    bool isWorldFlipped = false;
    World world;

    private void Awake()
    {
        world = FindObjectOfType<World>();
    }

    void Update()
    {
        GatherInput();
        Move();
        LowerOrRaiseBlock();
    }

    private void GatherInput()
    {
        goForward = Input.GetKeyDown(KeyCode.UpArrow);
        goBackward = Input.GetKeyDown(KeyCode.DownArrow);
        goLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        goRight = Input.GetKeyDown(KeyCode.RightArrow);
        pushUp = Input.GetKeyDown(KeyCode.A);
        pushDown = Input.GetKeyDown(KeyCode.Z);
    }

    private void Move()
    {
        if (!(goForward || goBackward || goLeft || goRight)) return;
        var newPosition = new Vector3Int();
        if (goForward)
        {
            newPosition = new Vector3Int(
                (int)transform.position.x,
                (int)transform.position.y,
                (int)transform.position.z + 1);
        }

        else if (goBackward)
        {
            newPosition = new Vector3Int(
                (int)transform.position.x,
                (int)transform.position.y,
                (int)transform.position.z - 1);
        }

        else if (goRight)
        {
            newPosition = new Vector3Int(
                (int)transform.position.x + 1,
                (int)transform.position.y,
                (int)transform.position.z);
        }

        else if (goLeft)
        {
            newPosition = new Vector3Int(
                (int)transform.position.x - 1,
                (int)transform.position.y,
                (int)transform.position.z);
        }

        Block occupyingBlock;
        var isBlockFound = world.FindBlockInPosition(newPosition, out occupyingBlock);

        if (isBlockFound)
        {
            if (IsMovementLegal(occupyingBlock))
            {
                transform.position = new Vector3Int(
                    newPosition.x,
                    occupyingBlock.topPoint,
                    newPosition.z);
                currentBlock = occupyingBlock;
            }
            else
            {
                var occupyingShape = occupyingBlock.parentShape;
                var pushDirection = Vector3Int.RoundToInt(occupyingBlock.transform.position - transform.position);
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
            transform.position = new Vector3Int(newPosition.x, 0, newPosition.z);
            currentBlock = null;

        }
    }

    private bool IsMovementLegal(Block occupyingBlock)
    {
        return Math.Abs(transform.position.y - occupyingBlock.topPoint) <= 0; // 0 for same-y-position travel
    }

    private void LowerOrRaiseBlock()
    {
        if (pushDown && currentBlock != null)
        {
            var blockYMoved = currentBlock.LowerBlock();
            if (blockYMoved)
            {
                transform.position = new Vector3Int((int)transform.position.x, (int)transform.position.y - 1, (int)transform.position.z);
            }
        }

        if (pushUp && currentBlock != null)
        {
            var blockYMoved = currentBlock.RaiseBlock();
            if (blockYMoved)
            {
                transform.position = new Vector3Int((int)transform.position.x, (int)transform.position.y + 1, (int)transform.position.z);
            }
        }
    }

    public bool IsWorldFlipped
    {
        get { return isWorldFlipped; }
        set { isWorldFlipped = !isWorldFlipped; }
    }


}
