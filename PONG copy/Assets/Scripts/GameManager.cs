using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int scoreP1 = 0;
    public int scoreP2 = 0;

    //-------------------
    //Add references to hold your UI Text objects
    public TextMeshProUGUI p1ScoreText;
    public TextMeshProUGUI p2ScoreText;

    public void Start()
    {
        //Update the UI Text objects to show the current score
        p1ScoreText.text = scoreP1.ToString();
        p2ScoreText.text = scoreP2.ToString();
    }
    //-------------------

    //plyerId=0 = Left player
    //plyerId=1 = Right player
    public void IncrScore(int playerID)
    {
        switch (playerID)
        {
            case 0: scoreP1++; break;
            case 1: scoreP2++; break;
        }
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        p1ScoreText.text = scoreP1.ToString();
        p2ScoreText.text = scoreP2.ToString();
    }

    //Wrapper methods that Unity's EventTrigger can actually "see"
    public void IncrPlayer1Score(BaseEventData data)
    {
        IncrScore(0);
    }

    public void IncrPlayer2Score(BaseEventData data)
    {
        IncrScore(1);
    }
}
