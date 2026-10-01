using UnityEngine;

public class InterferenceManager : MonoBehaviour
{

    // Reference to game manganer script to use the functions in it
    public GameManager gameManager;

    [Header("Interference Timer Settings")]
    public float minTime = 3.0f; // Minimum time between interference events
    public float maxTime = 8.0f; // Maximum time between interference events
    private float timer; // Timer to track events


    void Start()
    {
        ResetTimer();
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            TriggerRandomPaddleInterference();
            TriggerNonPaddleInterference();
            ResetTimer();
        }
    }
    // Resets the timer to a random value between minTime and maxTime
    private void ResetTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }

    private void TriggerRandomPaddleInterference()
    {
        if (gameManager == null) return;
        int triggeredEvent = Random.Range(0, 2); // Randomly choose an interference event
        switch (triggeredEvent)
        {
            case 0:
                gameManager.SwapPaddleDirection();
                break;
            case 1:
                gameManager.FreezePaddle();
                break;

        }
    }

    private void TriggerNonPaddleInterference()
    {
        if (gameManager == null) return;
        int triggeredEvent = Random.Range(0, 4); // Randomly choose an interference event
        switch (triggeredEvent)
        {
            case 0:
                gameManager.SwapBallDirection();
                break;
            case 1:
                gameManager.ScoreMultiplier();
                break;
            case 2:
                gameManager.SwapScores();
                break;
            case 3:
                // Randomly generate a boost multiplier between 1.5 and 3.0 to apply to ball speed
                float boostMultiplier = Random.Range(1.5f, 5.0f);
                gameManager.TurboBoostBall(boostMultiplier);
                break;
        }
    }

}
