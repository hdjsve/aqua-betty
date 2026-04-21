using UnityEngine;

public class EffectManager : MonoBehaviour
{
    public GameObject brokenHeartUI; // 拖入畫面中間的破碎愛心物件

    public void ShowBrokenHeart()
    {
        brokenHeartUI.SetActive(true);
        // 如果你有寫 Animator，這裡會自動播放
        // 1秒後自動隱藏
        Invoke("HideBrokenHeart", 1f);
    }

    void HideBrokenHeart()
    {
        brokenHeartUI.SetActive(false);
    }
}