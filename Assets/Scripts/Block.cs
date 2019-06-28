using UnityEngine;

[ExecuteInEditMode]
public class Block : MonoBehaviour
{
    public float topPoint;
    public float bottomPoint;
    public float height;
    public bool isYMovable = false;
    public GridSnapper gridSnapper;
    public Shape parentShape;

    float xPosition;
    float zPosition;
    float offset;
    World world;
    new Collider collider;

    private void Awake()
    {
        world = FindObjectOfType<World>();
        gridSnapper = GetComponent<GridSnapper>();
        parentShape = GetComponentInParent<Shape>();
    }

    
    public bool IsSnappingToGrid
    {
        get { return gridSnapper.shouldSnapToGrid; }
        set { gridSnapper.shouldSnapToGrid = !gridSnapper.shouldSnapToGrid; }
    }

    public bool CheckIfPushable(Vector3 pushDirection)
    {
        var potentialPosition = new Vector3(transform.position.x - pushDirection.x, transform.position.y - pushDirection.y, transform.position.z - pushDirection.z);

        Block occupyingBlock;
        var isBlockFound = world.FindBlockInPosition(potentialPosition, out occupyingBlock);

        if (isBlockFound)
        {
            if (occupyingBlock.parentShape == parentShape)
            {
                Debug.Log("Same Shape!");
                return true;
            }
            else
            {
                Debug.Log("Other Shape!");
                return false;
            }
        }
        else
        {
            Debug.Log("No occupying block");
            return true;

        }
    }

    //public void MoveBlock(Vector3 direction)
    //{
    //    var newPosition = transform.position + direction;
    //    world.UpdateWorldGrid(transform.position, newPosition, this);
    //    transform.position = newPosition;
    //}


    public bool LowerBlock()
    {
        var result = false;
        if (topPoint > 0.5f && isYMovable)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - 1, transform.position.z);
            result = true;
        }
        return result;
    }
    
    public bool RaiseBlock()
    {
        var result = false;
        if (bottomPoint < 0f && isYMovable)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
            result = true;
        }
        return result;
    }
        
    private void Start()
    {
        collider = GetComponent<Collider>();
    }

    private void Update()
    {
        UpdateFields();
    }

    private void UpdateFields()
    {
        xPosition = transform.position.x;
        zPosition = transform.position.z;
        gameObject.name = "Block " + $"({xPosition}, {zPosition})";

        bottomPoint = collider.bounds.min.y + 0.5f;
        topPoint = collider.bounds.max.y + 0.5f;

        height = transform.localScale.y;
        if (height % 2 == 0)
        {
            gridSnapper.yOffset = 0;
        }
        else
        {
            gridSnapper.yOffset = 0.5f;
        }
    }
}
