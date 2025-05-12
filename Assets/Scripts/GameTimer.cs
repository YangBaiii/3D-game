using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeLeft = 120f;
    public Text timerText;

    void Update()
    {
        timeLeft -= Time.deltaTime;
        timerText.text = Mathf.Ceil(timeLeft).ToString();
        if (timeLeft <= 0) EndGame();
    }

    void EndGame()
    {
        // Show win/lose UI
    }
} 