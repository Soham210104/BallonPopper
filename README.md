# BallonPopper Unity Game Documentation

## Overview
BallonPopper is an engaging 3D game that challenges players to pop balloons floating in the air with a simple OnMouseClick mechanism. The game is externally controlled by a Python script, allowing customization of the spawn interval and the force applied to make the balloons float. It features a main menu and a game scene, providing an enjoyable gaming experience.

## Features
### 1.Interactice Gameplay
Players can pop the ballon's with the help of Mouse Click and players should avoid missing the ballon or else it would decrement our lives.

### 2.External Python Control
This game is controlled externally by the python script and in that python script the Spawn interval time of Ballon and the speed by which the ballon floats is controlled.
*My python script is in the Scrips folder*

### 3.MainMenu
This game consist of interactive menu section which serves as a entry point for the player and player's can also view their leaderboard.

### 4.Leaderboard Integration
Players can submit their scores to a leaderboard using PlayFab.This allows a competitive element to a game.

# Setting Python Script
I have created a basic python script in which the variable of spawnInterval and ballonForce is already defined with some values.
The values are sent to the Unity in the form of JSON string and will create a file of name `unity-data.json` in the folder.
Then i have compile it in VSCode.
*Remeber to run python script before playing the Unity Engine*

# PlayFab Integration
1. PlayFab is integrated with the help of *UnitySDK package* and then giving the title name through the PlayFab website.
2. In PlayFab website i have created a new Leaderboard name GameScore
3. For leaderboard integration,I have first created a custom_id **PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError)**
4. Then created a SendLeaderBoard function which will send my score value with this line of code **PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderBoardUpdate, OnError)**
5. To display the LeaderBoard data on clicking the leaderboard button from main menu I have used two functions **a) FetchLeaderboardData()** which will fetch the GameScore statistics data,
  **b) OnLeaderboardGet** which will fetch my rank,id,score in the variable which will be accessed by my TextMeshProUGUI varaibles in the Menu Manager Script.

# Gameplay


https://github.com/Soham210104/BallonPopper/assets/70838687/0a132006-f6e0-4abb-9f85-40945075245a





 
