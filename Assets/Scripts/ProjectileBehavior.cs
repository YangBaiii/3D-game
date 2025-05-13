using UnityEngine;

public class ProjectileBehavior : MonoBehaviour
{
    private Vector3 direction;
    private float speed;
    private float damage;
    private float lifetime;
    private TrailRenderer trailRenderer;

    void Awake()
    {
        // Get or add TrailRenderer for fire effect
        trailRenderer = GetComponent<TrailRenderer>();
        if (trailRenderer == null)
        {
            trailRenderer = gameObject.AddComponent<TrailRenderer>();
            trailRenderer.startWidth = 0.1f;
            trailRenderer.endWidth = 0.05f;
            trailRenderer.time = 0.2f;
            trailRenderer.material = new Material(Shader.Find("Sprites/Default"));
            trailRenderer.startColor = new Color(1f, 0.5f, 0f, 0.8f);
            trailRenderer.endColor = new Color(1f, 0f, 0f, 0f);
        }
    }

    public void Initialize(Vector3 dir, float spd, float dmg, float life)
    {
        direction = dir;
        speed = spd;
        damage = dmg;
        lifetime = life;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Health health = other.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (other.CompareTag("Environment"))
        {
            Destroy(gameObject);
        }
    }
} 