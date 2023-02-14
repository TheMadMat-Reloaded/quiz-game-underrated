# Overview

This is a quiz game built on the Unity engine. It's a quiz about the  supernatural television series created by Eric Kripke.

I have selected some questions that will test your knowledge about the supernatural world.

This game was made as a project for the video game programming and augmented reality bootcamp offered by Ucamp.

For this project I used assets free package [2D Casual UI HD](https://assetstore.unity.com/packages/2d/gui/icons/2d-casual-ui-hd-82080).

# Features

 - Quiz questions and answers are displayed randomly.
 - Each correct answer adds 100 points to the score counter.
 - When the player answers correctly, the answer will be marked in green for 0.5s before launching the next question. Otherwise, mark with red the selected answer and shows the correct answer in green, this lasts 0.5s before launching the next question.
 - During the waiting time the player cannot mark another question.

## Additional Features

 - You can try again only once, giving only 25 points for answering correctly, if you fail the second opportunity will lose the 25 points instead of adding them. Is possible to end up with negative points when answering incorrectly.
 - In the second opportunity it is possible to skip questions to avoid losing points or finish the quiz, the latter will take you to the game over screen.
 - After finishing the game it restarts with the scene manager.
 - Saves the highest score achieved (Player preference) and shows at the end, indicating whether the previous record was broken.
 - If time runs out, the quiz ends on the game over screen.

 ## Controls

 - Mouse ( Select the options ).
