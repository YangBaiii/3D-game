using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Functional/ShieldSkill")]
public class ShieldSkill : Skill
{
    [Header("Shield Properties")]
    public float shieldDuration = 3f;
    public GameObject shieldEffectPrefab;
    public Color shieldColor = new Color(0.2f, 0.6f, 1f, 0.3f);
    public float shieldScale = 1.5f;

    public override void Activate(GameObject caster)
    {
        // Get the health component
        Health health = caster.GetComponent<Health>();
        if (health != null)
        {
            // Enable invincibility
            health.SetInvincible(true);

            // Spawn shield effect
            if (shieldEffectPrefab != null)
            {
                GameObject activeShield = Instantiate(shieldEffectPrefab, caster.transform.position, Quaternion.identity);
                activeShield.transform.parent = caster.transform;
                activeShield.transform.localScale = Vector3.one * shieldScale;

                // Set shield color if it has a renderer
                Renderer shieldRenderer = activeShield.GetComponent<Renderer>();
                if (shieldRenderer != null)
                {
                    shieldRenderer.material.color = shieldColor;
                }

                // Start shield duration coroutine
                ShieldController shieldController = activeShield.AddComponent<ShieldController>();
                shieldController.Initialize(shieldDuration, health);
            }

            // Play sound if available
            AudioSource audioSource = caster.GetComponent<AudioSource>();
            if (audioSource != null && skillSound != null)
            {
                audioSource.PlayOneShot(skillSound);
            }
        }
    }

    public override void Deactivate(GameObject user)
    {
        // Disable invincibility if skill is deactivated early
        Health health = user.GetComponent<Health>();
        if (health != null)
        {
            health.SetInvincible(false);
        }
    }
}

// Helper class to handle shield duration
public class ShieldController : MonoBehaviour
{
    private Health targetHealth;

    public void Initialize(float duration, Health health)
    {
        targetHealth = health;
        StartCoroutine(ShieldDuration(duration));
    }

    private IEnumerator ShieldDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        
        if (targetHealth != null)
        {
            targetHealth.SetInvincible(false);
        }
        Destroy(gameObject);
    }
} 