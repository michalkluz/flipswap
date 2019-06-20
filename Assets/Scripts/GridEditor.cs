using UnityEngine;

[ExecuteInEditMode]
public class GridEditor : MonoBehaviour
{
    int gridSize = 1;

    private void Start()
    {
        if (Application.isPlaying)
        {
            this.enabled = false;
        }
    }

    void Update()
    {
        SnapToGrid();
    }

    private void SnapToGrid()
    {
        this.gameObject.transform.position = new Vector3(
            Mathf.RoundToInt(transform.position.x - 0.1f / gridSize) + 0.5f,
            Mathf.RoundToInt(transform.position.y / gridSize),
            Mathf.RoundToInt(transform.position.z - 0.1f / gridSize) + 0.5f
            );
    }
}
