using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public Skill[] skills = new Skill[4];
    private int selectedSkill = 0;
    private float[] cooldownTimers = new float[4];

    void Update()
    {
        // Skill selection
        if (Input.GetKeyDown(KeyCode.Alpha1)) selectedSkill = 0;
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSkill = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSkill = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) selectedSkill = 3;

        // Cooldown timer
        for (int i = 0; i < cooldownTimers.Length; i++)
            if (cooldownTimers[i] > 0) cooldownTimers[i] -= Time.deltaTime;

        // Skill activation
        if (Input.GetMouseButtonDown(0) && cooldownTimers[selectedSkill] <= 0)
        {
            skills[selectedSkill]?.Activate(gameObject);
            cooldownTimers[selectedSkill] = skills[selectedSkill].cooldown;
        }
    }
} 