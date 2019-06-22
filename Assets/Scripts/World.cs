using UnityEngine;
using UnityEngine.SceneManagement;

public class World : MonoBehaviour
{
    public bool isFlipped = false;

    private bool spacePressed;
    private bool resetPressed;


    int currentLevel = 0;

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
    }

    
}
