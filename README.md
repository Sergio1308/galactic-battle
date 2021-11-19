# galactic-battle
3D game in the genre "Top-Down Shooter" for Android OS (but it is possible to run it on other platforms, tested only on Windows OS)

The game was created for educational and entertainment purposes to get practice.

Mastered and done:

* Possibilities of the development environment "Unity" and how to use it to create cross-platform games
* Basic constructs and methods of the C# language (including those that are most often used to create scripts)
* Creation of mathematical regularities of the trajectory of movement of objects using C#
* Debugging, testing, building project (Android & Windows)
* OOP principles

![screen](https://i.imgur.com/QWfdbl9.jpg)

# Rules and game overview:

### Goal 
Pass the waves and earn as many points as possible. Points are awarded for killing enemies and obstacles (like asteroids). 

### Game mechanics
The game screen contains the player object (spaceship) and obstacles (asteroids and enemy spaceships) that move towards the player (from top to down of the playing area). 
Spawn of obstacles on the playing area is presented in the form of waves. 

<img src="https://i.imgur.com/tCnsqB3.png" width="400">

With each new wave, the number of asteroids increases, and the spawn rate decreases (it means that more obstacles will spawn).

Here we have two GUI elements (left and right upper corner of the screen). The left element, which is represented as a circle with a number, displays how many asteroids will appear on the playing area (I remind that this number increases by 5 each wave). The right one, which is represented as a rectangle, shows the current number of points earned by the player (Destroy as many obstacles on your way as possible). More details about the scoring system are written [in the project code structure](#scoring-system)).

<img src="https://i.imgur.com/JJXGDBD.png" width="400">

So far, enemy spaceships are classified by color and they are no different from each other, but in the future, color will indicate the rank of the enemy. Each rank will have its own characteristics and properties: color, health, shooting mode (e.g. shooting with two bullets).

<img src="https://i.imgur.com/9vpg9uK.png" width="400">

### Conditions to end the game

The player only has one life. If the player collides with an asteroid, an enemy spaceship or the player is shot by its bullet, the game will be over.

<img src="https://i.imgur.com/KGoljUQ.png" height="500">

### Control mechanics 

Firstly, you can get acquainted with the controls and basic mechanics of the game right in the game menu (by clicking on the "How to play" button, which flashes in different colors). This is how the main menu looks like:

<img src="https://i.imgur.com/gCWonPz.png" height="500">

Also, you can see it [here](https://i.imgur.com/m6s0BRW.png).

Since the game was created for Android OS, there are two ways to control the player game object (spaceship): touch control and accelerometer control.

Touch control is implemented in the form of two areas on the screen, each of which is responsible for performing different functions. The screenshot below shows these two areas in semi-transparent white colors (it is not visible in the game).

<img src="https://i.imgur.com/4fJrvg9.png" width="400">

The left area is the touch movement area. To control the spaceship, you need to drag on this area. The right one is touch shooting area, just tap on this to take a shot. But these areas are active only if accelerometer control is disables in options menu! More details about control implementation are described [in the project code structure](#spaceship-control)).

In order to set the movement of the spaceship in a certain direction, data from the accelerometer are used. To use accelerometer control, the player needs to switch the toggle in the settings menu:

<img src="https://i.imgur.com/LEtWKzp.png" width="400">

# Project structure

The project structure consists of the files required to compile and run the game. So, "Scripts" folder contains С# classes for the implementation of many events in the game.

### Scoring system

The method is implemented in the "GameController" script. The increase score function is as follows:
```
public void IncreaseScore(int increment)
    {
        score += increment;
        scoreText.GetComponent<UnityEngine.UI.Text>().text = "Score: " + score;
    }
```
The "increment" variable is used as an argument to the function. When calling the function to increase the current number of points, we indicate the exact value, for example, 10, then 10 will be added to the current value. 

### Implementation of the spaceship control system
#### Touch Control
<...>
