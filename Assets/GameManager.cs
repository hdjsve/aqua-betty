using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject endPanel;
    public TextMeshProUGUI endStatusText; // 記得連結這個！

    public void StartGame()
    {
        Time.timeScale = 1f;
        startPanel.SetActive(false);
        FindObjectOfType<Spawner>().StartSpawning();
    }

    public void GameOver(bool isWin)
    {
        Time.timeScale = 0f; // 時間停止
        endPanel.SetActive(true); // 顯示結束畫面

        if (isWin) endStatusText.text = "海洋變乾淨了！";
        else endStatusText.text = "海洋被淹沒了...";
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}