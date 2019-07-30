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
            Move();
        }

        if ((direction == Directions.Crouch && isCrouched == false) || (direction == Directions.Stand && isCrouched == true))
        {
            Crouch();
        }

        Attach();
    }

    private void Move()
    {
        var newPosition = Vector3Int.RoundToInt(transform.position) + directionVectors[direction];
        var positionOccupied = world.FindBlockInPosition(newPosition, out occupyingBlock);

        if (isAttached && attachedShape != null)
        {
            var isShapePushable = attachedShape.CheckIfPushable(directionVectors[direction]);
            if (isShapePushable && (!positionOccupied || occupyingBlock.parentShape == attachedShape))
            {
                attachedShape.MoveShapeInDirection(directionVectors[direction]);
                transform.position = newPosition;
            }
        }
        else
        {
            transform.LookAt(newPosition);
            facingDirection = directionVectors[direction];
            if (!positionOccupied)
            {
                Block blockUnder;
                var isBlockUnder = world.FindBlockInPosition(newPosition + directionVectors[Directions.Crouch], out blockUnder);


                if (newPosition.y == 0)
                {
                    if (isCrouched)
                    {
                        transform.position = newPosition;
                        return;
                    }
                }

                if ((newPosition + directionVectors[Directions.Crouch]).y == 0)
                {
                    transform.position = newPosition;
                    if (isCrouched)
                    {
                        isCrouched = false;
                    }
                }
                                                          
                if (isBlockUnder)
                {
                    transform.position = newPosition;
                    if (!isCrouched)
                    {
                        isCrouched = true;
                    }
                }
                else
                {
                    Block blockUnderBlock;
                    var isBlockUnderBlock = world.FindBlockInPosition(newPosition + directionVectors[Directions.Crouch] + directionVectors[Directions.Crouch], out blockUnderBlock);

                    if (isBlockUnderBlock)
                    {
                        if (isCrouched)
                        {
                            transform.position = newPosition;
                            isCrouched = false;
                        }
                    }
                    else
                    {
                        //do nothing coz przepaść
                    }
                }


                // sprawdz czy pod pozycja jest blok lub poziom zero
                // jesli jest to przejdz
                // jesli nie ma, to sprawdz czy pod nim jest blok
                // jesli jest to jesli jestes kroucz to przejdz i przestan byc kroucz
                // jesli nie ma to nic nie rob
            }
        }
    }

    private void Crouch()
    {
        if (isAttached && attachedShape != null)
        {
            var isShapePushable = attachedShape.CheckIfPushable(directionVectors[direction]);
            if (isShapePushable) // TODO implement collision for Player!!
            {
                attachedShape.MoveShapeInDirection(directionVectors[direction]);
                transform.position += directionVectors[direction];
                foreach (Transform children in transform) //TODO change into animation
                {
                    if (children.GetComponent<Leg>() != null)
                    {
                        children.position -= directionVectors[direction]; ;
                    }
                }
                isCrouched = !isCrouched;
            }
        }
        else
        {
            transform.position += directionVectors[direction];
            foreach (Transform children in transform) //TODO change into animation
            {
                if (children.GetComponent<Leg>() != null)
                {
                    children.position -= directionVectors[direction]; ;
                }
            }
            isCrouched = !isCrouched;
        }
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
