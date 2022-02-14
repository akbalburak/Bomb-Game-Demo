# Demo - Bomb Game

This is a demo game that you try to explode all the bricks before your bombs run out.
Bombs can only explode their 4 -left, right, up, down- directions.
This demo contains 20 levels.
This demo doesn't contain any sounds or animations.

## Screenshots
![Menu-1](https://user-images.githubusercontent.com/20722654/153935829-4222169f-71f8-4030-b298-6ef5c08495dd.png)
![Menu-2](https://user-images.githubusercontent.com/20722654/153935843-2ff9f3f4-aaae-461d-89bb-ef5dea0564bd.png)
![Menu-3](https://user-images.githubusercontent.com/20722654/153935864-e3359a92-ad55-44f9-b214-259267d605b3.png)

## Installation
You may install the Bomb Study Case.apk file in Apks folder to test game in your mobile device.

## Used Technologies
**Unity3D :** 2019.4.16f1

## Some Usefull Tips About Project
#### Save and Load System
You can use SaveData.cs to save any data into mobile device storage.
An important note : After any changes made, make sure you have called the SaveChanges() method in the SaveData.cs.
SaveLoadController.cs is going to load data from playerprefs automaticly.

#### Creating A Level
- Right click anywhere in the Levels folder and navigate the Create - Levels -> New Level.
- While you build a level, you should care the following things.
  - Level column and row counts limited with 9.
  - 0 represent -> Its an empty place.
  - 1 represent -> Its a brick place.
  - The Bomb Count player may use is calculated automatically via the brick count + 2 when the level initialized.
- As last step, just drag the new level into the LevelDataService's "Levels" property which is located in Services object in the scene.

#### Managing Menus
- You may use MenuViewController.cs to manage views, normally in the scene there is no ui element. They are instantiated from the Prefabs folder after dragged into the MenuViewController class.
- Before showing any menu to optmize memory management make sure you call the CloseAll() method in the MenuViewController.cs

## Final Thoughts
It was a funny project. Most probably i am going to update these repository in a couple of week for new sprites, sounds and other things.

For now, See you later.👏
