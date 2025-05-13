using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Skills/Functional/HealingSkill")]
public class HealingSkill : Skill
{
    [Header("Healing Properties")]
    public float healAmount = 20f;
    public float healRadius = 3f;
    public GameObject healEffectPrefab;
    public Color healColor = new Color(0f, 1f, 0.5f, 0.5f);
    public float effectDuration = 2f;
    public float effectScale = 1f;

    public override void Activate(GameObject caster)
    {
        // Get the health component
        Health health = caster.GetComponent<Health>();
        if (health != null)
        {
            // Check if healing would be effective
            if (health.GetHealthPercentage() < 1f)
            {
                // Apply healing
                health.Heal(healAmount);
                
                // Spawn healing effect
                if (healEffectPrefab != null)
                {
                    GameObject effect = Instantiate(healEffectPrefab, caster.transform.position, Quaternion.identity);
                    effect.transform.parent = caster.transform;
                    effect.transform.localScale = Vector3.one * effectScale;

                    // Set effect color if it has a renderer
                    Renderer effectRenderer = effect.GetComponent<Renderer>();
                    if (effectRenderer != null)
                    {
                        effectRenderer.material.color = healColor;
                    }

                    // Add healing effect controller
                    HealingEffectController controller = effect.AddComponent<HealingEffectController>();
                    controller.Initialize(effectDuration);
                }

                // Play sound if available
                AudioSource audioSource = caster.GetComponent<AudioSource>();
                if (audioSource != null && skillSound != null)
                {
                    audioSource.PlayOneShot(skillSound);
                }
            }
        }
    }

    public override void Deactivate(GameObject user)
    {
        // Nothing to deactivate for this skill
    }
}

// Helper class to handle healing effect duration
public class HealingEffectController : MonoBehaviour
{
    public void Initialize(float duration)
    {
        StartCoroutine(EffectDuration(duration));
    }

    private IEnumerator EffectDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        Destroy(gameObject);
    }
} 