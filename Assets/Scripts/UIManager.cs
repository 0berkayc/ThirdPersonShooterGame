using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMananger : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("scene");
    }

    public void ExitGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
   
}
