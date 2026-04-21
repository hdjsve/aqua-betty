using UnityEngine;
using System.Collections; // 記得加這個才能用 Coroutine

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

    IEnumerator FlyToBag()
    {
        // 找到左下角垃圾袋的位置
        Vector3 targetPos = GameObject.Find("TrashBagTarget").transform.position;
        float duration = 0.5f; // 飛行時間
        float elapsed = 0;
        Vector3 startPos = transform.position;

        while (elapsed < duration)
        {
            // 每一幀往目標靠近一點
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, elapsed / duration); // 同時縮小
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(gameObject); // 到達後消失
    }

    void OnMouseDown() // 手機觸控或滑鼠點擊
    {
        // 點到了！加分（如果有分數系統的話）
        CancelInvoke("Missed"); // 取消倒數
        Destroy(gameObject); // 垃圾消失

        // 停止消失的倒數
        CancelInvoke("Missed");
        // 關閉碰撞體，避免重複點擊
        GetComponent<Collider2D>().enabled = false;

        // 開始飛向垃圾袋的動畫
        StartCoroutine(FlyToBag());
    }

    void Missed()
    {
        // 變色特效：變深褐色或綠色代表變髒
        GetComponent<SpriteRenderer>().color = new Color(0.5f, 0.4f, 0.2f);

        // 觸發畫面的愛心破碎動畫
        GameObject.FindObjectOfType<EffectManager>().ShowBrokenHeart();

        healthSystem.LoseHealth();
        Destroy(gameObject, 0.5f); // 延遲半秒消失，讓玩家看清楚變髒了

        // 沒點到，扣一顆愛心
        if (healthSystem != null)
        {
            healthSystem.LoseHealth();
        }
        Destroy(gameObject); // 垃圾消失
    }
}