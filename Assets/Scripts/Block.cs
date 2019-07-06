using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class Block : MonoBehaviour
{
    public int topPoint;
    public int bottomPoint;
    public GridSnapper gridSnapper;
    public Shape parentShape;

    int xPosition;
    int yPosition;
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

    private void Update()
    {
        UpdateFields();
    }

    private void UpdateFields()
    {
        xPosition = (int)transform.position.x;
        yPosition = (int)transform.position.y;
        zPosition = (int)transform.position.z;
        gameObject.name = "Block " + $"({xPosition}, {yPosition}, {zPosition})";

        bottomPoint = (int)collider.bounds.min.y;
        topPoint = (int)collider.bounds.max.y;
    }
}
