using UnityEngine;

public class PlayerProfile : MonoBehaviour
{
    public void SaveProfile()
    {
        PlayerPrefs.SetInt("Skill1Unlocked", 1);
        // Save other data as needed
        PlayerPrefs.Save();
    }

    public void LoadProfile()
    {
        int skill1 = PlayerPrefs.GetInt("Skill1Unlocked", 0);
        // Load other data as needed
    }
} 