2026-11-19

Initial idea and breakdown for final prototyping project

characters:
3d first person
Cats: artemis and apollo

setting: 
a house/apartment rooms

items:
kitchen:
note: 
    thank you for staying over and pet-sitting for the three day long-weekend. If you're reading this you remembered where the keys were Help yourself to the fridge. The canned wet food in on the counter. There's a thank you gift for you to open only on the LAST day. 

thank you gift:
    something related to artemis, if you open it before the time comes you'll aquire a portrait of artemis (only at the end, a paper sheet will fall on the ground and glitches the word missing then you wake up irl where there's only one cat)

food/water bowls: only one gets empty when you wake up
food: in cans
water jug in fridge??

---
visuals inspired by low-poly cat from low-poly shorts, small house 3-4 rooms? you can actually access, main rooms: kitchen and livingroom, need at least one door that can be interacted with 

livingroom:
-digital clock
-couch, table
-lamp
-tv
-windows?
-cat tree and bed

bathroom?(if time allows?)
 ------

Player states: Awake/Asleep/(combined? confusion state??)

Awake:
-regular lighting, only one cat visibly moves, the other is 'there' but it's actually an illusion and only truly moves in player's Asleep state, you can fill the food and water, pet apollo on your lap while sitting at the TV where you can go to sleep on the couch cause there's nothing left to do

Asleep:
-within asleep I want there to be two states: falsedreams (stressdreams/false memories that occur over the night) and realdreams (the true event that happened but with strangeness or parts missing that is supposed to 'feel' like a dream to the player. for example waking up to the kitchen door wide open and no cats in the house then you wake up and find the cats 'there' but only one(you get it))
-there's some kind of filter that visually indicates being in a asleep state

------
2026-11-22

I started working on the base environment for the gameplay to take place in a several inter connected rooms. I made several rooms using basic cubes with planes, keeping all the dimensions at the same scale and using the snapping/grid to avoid gaps. Despite using the grid and keeping the dimensions to the scale I set there are still gaps and pieces that don't seem to align correctly. I downloaded the probuilder package within unity and used the tools it provides to create doorways that can be passed through by deleting the planes after cutting them into the objects. I wanted no doors in the halls or doorways but I do want interactable front and back doors that have the proper functionality. I intended to make full room prefabs but I realized that I was wanting different dimensions in every room and it wouldn't actually be helpful for this prototype at this point at least.

To do?: 
-make room item layouts using grey square placeholders, how to implement scene changes for the different player wake states,implement interactions with environment,think about how to differentiate reality dream from false dream