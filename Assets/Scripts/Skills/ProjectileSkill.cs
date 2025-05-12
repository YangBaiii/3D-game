using UnityEngine;

[CreateAssetMenu(menuName = "Skills/Attack/ProjectileSkill")]
public class ProjectileSkill : Skill
{
    [Header("Projectile Properties")]
    public float projectileSpeed = 10f;
    public float damage = 10f;
    public float lifetime = 3f;

    public override void Activate(GameObject user)
    {
        // Get the user's forward direction
        Vector3 spawnPos = user.transform.position + user.transform.forward;
        Quaternion rotation = user.transform.rotation;

        // Spawn the projectile
        GameObject projectile = Instantiate(skillEffectPrefab, spawnPos, rotation);
        
        // Add projectile behavior
        ProjectileBehavior behavior = projectile.AddComponent<ProjectileBehavior>();
        behavior.Initialize(projectileSpeed, damage, lifetime);

        // Play effects
        PlayEffect(spawnPos, rotation);
        PlaySound(user.GetComponent<AudioSource>());
    }

    public override void Deactivate(GameObject user)
    {
        // Nothing to deactivate for this skill
    }
} 