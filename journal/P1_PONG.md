Ideas:

PONG Reimagined - 2026-09-20 1AM

Variations Ideas:

-> opposite pong
    -> Paddles are goals
    -> Try to avoid hitting the ball

-> Disguised pong
    -> Hidden in plain sight
    -> multiple balls? 
    -> one ball is the real ball the ohters are false balls

-> Interference pong
    -> An outside force randomly changes:
        -> direction of ball
        -> speed of ball
        -> speed of paddle

-> Fragile pong
    -> Every time the ball hits either paddle it cracks until it's completely gone


------------

2026-09-20 14:30

Reflecting back on my brainstorm and ideas the options I'm leaning towards are either Disguised or Interference Pong as they are the simplest to implement. I don't want to get too carried away and end up not completing the task so I'm attempting to rein myself in during the ideation phase. 

Currently the one I'm leaning towards developing/implementing is is Interference Pong.

Outline of changes to base game:

-> Create script that can access ball, paddles and score
Randomize:
-> Resetting score -> ResetScore()
    -> Swap scores
-> Reset ball -> ball.ResetBall()
-> Switch players controls
    ->  private void Update()
    {
        _direction = Vector2.zero;

        if (Keyboard.current.wKey.isPressed)
            _direction = Vector2.up;
        else if (Keyboard.current.sKey.isPressed)
            _direction = Vector2.down;

        paddle.direction = _direction;
    }
    -> Swap keyW and keyS
-> Switch paddles sides mid game ?

-> Score multiplier

Other considerations:
-> how long will these effects last for?
    -> Add timer?
-> what will trigger it?
-> how to decide which effect will be triggered
-> Add a text notification that tells the players when a change happens 

------------------------
17:30

Added many methods to control the different types of interference. I may have gone a bit overboard but I do feel like I;ve reached a solid point where I've built the base of what I imagined for Interference Pong.

So far my focus has been on the features of swapping the direction of moving objects, resetting objects/score, reversing the logic of the paddles for both player and CPU. Finally, I'm working on a driver class called the InterferenceManager controlled by a randomized timer which at certain point uses a random range to choose which case of interference will occur and for how long.