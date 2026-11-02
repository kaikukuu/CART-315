2026-10-27

Brainstorm:

-> walk thru space/the stars
    -> Particle logic??
-> twinkling
-> floating

sounds: -star sounds from observatory
-wind chimes
-

-> blackhole corner??
-> big star 
    -> if the player comes in contact: player can floatt up to reach the rest of the stars in the air
-> roaming blackhole: makes vacuum noise
    -> sucks up stars and the player if the player doesn't get away fast enough

Been working on:
-Made changes to make the plane's scale 10x what it was previously because when I was tsting the player view I kept running right off the edges
    -i don't want walls tho...
    - problem that came up was that the player can't reach all the stars in the air so I thought about adding a big star that if the player touches it allows them to float up
    - but if the player is floating up you could get sucked up by the blackhole
-added two types of star sounds to make two different sounding star prefabs
-

2026-10-28

worked on:
-my main ideas of a floating player or having a player be able to walk around a sphere would be a ton of work and I don't have enough time
-floating soln: making a platform that floats and has different controls to move around the platform itself
or trampoline bouncy spots
-cons: easy to miss the landing and slide off platform, need platform to remain floating for longer

-----------
2026-10-29

Adding the base for Prototype 3 and doing some experimentation with different ideas I could possibly implement in time for class. I'm thinking that I will make a walking sim thru the stars, space and maybe the planets. The player would walk on a plane and "explore" the universe.

------------
2026-10-30
I made several large changes and additions:

Made two types of star prefabs with a material that emits light and two types of sounds; a big star that goes in a random path leaving stars behind it-> still only produces star1 objects. Similarly I implemented the blackhole that goes in a random path and sucks up stars; if the player comes into contact with it the scene is reset as a kind of challenge or goal to avoid?

I've started experimenting with the Terrain and experimenting with the different brushes to build up structures -. the created terrain map is so large compared to the scale of the player which makes it difficult to get across a whole map and am still looking for a solution

Finally I made a platform that can lift the player and with a separate set of controls to move the platform. An interesting mistake that gave me something to think about occurred where I realized at some point that i had implemented the platform to rise when the player jumped. it was fun to see how far you can get with one jump and I'm thinking about its implementation possibly as both a transporter and a teleporter (since the discoverable map is so huge)

----------------
2026-10-31

Given that the prototype is due today this session I intend to attempt to pull the walking sim together but I still have no idea how. I got really frustrated yesterday as I made negligible progress.

I wanted to use the terrain object to paint back on some mountains for the player to walk through and explore. Using the terrain made the map so massive that no matter what I was trying to add it looked tiny and left the horizon very barren which I liked but I don't know if it would be interesting for someone to actually explore?

Sound effects:

https://www.videvo.net/royalty-free-sound-effects/twinkle/ : shooting star/star shimmering
https://pixabay.com/sound-effects/search/dead-air/ : ezanarini-air-ambience-234180
https://pixabay.com/sound-effects/search/shining/ : magiaz-brilho_magico-363509

I've been finding it a bit difficult to navigate and reorder 3d objects and scenes which has been a bit discouraging

Concept: it's dream like in the way that it's strange, I was also thinking about all the different planets from le petit prince which was pretty ambitious
-used thestair and stair landing assets from the example play room

more sounds:
https://pixabay.com/sound-effects/search/vacuum/?pagi=3 
https://pixabay.com/sound-effects/search/heartbeat/
https://pixabay.com/sound-effects/search/ethereal/?pagi=3 galaxy, ghostly sigh,crossing over

space between spaces

This session I made the whole floating ramp, reorganized the star assets that the player can collect, added sound effects to the blackhole/big star/footsteps, manipulated some of the player controls like the speed of movement and jumping range. I made some star paths to follow and a barrier through the middle of the plane to force the player to actually use the ramps, I highlighted the platform by adding an emmiting material to it (which isn't completely bug free and no longer goes up and down but still uses the arrow keys for the player to use). I also added a big red star that makes a heartbeat sound and beats as if the environment itself is alive in strangely organic ways. I think I managed to pull togehter a prototype that although incomplete embodies the type of otherworldly, slightly unnerving vibe I was going for. I'm not sure what next steps would be but I don't have anymore time to continue so this is it for now.