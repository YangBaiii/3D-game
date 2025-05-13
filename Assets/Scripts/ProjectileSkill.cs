using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Attack/ProjectileSkill")]
public class ProjectileSkill : Skill
{
    [Header("Projectile Properties")]
    public GameObject projectilePrefab;
    public float projectileSpeed = 15f;
    public float projectileLifetime = 3f;
    public float projectileScale = 0.3f; // Smaller scale for a more reasonable fireball size
    public float damage = 10f;

    public override void Activate(GameObject caster)
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab not assigned!");
            return;
        }

        // Get mouse position in world space
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Vector3 targetPosition;

        if (Physics.Raycast(ray, out hit))
        {
            targetPosition = hit.point;
        }
        else
        {
            targetPosition = ray.GetPoint(100f);
        }

        // Calculate direction
        Vector3 direction = (targetPosition - caster.transform.position).normalized;
        
        // Spawn projectile slightly above ground and in front of caster
        Vector3 spawnPosition = caster.transform.position + direction * 1f;
        spawnPosition.y += 1f; // Adjust this value based on your character's height
        
        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.LookRotation(direction));

        // Set projectile scale
        projectile.transform.localScale = Vector3.one * projectileScale;

        // Add projectile behavior
        ProjectileBehavior behavior = projectile.AddComponent<ProjectileBehavior>();
        behavior.Initialize(direction, projectileSpeed, damage, projectileLifetime);

        // Play sound if available
        AudioSource audioSource = caster.GetComponent<AudioSource>();
        if (audioSource != null && skillSound != null)
        {
            audioSource.PlayOneShot(skillSound);
        }
    }

    public override void Deactivate(GameObject user)
    {
        // Nothing to deactivate for this skill
    }
} 