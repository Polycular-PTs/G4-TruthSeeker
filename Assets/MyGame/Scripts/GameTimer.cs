using UnityEngine;
using TMPro;

public class gameTimer : MonoBehaviour
{
    public float startTime = 15f;
    private float timeLeft;
    private bool isPaused = false;

    public TextMeshProUGUI timerText;

    void Start()
    {
        timeLeft = startTime;
        UpdateTimerUI();
    }

    void Update()
    {
        if (isPaused || timeLeft <= 0)
            return;

        timeLeft -= Time.deltaTime;
        timeLeft = Mathf.Clamp(timeLeft, 0, startTime);

        UpdateTimerUI();

        if (timeLeft <= 0)
        {
            TimerAbgelaufen();
        }
    }

    void UpdateTimerUI()
    {
        timerText.text = Mathf.Ceil(timeLeft).ToString();
    }

    void TimerAbgelaufen()
    {
        Debug.Log("Timer abgelaufen!");
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }
}

