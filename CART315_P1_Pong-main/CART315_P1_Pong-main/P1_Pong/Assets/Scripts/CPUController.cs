using UnityEngine;

public class CPUController : MonoBehaviour
{
    public Ball ball;
    public Paddle paddle;

    //Variable to track whether in inverse direction state
    //paddleDir = 0 -> Regular 
    //paddleDir = 1 -> Swap paddles direction
    // public int paddleDir = 0;

    public bool isFrozen;
    public bool isInverted;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    // Update is called once per frame
    public void Update()
    {

        // if (paddleDir == 0)
        // {
        //     paddle.direction = new Vector2(0.0f, (ballPos - paddlePos).y);
        // }
        // else if (paddleDir == 1)
        // {
        //     paddle.direction = new Vector2(0.0f, -(ballPos - paddlePos).y);
        // }

        if (isFrozen)
        {
            // Set the paddle's direction to zero to stop it from moving
            paddle.direction = Vector2.zero;
            return;
        }
        else if (isInverted)
        {
            // Invert the paddle's direction to move in the opposite direction of the ball
            Vector2 ballPos = ball.transform.position;
            Vector2 paddlePos = paddle.transform.position;
            paddle.direction = new Vector2(0f, -(ballPos.y - paddlePos.y));


        }
        else
        {
            Vector2 ballPos = ball.transform.position;
            Vector2 paddlePos = paddle.transform.position;
            paddle.direction = new Vector2(0f, ballPos.y - paddlePos.y);
        }



    }

    //----------------------------

}