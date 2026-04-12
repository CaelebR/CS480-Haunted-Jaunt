# Hanted Jaunt CS 480
(Worked on project solo)
## Dot porduct chnage 
A flashlight was added to the player that can be toggled on and off using the F key.
The flashlight uses a dot product to determine if a ghost is in front of the player.
(you have to go up to a wall to see if the flashlight it on. I could not figue out a simple way to make the 
light a beam of light also the light gose threw the wall could not figuer out how to fix that )

If a ghost is within range and in front of the player, it becomes stunned

If the ghost is outside the flashlight cone, it continues moving normally

## Linear Interpolation change 
Linear interpolation was used to create smoother transitions in gameplay:

The flashlight intensity fades in and out instead of instantly turning on/off

This is done using Mathf.Lerp to gradually change brightness

(once again you have to put the light up agenst the wall to see this change)

## Particle Effects
A particle effect was added to each ghost.

When a ghost is hit by the flashlight, a visual effect (particles) plays

When the ghost is no longer affected, the particles stop

## Sound Effects
A new sound effect was added for ghost interactions.

When a ghost is hit by the flashlight, a sound plays once

This helps give feedback to the player
