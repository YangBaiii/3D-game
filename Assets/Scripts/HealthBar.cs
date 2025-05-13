using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Transform target; // The character to follow
    public Vector3 offset = new Vector3(0, 2f, 0); // Offset above the character
    public Slider healthSlider;
    public Image fillImage;
    public Gradient healthGradient; // Color gradient based on health percentage

    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        
        // Set up the health bar appearance
        if (healthSlider != null)
        {
            healthSlider.minValue = 0f;
            healthSlider.maxValue = 1f;
            healthSlider.value = 1f;
        }

        // Create gradient if not set
        if (healthGradient == null)
        {
            healthGradient = new Gradient();
            healthGradient.SetKeys(
                new GradientColorKey[] { 
                    new GradientColorKey(Color.red, 0.0f),
                    new GradientColorKey(Color.yellow, 0.5f),
                    new GradientColorKey(Color.green, 1.0f)
                },
                new GradientAlphaKey[] {
                    new GradientAlphaKey(1.0f, 0.0f),
                    new GradientAlphaKey(1.0f, 1.0f)
                }
            );
        }
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Update position to follow the character
            transform.position = target.position + offset;
            
            // Make the health bar face the camera
            transform.rotation = mainCamera.transform.rotation;
        }
    }

    public void UpdateHealth(float healthPercentage)
    {
        if (healthSlider != null)
        {
            healthSlider.value = healthPercentage;
            
            // Update fill color based on health percentage
            if (fillImage != null)
            {
                fillImage.color = healthGradient.Evaluate(healthPercentage);
            }
        }
    }
} 