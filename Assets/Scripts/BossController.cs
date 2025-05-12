using UnityEngine;

public class BossController : MonoBehaviour
{
    public Vector3 center = Vector3.zero;
    public float areaSize = 8f;
    public float moveSpeed = 3f;
    private Vector3 targetPosition;
    private float timer = 0f;

    void Start()
    {
        MoveToRandomPoint();
    }

    void Update()
    {
        timer += Time.deltaTime;
        
        // Move towards target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        // Rotate to face movement direction
        if (targetPosition != transform.position)
        {
            Vector3 direction = (targetPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
        }

        // Get new random position after delay or when reached destination
        if (timer > 3f || Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            MoveToRandomPoint();
            timer = 0f;
        }
    }

    void MoveToRandomPoint()
    {
        // Calculate random position within the 8x8 area
        float x = center.x + Random.Range(-areaSize/2, areaSize/2);
        float z = center.z + Random.Range(-areaSize/2, areaSize/2);
        targetPosition = new Vector3(x, transform.position.y, z);
    }
} 