using UnityEngine;

[ExecuteInEditMode]
public class GridSnapper : MonoBehaviour
{
    [SerializeField] bool shouldSnapToGrid = true;
    [SerializeField] float yOffset = 0;

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
            Mathf.RoundToInt(transform.position.x - 0.1f) + 0.5f,
            Mathf.RoundToInt(transform.position.y - 0.1f) + yOffset,
            Mathf.RoundToInt(transform.position.z - 0.1f) + 0.5f
            );
    }
}
