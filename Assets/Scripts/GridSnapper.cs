using UnityEngine;

[ExecuteInEditMode]
public class GridSnapper : MonoBehaviour
{
    public bool shouldSnapToGrid = true;

    void Update()
    {
        if (shouldSnapToGrid)
        {
            SnapToGrid();
        }
    }

    private void SnapToGrid()
    {
        this.gameObject.transform.position = new Vector3Int(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.z)
            );
    }
}
