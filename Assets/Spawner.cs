using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject trashPrefab; // 放入你的垃圾 Prefab
    public Transform[] spawnPoints; // 放入你的 9 個 Holes
    public float spawnInterval = 1.5f; // 每 1.5 秒出一顆垃圾

    void Start()
    {
        InvokeRepeating("SpawnTrash", 1f, spawnInterval);
    }

    void SpawnTrash()
    {
        // 隨機選一個洞
        int randomIndex = Random.Range(0, spawnPoints.Length);
        // 生成垃圾
        Instantiate(trashPrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}