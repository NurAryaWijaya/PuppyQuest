using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // Ganti "GameScene" dengan nama scene gameplay kamu
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game"); // hanya terlihat di editor
        Application.Quit(); // berfungsi di build, bukan di editor
    }
}
