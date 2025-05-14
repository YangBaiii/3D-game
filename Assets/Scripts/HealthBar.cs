using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 2f, 0);
    public Slider healthSlider;
    public Color highHealthColor = Color.green;
    public Color lowHealthColor = Color.red;

    private Camera mainCamera;
    private Health targetHealth;
    private Image fillImage;

    void Start()
    {
        mainCamera = Camera.main;
        
        if (healthSlider != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = 1f;
            healthSlider.value = 1f;
            
            // Get the fill image
            fillImage = healthSlider.fillRect.GetComponent<Image>();
            if (fillImage != null)
            {
                fillImage.color = highHealthColor;
            }
        }

        if (target != null)
        {
            targetHealth = target.GetComponent<Health>();
            if (targetHealth != null)
            {
                targetHealth.onDamage.AddListener(OnHealthChanged);
                targetHealth.onHeal.AddListener(OnHealthChanged);
            }
        }
    }

    void OnDestroy()
    {
        if (targetHealth != null)
        {
            targetHealth.onDamage.RemoveListener(OnHealthChanged);
            targetHealth.onHeal.RemoveListener(OnHealthChanged);
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
            transform.rotation = mainCamera.transform.rotation;
        }
    }

    private void OnHealthChanged()
    {
        if (targetHealth != null)
        {
            UpdateHealth(targetHealth.GetHealthPercentage());
        }
    }

    public void UpdateHealth(float healthPercentage)
    {
        if (healthSlider != null)
        {
            healthSlider.value = healthPercentage;
            
            if (fillImage != null)
            {
                fillImage.color = Color.Lerp(lowHealthColor, highHealthColor, healthPercentage);
            }
        }
    }
} 