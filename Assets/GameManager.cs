using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public GameObject finishPanel;
    public GameObject controllerPanel;
    [SerializeField] private TMP_Text diedText1;
    [SerializeField] private TMP_Text diedText2;
    [SerializeField] private Button finishToMainMenu; // pastikan ini UnityEngine.UI.Button

    private int diedCount;

    private void Start()
    {
        // Ambil data dari PlayerPrefs
        diedCount = PlayerPrefs.GetInt("DiedCount", 0);
        diedText2.text = $"{diedCount}";

        // Panel nonaktif di awal
#if UNITY_ANDROID || UNITY_IOS
        controllerPanel.SetActive(true);
#else
        controllerPanel.SetActive(false);
#endif
        gameOverPanel.SetActive(false);
        finishPanel.SetActive(false);
        finishToMainMenu.gameObject.SetActive(false);
    }

    public void GameOver()
    {
        controllerPanel.SetActive(false);
        Time.timeScale = 0f;
        diedCount--;

        // Simpan data
        PlayerPrefs.SetInt("DiedCount", diedCount);
        PlayerPrefs.Save();

        // Update text
        diedText1.text = $"{diedCount}";
        diedText2.text = $"{diedCount}";

        gameOverPanel.SetActive(true);
    }

    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Finish()
    {
        controllerPanel.SetActive(false);
        PlayerPrefs.DeleteKey("DiedCount");
        StartCoroutine(FinishSequence());
    }

    private IEnumerator FinishSequence()
    {
        // Pop-in panel
        yield return StartCoroutine(PopIn(finishPanel));

        // Tunggu 3 detik
        yield return new WaitForSecondsRealtime(3f);

        // Munculkan tombol
        finishToMainMenu.gameObject.SetActive(true);

        // Tambahkan event klik
        finishToMainMenu.onClick.RemoveAllListeners();
        finishToMainMenu.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("MainMenu"); // ganti sesuai nama scene Main Menu kamu
        });
    }

    private IEnumerator PopIn(GameObject obj)
    {
        obj.transform.localScale = Vector3.zero;
        obj.SetActive(true);

        float t = 0;
        while (t < 1)
        {
            t += Time.unscaledDeltaTime / 0.2f; // durasi 0.2 detik
            float scale = Mathf.SmoothStep(0, 1, t);
            obj.transform.localScale = Vector3.one * scale;
            yield return null;
        }
    }

}
