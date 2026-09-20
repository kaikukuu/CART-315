using UnityEngine;

public class InterferenceManager : MonoBehaviour
{

    // Reference to game manganer script to use the functions in it
    public GameManager gameManager;

    [Header("Interference Timer Settings")]
    public float minTime = 5.0f; // Minimum time between interference events
    public float maxTime = 10.0f; // Maximum time between interference events
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
            TriggerRandomInterference();
            ResetTimer();
        }
    }

    private void ResetTimer()
    {
        timer = Random.Range(minTime, maxTime);
    }

    private void TriggerRandomInterference()
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
                gameManager.SwapBallDirection();
                break;
            case 4:
                gameManager.ResetScore();
                break;
            case 5:
                gameManager.SwapPaddleDirection();
                break;



        }
    }

}
