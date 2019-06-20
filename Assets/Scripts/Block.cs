using UnityEngine;

[ExecuteInEditMode]
public class Block : MonoBehaviour
{
    public float topPoint;
    public float bottomPoint;
    public float height;

    float xPosition;
    float zPosition;
    float offset;

    Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    private void Update()
    {
        UpdateFields();
        RenameBlock();
        SnapToGrid();
    }

    private void UpdateFields()
    {
        xPosition = transform.position.x - 0.5f;
        zPosition = transform.position.z - 0.5f;
        bottomPoint = collider.bounds.min.y + 0.5f;
        topPoint = collider.bounds.max.y + 0.5f;
        height = transform.localScale.y;
    }

    private void RenameBlock()
    {
        gameObject.name = "Block " + $"({xPosition}, {zPosition})";
    }

    private void SnapToGrid()
    {
        if (height % 2 == 0)
        {
            offset = 0.5f;
        }
        else
        {
            offset = 0;
        }

        this.gameObject.transform.position = new Vector3(
            Mathf.RoundToInt(transform.position.x - 0.1f) + 0.5f,
            Mathf.RoundToInt(transform.position.y - 0.1f) + offset,
            Mathf.RoundToInt(transform.position.z - 0.1f) + 0.5f
            );
    }
}
