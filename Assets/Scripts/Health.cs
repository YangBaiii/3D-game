using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    public float maxHealth = 100f;
    public float currentHealth;
    public float damageFlashDuration = 0.2f;
    public Color damageFlashColor = Color.red;
    public Color healFlashColor = Color.green;

    [Header("UI References")]
    public HealthBar healthBar;
    public GameObject damageTextPrefab;
    public Transform damageTextSpawnPoint;

    [Header("Events")]
    public UnityEvent onDeath;
    public UnityEvent onDamage;
    public UnityEvent onHeal;

    private bool isInvincible = false;
    private Renderer[] renderers;
    private Color[] originalColors;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        // Cache renderers and their original colors for damage flash effect
        renderers = GetComponentsInChildren<Renderer>();
        originalColors = new Color[renderers.Length];
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                originalColors[i] = renderers[i].material.color;
            }
        }
    }

    public void TakeDamage(float damage)
    {
        if (isInvincible) return;
        
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            onDeath.Invoke();
        }
        else
        {
            onDamage.Invoke();
            StartCoroutine(DamageFlash());
            SpawnDamageText(damage);
        }
        UpdateHealthUI();
    }

    public void Heal(float amount)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
            onHeal.Invoke();
            StartCoroutine(HealFlash());
            SpawnDamageText(amount, true);
            UpdateHealthUI();
        }
    }

    public void SetInvincible(bool invincible)
    {
        isInvincible = invincible;
    }

    public float GetHealthPercentage()
    {
        return currentHealth / maxHealth;
    }

    private void UpdateHealthUI()
    {
        if (healthBar != null)
        {
            healthBar.UpdateHealth(GetHealthPercentage());
        }
    }

    private IEnumerator DamageFlash()
    {
        // Flash red
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material.HasProperty("_Color"))
            {
                renderer.material.color = damageFlashColor;
            }
        }

        yield return new WaitForSeconds(damageFlashDuration);

        // Restore original colors
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                renderers[i].material.color = originalColors[i];
            }
        }
    }

    private IEnumerator HealFlash()
    {
        // Flash green
        foreach (Renderer renderer in renderers)
        {
            if (renderer.material.HasProperty("_Color"))
            {
                renderer.material.color = healFlashColor;
            }
        }

        yield return new WaitForSeconds(damageFlashDuration);

        // Restore original colors
        for (int i = 0; i < renderers.Length; i++)
        {
            if (renderers[i].material.HasProperty("_Color"))
            {
                renderers[i].material.color = originalColors[i];
            }
        }
    }

    private void SpawnDamageText(float amount, bool isHeal = false)
    {
        if (damageTextPrefab != null)
        {
            Vector3 spawnPos = damageTextSpawnPoint != null ? 
                damageTextSpawnPoint.position : 
                transform.position + Vector3.up * 2f;

            GameObject damageText = Instantiate(damageTextPrefab, spawnPos, Quaternion.identity);
            DamageTextController textController = damageText.GetComponent<DamageTextController>();
            if (textController != null)
            {
                textController.Initialize(amount, isHeal);
            }
        }
    }

    void Die()
    {
        // Handle death
        Debug.Log($"{gameObject.name} has died!");
    }
} 