using UnityEngine;
using TMPro;

public class DamageTextController : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float fadeSpeed = 1f;
    public float lifetime = 1f;
    public TextMeshPro textMesh;

    private void Start()
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshPro>();
        }
        Destroy(gameObject, lifetime);
    }

    public void Initialize(float amount, bool isHeal = false)
    {
        if (textMesh != null)
        {
            textMesh.text = (isHeal ? "+" : "-") + amount.ToString("0");
            textMesh.color = isHeal ? Color.green : Color.red;
        }
    }

    private void Update()
    {
        // Float upward
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;

        // Fade out
        if (textMesh != null)
        {
            Color color = textMesh.color;
            color.a -= fadeSpeed * Time.deltaTime;
            textMesh.color = color;
        }
    }
} 