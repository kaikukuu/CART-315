using System;
using Unity.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using TMPro;

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

    private bool paddleDirectionSwapped;
    private bool ballSpeedBoosted;
    private bool paddleFrozen;

    //---------------

    private void Start()
    {
        StartRound();
    }

    public void StartRound()
    {
        ResetActiveInterferenceEffects();

        // Clears any prev vals
        if (CPUPaddle != null) CPUPaddle.paddleDir = 0;
        if (plyrPaddle != null) plyrPaddle.paddleDir = 0;
        ball.ResetBall();
        ball.AddStartingForce();
    }

    public void CourtTriggered(int courtId)
    {
        score.IncreaseScore((courtId == 0 ? 1 : 0)); //If left court was triggered, right player scores & vice versa
        StartRound();
    }

    // ---------------------
    // Resets the score for both players
    public void ResetScore()
    {
        score.ResetScore();
        Notify("Scores have been reset!");
    }

    // Randomly swap scores
    public void SwapScores()
    {
        int tempScore = score.scorePlayerOne;
        score.scorePlayerOne = score.scorePlayerTwo;
        score.scorePlayerTwo = tempScore;
        score.UpdateScore();

        Notify("Scores have been swapped!");
    }

    // Multiplies the score of the target player by 2
    public void ScoreMultiplier()
    {// Roll between 0 and 1 to pick the target player
     // 0 = player, 1 = CPU            
        int targetPlayerId = Random.Range(0, 2);
        int multiplier = 2;
        if (targetPlayerId == 0)
        {
            score.scorePlayerOne = multiplier * score.scorePlayerOne;
            score.UpdateScore();
            Notify("Player Score Multiplied");
        }
        else
        {
            score.scorePlayerTwo = multiplier * score.scorePlayerTwo;
            score.UpdateScore();
            Notify("CPU Score Multiplied");

        }
    }

    // Logic to swap paddle direction
    public void SwapPaddleDirection()
    {
        paddleDirectionSwapped = true;

        // Roll between 0 and 1 to pick the target player
        // 0 = player, 1 = CPU            
        int targetPlayerId = Random.Range(0, 2);
        // Check if the CPU paddle exists and is currently in the regular direction (0)
        if (CPUPaddle && plyrPaddle != null)
        {
            if (targetPlayerId == 0)
            {
                //Invert player paddle dir
                plyrPaddle.paddleDir = 1;
                CPUPaddle.paddleDir = 0;
                Notify("Your controls are inverted");
            }
            else if (targetPlayerId == 1)
            {
                CPUPaddle.paddleDir = 0;
                plyrPaddle.paddleDir = 1;
                Notify("CPU's controls are inverted");
            }
        }
    }

    // REset paddle direction to normal (0)
    public void ResetPaddleDirection()
    {
        if (CPUPaddle != null) CPUPaddle.paddleDir = 0;
        if (plyrPaddle != null) plyrPaddle.paddleDir = 0;
        Notify("Paddle direction reset to normal");
    }

    public void SwapBallDirection()
    {
        ball.ResetBallDirection();
        Notify("Ball Direction Swapped");
    }

    public void FreezePaddle()
    {
        paddleFrozen = true;

        // Roll between 0 and 1 to pick the target player
        // 0 = player, 1 = CPU            
        int targetPlayerId = Random.Range(0, 2);

        if (targetPlayerId == 0)
        {
            plyrPaddle.paddleDir = 0; // Freeze player paddle
            Notify("Player paddle frozen");
        }
        else
        {
            CPUPaddle.paddleDir = 0; // Freeze CPU paddle
            Notify("CPU paddle frozen");
        }
    }

    public void UnfreezePaddle()
    {
        // Reset paddle direction to normal (0)
        if (plyrPaddle != null) plyrPaddle.paddleDir = 0;
        if (CPUPaddle != null) CPUPaddle.paddleDir = 0;
        paddleFrozen = false;
        Notify("Paddle unfrozen");
    }

    // Adds a turbo boost to the ball's current velocity
    public void TurboBoostBall()
    {
        ballSpeedBoosted = true;
        ball.speed *= 5; // Double the ball speed
        Notify("Ball Turbo Boost");
    }

    public void ResetBallSpeed()
    {
        if (ball == null) return;
        ball.ResetBallSpeed();
        ballSpeedBoosted = false;
        Notify("Ball speed reset to normal");
    }

    public void ResetActiveInterferenceEffects()
    {
        if (paddleDirectionSwapped)
        {
            ResetPaddleDirection();
            paddleDirectionSwapped = false;
        }

        if (ballSpeedBoosted)
        {
            ResetBallSpeed();
        }

        if (paddleFrozen)
        {
            UnfreezePaddle();
        }
    }

    // Add text to the screen to indicate which interference event was triggered
    public void Notify(string eventName)
    {
        Debug.Log(eventName + " triggered!");

    }
}