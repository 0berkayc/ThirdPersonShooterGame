using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RescueTrigger : MonoBehaviour
{
    private bool playerNearby = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            Debug.Log("Player kafesin önünde, E'ye basabilirsin.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
        }
    }

    void Update()
    {
        if (playerNearby && Keyboard.current.eKey.wasPressedThisFrame)
        {
            GoToVictoryScene();
        }
    }

    void GoToVictoryScene()
    {
        Debug.Log("Kurtarma tamamlandı! VictoryScreen sahnesine geçiliyor...");
        SceneManager.LoadScene("VictoryScreen");
    }
}
