using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject baseObstaclePrefab;
    public int baseObstacleCount = 15;
    public int stackedObstacleCount = 5;

    void Start()
    {
        // Place base obstacles
        for (int i = 0; i < baseObstacleCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-17, 17), 0.5f, Random.Range(-17, 17));
            Instantiate(baseObstaclePrefab, pos, Quaternion.identity);
        }

        // Place stacked obstacles (2 cubes high)
        for (int i = 0; i < stackedObstacleCount; i++)
        {
            Vector3 pos = new Vector3(Random.Range(-17, 17), 0.5f, Random.Range(-17, 17));
            GameObject bottom = Instantiate(baseObstaclePrefab, pos, Quaternion.identity);
            Vector3 topPos = pos + new Vector3(0, 1, 0);
            Instantiate(baseObstaclePrefab, topPos, Quaternion.identity);
        }
    }
} 