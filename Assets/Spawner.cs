using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("生成設定")]
    public GameObject trashPrefab;    // 你的垃圾 Prefab
    public Transform[] spawnPoints;   // 那 9 個洞口 (Hole_1 ~ Hole_9)
    public float spawnInterval = 1.5f; // 生成間隔

    private bool isSpawning = false;

    // 由 GameManager 的 StartGame() 呼叫
    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            // 延遲 1 秒後開始，每隔 spawnInterval 秒生成一次
            InvokeRepeating("SpawnTrash", 1.0f, spawnInterval);
        }
    }

    // 停止生成（用於遊戲結束時）
    public void StopSpawning()
    {
        isSpawning = false;
        CancelInvoke("SpawnTrash");
    }

    void SpawnTrash()
    {
        if (spawnPoints.Length == 0 || trashPrefab == null) return;

        // 隨機選一個洞口
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform point = spawnPoints[randomIndex];

        // 在選中的洞口位置生成垃圾
        Instantiate(trashPrefab, point.position, Quaternion.identity);
    }
}