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

----------
19:00

Found out it was immpossible for the human player to actually score bc the Right court had: Box Collider2d: is Trigger was checked so I unchecked it and now it works...

I'm actually already close to being at the point where I no longer need to focus on the gameplay and could possibly explore some different appearance changes, provided I have the time and don't run into too many bugs. I believe Interference Pong's concept and implementation is already strong so I may not.

----------------
2026-09-23 22:00

-> To do this session:
    - Adjust score multiplication logic -> only double?
    - Change speed of ball
    - Paddle speed 
0:47

I made a lot of finishing touches to Interference Pong and it seems to have come together well. It's pretty much still my introduction to Unity and writing in C#, although I have lots of experience in other OOP languages like Java and C++ which informs my choices in how I decided to design the methods and Inteference Manager. I keep getting the itch to delve further in customizating assets and the appearance of the game but I'm holding myself back in order to focus on the implementation of features first. I love to be creative with visuals but I really want to challenge myself to start from the foundation (the program) rather than the appearance (assets and visual design). I tend to get ahead of myself when I'm excited and I delve into complex projects too quickly not taking the time to plan. This project has eessentially been an exercise in ignoring my need to make everything super nice looking and polished (aka my perfectionism rearing its head). By submitting this project in the state it is currently in wihout many outward chnages it highlights the gameplay changes not visible until the game is played and experienced. 