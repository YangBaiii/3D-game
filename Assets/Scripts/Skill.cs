using UnityEngine;

public abstract class Skill : ScriptableObject
{
    public enum SkillType { Attack, Defense, Healing }  // ✅ Move outside of header usage

    [Header("Basic Properties")]
    public string skillName;
    public string description;
    public Sprite skillIcon;
    public float cooldown;
    public int difficultyLevel;
    public float manaCost;

    [Header("Visual Effects")]
    public GameObject skillEffectPrefab;
    public AudioClip skillSound;

    [Header("Skill Type")]
    public SkillType skillType;  // ✅ Only apply [Header] to fields

    public abstract void Activate(GameObject user);
    public abstract void Deactivate(GameObject user);

    protected void PlayEffect(Vector3 position, Quaternion rotation)
    {
        if (skillEffectPrefab != null)
        {
            Instantiate(skillEffectPrefab, position, rotation);
        }
    }

    protected void PlaySound(AudioSource source)
    {
        if (skillSound != null && source != null)
        {
            source.PlayOneShot(skillSound);
        }
    }
}