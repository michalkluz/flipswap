using UnityEngine;
using static Assets.Helpers.DirectionsHelper;

public class Player : MonoBehaviour
{
    Shape attachedShape;
    Directions direction;
    bool attach;
    bool isCrouched = false;
    bool isAttached = false;
    bool isWorldFlipped = false;
    World world;
    Block occupyingBlock;
    Vector3Int facingDirection;

    private void Awake()
    {
        world = FindObjectOfType<World>();
    }

    void Update()
    {
        GatherInput();

        if ((direction & (Directions.Left | Directions.Right | Directions.Up | Directions.Down)) > 0)
        {
            var newPosition = Vector3Int.RoundToInt(transform.position) + directionVectors[direction];
            if (isAttached && attachedShape != null)
            {
                var shapePushed = attachedShape.PushShape(directionVectors[direction]);
                if (shapePushed) // TODO implement collision for Player!!
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
                transform.LookAt(newPosition);
                facingDirection = directionVectors[direction];
                if (!world.FindBlockInPosition(newPosition, out occupyingBlock))
                {
                    transform.position = newPosition;
                }
            }
        }

        if ((direction == Directions.Crouch && isCrouched == false) || (direction == Directions.Stand && isCrouched == true))
        { 
            if (isAttached && attachedShape != null)
            {

            }
            else
            {
                transform.position += directionVectors[direction];
                isCrouched = !isCrouched;
            }
        }

        Attach();
    }


    private void Attach()
    {
        if (attach)
        {
            if (world.FindBlockInPosition(Vector3Int.RoundToInt(transform.position) + facingDirection, out occupyingBlock))
            {
                attachedShape = occupyingBlock.parentShape;
                isAttached = true;
            }
        }
        else
        {
            attachedShape = null;
            isAttached = true;
        }
    }

    private void GatherInput()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction = Directions.Right;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction = Directions.Left;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            direction = Directions.Stand;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            direction = Directions.Crouch;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction = Directions.Up;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction = Directions.Down;
        }
        else
        {
            direction = Directions.None;
        }
        attach = Input.GetKey(KeyCode.E);
    }

    public bool IsWorldFlipped
    {
        get { return isWorldFlipped; }
        set { isWorldFlipped = !isWorldFlipped; }
    }
}
