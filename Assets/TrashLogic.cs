using UnityEngine;
using System.Collections;

public class TrashLogic : MonoBehaviour
{
    [Header("基礎設定")]
    public float lifeTime = 2.0f;          // 垃圾存在時間
    public Sprite[] allTrashSprites;       // 放入那 19 種切割好的垃圾圖片

    [Header("外部連結")]
    private GameHealth healthSystem;
    private EffectManager effectMgr;
    private SpriteRenderer sr;
    private bool isProcessed = false;      // 確保垃圾只會被處理一次（點擊或漏掉）

    void Awake()
    {
        sr = GetComponent<SpriteRenderer>();

        // 1. 出生時隨機選一張圖片
        if (sr != null && allTrashSprites != null && allTrashSprites.Length > 0)
        {
            sr.sprite = allTrashSprites[Random.Range(0, allTrashSprites.Length)];
        }
    }

    void Start()
    {
        // 2. 自動尋找場景中的大腦與特效管理員
        healthSystem = Object.FindAnyObjectByType<GameHealth>();
        effectMgr = Object.FindAnyObjectByType<EffectManager>();

        // 3. 開始倒數計時，時間到就執行 Missed (沒點到)
        Invoke("Missed", lifeTime);
    }

    // 當玩家點擊到垃圾
    void OnMouseDown()
    {
        if (isProcessed) return; // 如果已經變髒或正在飛，就不重複執行
        isProcessed = true;

        CancelInvoke("Missed"); // 取消倒數
        GetComponent<Collider2D>().enabled = false; // 關閉碰撞，防止連點

        // 執行飛向左下角的動畫
        StartCoroutine(FlyToBag());
    }

    // 沒點到垃圾時的邏輯
    void Missed()
    {
        if (isProcessed) return;
        isProcessed = true;

        // 1. 變色特效：讓垃圾變暗褐色，代表污染了海洋
        if (sr != null)
        {
            sr.color = new Color(0.5f, 0.4f, 0.2f);
        }

        // 2. 觸發畫面中央的愛心破碎動畫
        if (effectMgr != null)
        {
            effectMgr.ShowBrokenHeart();
        }

        // 3. 扣除生命值
        if (healthSystem != null)
        {
            healthSystem.LoseHealth();
        }

        // 4. 延遲 0.5 秒後消失，讓玩家看到「變髒」的瞬間
        Destroy(gameObject, 0.5f);
    }

    // 飛向垃圾袋的協程動畫
    IEnumerator FlyToBag()
    {
        // 1. 尋找目標
        GameObject target = GameObject.Find("TrashBagTarget");

        if (target != null)
        {
            Vector3 startPos = transform.position;
            Vector3 targetPos = target.transform.position;

            // 強制把 Z 軸拉回 0，防止垃圾飛到攝影機後面
            targetPos.z = 0;
            startPos.z = 0;

            float duration = 0.5f;
            float elapsed = 0;

            while (elapsed < duration)
            {
                float t = elapsed / duration;
                // 增加平滑曲線
                t = t * t * (3f - 2f * t);

                // 移動位置
                transform.position = Vector3.Lerp(startPos, targetPos, t);
                // 縮小尺寸
                transform.localScale = Vector3.Lerp(Vector3.one, Vector3.zero, t);

                elapsed += Time.deltaTime;
                yield return null;
            }
        }

        Destroy(gameObject);
    }
}