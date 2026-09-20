using System;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Score score;
    public Ball ball;
    public GameStates gameState;

    //-----------------
    // int paddleDir = 0;
    // int randomNumMult = Random.Range(2, 11);
    // int randomNumSwap = Random.Range(0, 2);

    //Remember to assign the CPUController script to the CPUPaddle variable in the inspector
    public CPUController CPUPaddle;
    public PlayerController plyrPaddle;

    //---------------

    private void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        ball.ResetBall();
        ball.AddStartingForce();
    }

    public void CourtTriggered(int courtId)
    {
        score.IncreaseScore((courtId == 0 ? 1 : 0)); //If left court was triggered, right player scores & vice versa
        StartRound();
    }

    // ---------------------
    // Randomly resets the score for both players
    public void ResetScore()
    {
        score.ResetScore();
    }

    // Randomly swap scores
    public void SwapScores()
    {
        int tempScore = score.scorePlayerOne;
        score.scorePlayerOne = score.scorePlayerTwo;
        score.scorePlayerTwo = tempScore;

        Debug.Log("Scores Swapped");
    }

    public void ScoreMultiplier()
    {
        //Add some kind of logic to choose which score is chosen
        int randomNumMult = Random.Range(2, 11);
        score.scorePlayerOne = randomNumMult * score.scorePlayerOne;
        score.scorePlayerTwo = randomNumMult * score.scorePlayerTwo;
        Debug.Log(score + "Score Multiplied");
    }

    // Logic to swap paddle direction
    public void SwapPaddleDirection()
    {
        // Check if the CPU paddle exists and is currently in the regular direction (0)
        if (CPUPaddle && plyrPaddle != null)
        {
            int randomNumSwap = Random.Range(0, 2);

            if (randomNumSwap == 1)
            {
                CPUPaddle.paddleDir = 1;
                plyrPaddle.paddleDir = 1;
                // Invert the paddle direction
                Debug.Log("Paddle Direction Swapped");
            }
        }

    }

    public void SwapBallDirection()
    {
        ball.ResetBallDirection();
        Debug.Log("Ball Direction Swapped");
    }
}