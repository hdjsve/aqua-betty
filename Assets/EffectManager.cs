using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject brokenHeartUI; // 拖入畫面中間的 BrokenHeartFX 物件

    void Start()
    {
        if (brokenHeartUI != null) brokenHeartUI.SetActive(false);
    }

    public void ShowBrokenHeart()
    {
        if (brokenHeartUI != null)
        {
            brokenHeartUI.SetActive(true);
            // 1秒後自動隱藏
            Invoke("HideBrokenHeart", 1.0f);
        }
    }

    void HideBrokenHeart()
    {
        brokenHeartUI.SetActive(false);
    }
}