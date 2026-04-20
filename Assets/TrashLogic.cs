using UnityEngine;

public class TrashLogic : MonoBehaviour
{
    public float lifeTime = 2.0f; // 垃圾停留 2 秒
    private GameHealth healthSystem; // 引用剛才提到的生命系統

    void Start()
    {
        // 找到畫面上的生命值管理員
        healthSystem = FindObjectOfType<GameHealth>();

        // 倒數計時，時間到就呼叫 Missed
        Invoke("Missed", lifeTime);
    }

    void OnMouseDown() // 手機觸控或滑鼠點擊
    {
        // 點到了！加分（如果有分數系統的話）
        CancelInvoke("Missed"); // 取消倒數
        Destroy(gameObject); // 垃圾消失
    }

    void Missed()
    {
        // 沒點到，扣一顆愛心
        if (healthSystem != null)
        {
            healthSystem.LoseHealth();
        }
        Destroy(gameObject); // 垃圾消失
    }
}