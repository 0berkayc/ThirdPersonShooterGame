using UnityEngine;
using UnityEngine.SceneManagement;

public class winScreen : MonoBehaviour
{
    public string gameSceneName = "scene";
    public void replayGame()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
