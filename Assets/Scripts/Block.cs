using UnityEngine;

[ExecuteInEditMode]
public class Block : MonoBehaviour
{
    public float topPoint;
    public float bottomPoint;
    public float height;
    public bool isYMovable = false;

    float xPosition;
    float zPosition;
    float offset;

    GridSnapper gridsnapper;
    new Collider collider;

    private void Awake()
    {
        gridsnapper = GetComponent<GridSnapper>();
    }

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
            gridsnapper.yOffset = 0;
        }
        else
        {
            gridsnapper.yOffset = 0.5f;
        }
    }
}
