using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public class World : MonoBehaviour
{
    public bool isFlipped = false;
    public UnityEvent worldFlipped;
    public Dictionary<Vector3Int, Block> worldGrid = new Dictionary<Vector3Int, Block>();

    bool spacePressed;
    bool resetPressed;
    int currentLevel = 0;
    List<Shape> allShapes;


    private void Awake()
    {
        FindAllShapes();
        FillGrid();
    }

    private void FindAllShapes()
    {
        allShapes = FindObjectsOfType<Shape>().ToList();
    }

    private void FillGrid()
    {
        foreach (var block in allShapes.SelectMany(shape => shape.blocks.Select(block => block)))
        {
            worldGrid.Add(Vector3Int.RoundToInt(block.transform.position), block);
        }
    }

    void Update()
    {
        spacePressed = Input.GetKeyDown(KeyCode.Space);
        if (spacePressed)
        {
            FlipWorld();
        }
        resetPressed = Input.GetKeyDown(KeyCode.R);
        if (resetPressed)
        {
            ReloadLevel();
        }
    }

    public bool FindBlockInPosition(Vector3Int position, out Block blockInPosition)
    {   
        return worldGrid.TryGetValue(position, out blockInPosition);
    }

    public void UpdateWorldGrid(List<Vector3Int> oldKeys, List<KeyValuePair<Vector3Int, Block>> entriesToAdd)
    {
        foreach (var key in oldKeys)
        {
            worldGrid.Remove(key);
        }
        foreach (var entry in entriesToAdd)
        {
            worldGrid.Add(entry.Key, entry.Value);
        }
    }

    public void ObjectiveFulfilled()
    {
        bool isLastLevel = currentLevel == SceneManager.sceneCountInBuildSettings - 1;
        if (isLastLevel)
        {
            WinGame();
        }
        else
        {
            currentLevel++;
            SceneManager.LoadSceneAsync(currentLevel);
        }
    }

    public void ReloadLevel()
    {
        foreach (var block in worldGrid)
        {
            Debug.Log(block.ToString());
        }

        //SceneManager.LoadSceneAsync(currentLevel);
    }

    private void WinGame()
    {
        Debug.Log("You've won the game!");
    }

    public void FlipWorld()
    {
        transform.Rotate(new Vector3Int(0, 0, 1), -180, Space.Self);
        isFlipped = !isFlipped;
        worldFlipped.Invoke();
    }


}
