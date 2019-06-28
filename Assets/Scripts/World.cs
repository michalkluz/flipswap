using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Linq;

public class World : MonoBehaviour
{
    public bool isFlipped = false;
    public UnityEvent worldFlipped;
    public Dictionary<Vector3, Block> worldGrid = new Dictionary<Vector3, Block>();

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
            worldGrid.Add(block.transform.position, block);
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

    public bool FindBlockInPosition(Vector3 position, out Block blockInPosition)
    {
        return worldGrid.TryGetValue(position, out blockInPosition);
    }

    public void UpdateWorldGrid(List<Vector3> oldKeys, List<KeyValuePair<Vector3, Block>> entriesToAdd)
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
        SceneManager.LoadSceneAsync(currentLevel);
    }

    private void WinGame()
    {
        Debug.Log("You've won the game!");
    }

    public void FlipWorld()
    {
        transform.Rotate(new Vector3(0, 0, 1), -180, Space.Self);
        isFlipped = !isFlipped;
        worldFlipped.Invoke();
    }


}
