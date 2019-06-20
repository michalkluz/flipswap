using UnityEngine;

[ExecuteInEditMode]
public class Block : MonoBehaviour
{
    public float capPosition;

    float xPosition;
    float yPosition;
    float zPosition;

    float height = 1;

    private void Update()
    {
        UpdatePosition();
        RenameBlock();
    }

    private void UpdatePosition()
    {
        xPosition = transform.position.x - 0.5f;
        yPosition = transform.position.y;
        capPosition = yPosition + height;
        zPosition = transform.position.z - 0.5f;
    }

    private void RenameBlock()
    {
        gameObject.name = "Block " + $"({xPosition}, {zPosition})";
    }
}
