using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public Skill[] skills = new Skill[4];
    private int selectedSkill = 0;
    private float[] cooldownTimers;
    public Image[] skillIcons;
    public Image[] cooldownOverlays;

    void Start()
    {
        cooldownTimers = new float[skills.Length];
        Debug.Log("SkillManager initialized with " + skills.Length + " skill slots");
        for (int i = 0; i < skills.Length; i++)
        {
            if (skills[i] != null)
            {
                Debug.Log("Skill " + i + " is: " + skills[i].skillName);
            }
            else
            {
                Debug.Log("Skill " + i + " is null");
            }
        }
    }

    void Update()
    {
        // Skill selection
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedSkill = 0;
            Debug.Log("Selected skill 1: " + (skills[0] != null ? skills[0].skillName : "null"));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) selectedSkill = 1;
        if (Input.GetKeyDown(KeyCode.Alpha3)) selectedSkill = 2;
        if (Input.GetKeyDown(KeyCode.Alpha4)) selectedSkill = 3;

        // Update cooldowns
        for (int i = 0; i < cooldownTimers.Length; i++)
        {
            if (cooldownTimers[i] > 0)
            {
                cooldownTimers[i] -= Time.deltaTime;
                if (cooldownOverlays != null && cooldownOverlays[i] != null)
                {
                    cooldownOverlays[i].fillAmount = cooldownTimers[i] / skills[i].cooldown;
                }
            }
        }

        // Skill activation
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse clicked. Selected skill: " + selectedSkill);
            if (cooldownTimers[selectedSkill] <= 0)
            {
                if (skills[selectedSkill] != null)
                {
                    Debug.Log("Activating skill: " + skills[selectedSkill].skillName);
                    skills[selectedSkill].Activate(gameObject);
                    cooldownTimers[selectedSkill] = skills[selectedSkill].cooldown;
                }
                else
                {
                    Debug.Log("Selected skill is null!");
                }
            }
            else
            {
                Debug.Log("Skill on cooldown: " + cooldownTimers[selectedSkill] + " seconds remaining");
            }
        }
    }
} 