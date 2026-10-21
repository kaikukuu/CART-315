2026-10-20 19:00

Brainstorming session:

game inspired by Simon swipe memory game by Hasbro

    -tiles light up in patterns and player must bounce ball off the right bricks shown in the order
    -may add some digital sketches 

Main changes outline:
-fewer tiles
-make rounds as sub levels
-make tile light up patterns
-what will i do about the changing colors ? and barriers
-paddle will be stuck on an arc instead of just straight
-prevent ball from randomly changing direction as often

TO do:
-researching splines and all the options for accomplishing the curved paddle on a determined path.
-------------------------------------------------
10-21

i felt so stuck at thre bbeginnning and had so much on my mind that i didn't have the mindspaace to come up with a topic that i felt would be succesful and mesh well with the base game for the brick breaking game 

lvl1:
required components:
-
blue(b): 1 hit
gree(g): 2 hits
yellow: 3 hits

ball-> button for pressing in the patterns
-> onpress of space key: activate the selected tile
-5 fixed positions for the paddle, controlled by left and right buttons

experiemental patterns: 
1.1: b1,b2,b3,b4,b5
1.2: b1,b2,b3,b4,b5,g1,g2,g3,g4,g5

playthru logic for game:
-> the tiles all light up once in a pattern
-> when the pattern is done it's the player's turn to repeat the pattern using the arrows L-R to move to the correct column's button then repeat the pattern by pressing the correct number of times for the tile's color and in the correct order 

Work on:
-Remove: ball, paddle and resetzone
brick.cs:
    edit or maybe discard most
    bricks will be separate from buttons 
    logic for lighting up patterns 
buttons.cs

Reflection: how can I change the rest of the game code to change this from a brick breaker game to a "simon-says" pattern call and response game. the unbreakable "bricks" are now buttons that the player uses to repeat back the pattern displayed on the bricks at the start of each sublvl. The player uses the right and left keys to switch between the buttons (columns) and press the space bar the correct number of times according to its color. I want the bricks to stay up in the same positions throughout lvl 1 then updates at lvl 2. The sublvls should use the same map but different patterns that escalate in difficulty. any logic from the brick breaker that wouldn't be useful can be overriden

maybe I'm doing too much.... 