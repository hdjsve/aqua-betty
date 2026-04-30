using UnityEngine;
using UnityEngine.UI;

public class GameHealth : MonoBehaviour
{
    [Header("生命值設定")]
    public int health = 3;
    public Image[] hearts; // 在 Inspector 裡把 Heart_1, 2, 3 拖進去

    private GameManager gameManager;

    void Start()
    {
        gameManager = Object.FindAnyObjectByType<GameManager>();

        // 確保遊戲開始時，所有愛心都是顯示狀態
        foreach (Image h in hearts)
        {
            if (h != null) h.enabled = true;
        }
    }

    public void LoseHealth()
    {
        if (health <= 0) return;

        health--;

        // 隱藏對應索引的愛心圖片
        if (hearts != null && health < hearts.Length)
        {
            hearts[health].enabled = false;
        }

        // 如果血量沒了，通知 GameManager 結束遊戲
        if (health <= 0)
        {
            if (gameManager != null)
            {
                gameManager.GameOver(false); // false 代表失敗
            }
        }
    }
}