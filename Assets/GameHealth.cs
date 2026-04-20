using UnityEngine;
using UnityEngine.UI; // 使用 UI 必須加這一行

public class GameHealth : MonoBehaviour
{
    public int health = 3;
    public Image[] hearts; // 用來放你畫面上的 3 顆愛心圖片

    public void LoseHealth()
    {
        if (health <= 0) return;

        health--;

        // 根據剩餘血量隱藏愛心
        // 例如：剩 2 顆血，就隱藏 hearts[2]
        if (hearts != null && health < hearts.Length)
        {
            hearts[health].enabled = false;
        }

        if (health <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("遊戲結束！小美人魚哭了...");
        // 這裡之後可以寫：顯示遊戲結束面板
    }
}