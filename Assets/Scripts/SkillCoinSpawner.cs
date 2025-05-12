using UnityEngine;

public class SkillCoinSpawner : MonoBehaviour
{
    public GameObject coinPrefab;
    public float spawnInterval = 10f;
    private GameObject currentCoin;

    void Start() { InvokeRepeating(nameof(SpawnCoin), 2f, spawnInterval); }

    void SpawnCoin()
    {
        if (currentCoin != null) return;
        Vector3 pos = new Vector3(Random.Range(-17, 17), 0.5f, Random.Range(-17, 17));
        currentCoin = Instantiate(coinPrefab, pos, Quaternion.identity);
    }
} 