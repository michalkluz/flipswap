using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    bool goForward;
    bool goBackward;
    bool goLeft;
    bool goRight;
    bool goUp;
    bool goDown;
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
    }

    private void GatherInput()
    {
        goForward = Input.GetKeyDown(KeyCode.UpArrow);
        goBackward = Input.GetKeyDown(KeyCode.DownArrow);
        goLeft = Input.GetKeyDown(KeyCode.LeftArrow);
        goRight = Input.GetKeyDown(KeyCode.RightArrow);
        goUp = Input.GetKeyDown(KeyCode.A);
        goDown = Input.GetKeyDown(KeyCode.Z);
    }

    private void Move()
    {
        if (!(goForward || goBackward || goLeft || goRight || goUp || goDown)) return;
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

        else if (goUp)
        {
            newPosition = new Vector3Int(
                (int)transform.position.x,
                (int)transform.position.y + 1,
                (int)transform.position.z);
        }
        else if (goDown)
        {
            newPosition = new Vector3Int(
                (int)transform.position.x,
                (int)transform.position.y - 1,
                (int)transform.position.z);
        }

        Block occupyingBlock;
        var isBlockFound = world.FindBlockInPosition(newPosition, out occupyingBlock);

        if (isBlockFound)
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
                //donothing because can't push
            }

        }
        else
        {
            transform.position = newPosition;
        }
    }

    public bool IsWorldFlipped
    {
        get { return isWorldFlipped; }
        set { isWorldFlipped = !isWorldFlipped; }
    }


}
