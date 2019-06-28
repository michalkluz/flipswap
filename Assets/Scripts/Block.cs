using UnityEngine;

[ExecuteInEditMode]
public class Block : MonoBehaviour
{
    public int topPoint;
    public int bottomPoint;
    public int height;
    public bool isYMovable = false;
    public GridSnapper gridSnapper;
    public Shape parentShape;

    int xPosition;
    int zPosition;
    float offset;
    World world;
    new BoxCollider collider;

    private void Awake()
    {
        world = FindObjectOfType<World>();
        gridSnapper = GetComponent<GridSnapper>();
        parentShape = GetComponentInParent<Shape>();
        collider = GetComponentInChildren<BoxCollider>();
    }

    public bool IsSnappingToGrid
    {
        get { return gridSnapper.shouldSnapToGrid; }
        set { gridSnapper.shouldSnapToGrid = !gridSnapper.shouldSnapToGrid; }
    }

    public bool CheckIfPushable(Vector3Int pushDirection)
    {
        var potentialPosition = new Vector3Int((int)transform.position.x + pushDirection.x, (int)transform.position.y + pushDirection.y, (int)transform.position.z + pushDirection.z);

        Block occupyingBlock;
        var isBlockFound = world.FindBlockInPosition(potentialPosition, out occupyingBlock);
        if (isBlockFound)
        {
            if (occupyingBlock.parentShape.name == parentShape.name)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;

        }
    }

    public bool LowerBlock()
    {
        var result = false;
        if (topPoint > 0.5f && isYMovable)
        {
            transform.position = new Vector3Int((int)transform.position.x, (int)transform.position.y - 1, (int)transform.position.z);
            result = true;
        }
        return result;
    }

    public bool RaiseBlock()
    {
        var result = false;
        if (bottomPoint < 0f && isYMovable)
        {
            transform.position = new Vector3Int((int)transform.position.x, (int)transform.position.y + 1, (int)transform.position.z);
            result = true;
        }
        return result;
    }

    private void Update()
    {
        UpdateFields();
    }

    private void UpdateFields()
    {
        xPosition = (int)transform.position.x;
        zPosition = (int)transform.position.z;
        gameObject.name = "Block " + $"({xPosition}, {zPosition})";

        bottomPoint = (int)collider.bounds.min.y;
        topPoint = (int)collider.bounds.max.y;
    }
}
