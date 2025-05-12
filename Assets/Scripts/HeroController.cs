using UnityEngine;

public class HeroController : MonoBehaviour
{
    public GameObject moveIndicatorPrefab;
    public float moveSpeed = 5f;
    private GameObject currentIndicator;
    private Vector3 targetPosition;
    private bool isMoving = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // Right mouse
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject.CompareTag("Map")) // Make sure to tag your map
                {
                    targetPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
                    isMoving = true;

                    if (currentIndicator != null) Destroy(currentIndicator);
                    currentIndicator = Instantiate(moveIndicatorPrefab, hit.point + Vector3.up * 0.1f, Quaternion.identity);
                }
            }
        }

        if (isMoving)
        {
            // Move towards target position
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            
            // Rotate to face movement direction
            if (targetPosition != transform.position)
            {
                Vector3 direction = (targetPosition - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
            }

            // Check if reached destination
            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                isMoving = false;
                if (currentIndicator != null)
                {
                    Destroy(currentIndicator);
                }
            }
        }
    }
} 