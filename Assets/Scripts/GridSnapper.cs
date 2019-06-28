using UnityEngine;

[ExecuteInEditMode]
public class GridSnapper : MonoBehaviour
{
    public bool shouldSnapToGrid = true;
    public float yOffset = 0.5f;

    void Update()
    {
        if (shouldSnapToGrid)
        {
            SnapToGrid();
        }
    }

    private void SnapToGrid()
    {
        this.gameObject.transform.position = new Vector3(
            Mathf.RoundToInt(transform.position.x - 0.1f),
            Mathf.RoundToInt(transform.position.y - 0.1f) + yOffset,
            Mathf.RoundToInt(transform.position.z - 0.1f)
            );
    }
}
