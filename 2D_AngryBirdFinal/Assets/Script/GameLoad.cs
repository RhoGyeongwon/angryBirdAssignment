
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviour
{
    public void LoadGame()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (currentSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            currentSceneIndex = 0;
        }
        SceneManager.LoadScene(currentSceneIndex);
    }
}

