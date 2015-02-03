using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipSelect : FContainer {
	public static ShipSelect instance;
	private int ship1ID = UnityEngine.Random.Range(2,9);
	private int ship2ID = UnityEngine.Random.Range(2,9);
	private int asteroidSpawnRate = 6;
	private bool spawnAsteroids = false;
	
	private FSprite player1Logo;
	private FSprite player2Logo;
	private FSprite background;
	private FSprite redBar;
	private FSprite selectedCircle1;
	private FSprite selectedCircle2;
	private FSprite boxes1;
	private static float boxXOffset = 128f;
	private static float player1yOffset = -79f;
	private FSprite boxes2;
	private static float player2yOffset = -198f;
	private static float xBox = 27.5f;
	private static float yBox = 26f;
	private FButton plusAsteroids;
	private FButton minusAsteroids;
	private FButton noAsteroids;
	private FButton startButton;
	private FButton exitButton;
	private FButton toggleAutoPilot1;
	private FButton toggleAutoPilot2;
	private bool AP1 = false;
	private bool AP2 = true;
	private FLabel APOnLabel1;
	private FLabel APOffLabel1;
	private FLabel APOnLabel2;
	private FLabel APOffLabel2;
	private FLabel randomLabel1;
	private FLabel randomLabel2;
	
	private FButton demon1;
	private FButton demon2;
	private FButton angel1;
	private FButton angel2;
	private FButton fatty1;
	private FButton fatty2;
	private FButton ghost1;
	private FButton ghost2;
	private FButton ninja1;
	private FButton ninja2;
	private FButton storm1;
	private FButton storm2;
	private FButton plague1;
	private FButton plague2;
	private FButton random1;
	private FButton random2;
	
	//	private Random random = new Random();
	
	public ShipSelect ()
	{
		instance = this;
		
		if (DogfighterMain.instance.musicOn == true)
			FSoundManager.PlayMusic("Music/MeleeSelect", 0.2f);
		
		background = new FSprite("Ship_Select_BG.png");
		// eventually this will change to 1080
		background.scaleY = (Futile.screen.height / 1080.0f);
		// eventually this will change to 1920
		background.scaleX = (Futile.screen.width / 1920.0f);
		AddChild(background);
		
		redBar = new FSprite("Ship_Select_Bar.png");
		redBar.scaleX = (Futile.screen.width / (3f * 369f));
		redBar.scaleY = (Futile.screen.height / 1080f);
		redBar.x = Futile.screen.halfWidth - (Futile.screen.width / 6f);
		AddChild(redBar);
		
		player1Logo = new FSprite("Ship_Select_Player_1.png");
		player1Logo.scale = 0.25f;
		player1Logo.x = -(Futile.screen.halfWidth) + 61f;
		player1Logo.y = Futile.screen.halfHeight - 25f;
		AddChild(player1Logo);
		
		for (int j = 1; j > -1; j--) {
			for (int i = 1; i < 5; i++) {
				if (!(j == -0 && i == 4)) {
					createCircle(-(Futile.screen.halfWidth) + (i * xBox) - 9.75f, 
					             Futile.screen.halfHeight + player1yOffset + (j * yBox) - 1f);
				}
			}
		}
		
		random1 = new FButton("Holding_Circle_Unselected.png");
		random1.scale = 0.14f;
		random1.x = (-Futile.screen.halfWidth) + (4 * xBox) - 9.75f;
		random1.y = Futile.screen.halfHeight + player1yOffset + (0 * yBox) - 1f;
		AddChild(random1);
		random1.SignalRelease += Random1Release;
		
		for (int j = 1; j > -1; j--) {
			for (int i = 1; i < 5; i++) {
				if (!(j == -0 && i == 4)) {
					createCircle(-(Futile.screen.halfWidth) + (i * xBox) - 9.75f, 
					             Futile.screen.halfHeight + player2yOffset + (j * yBox) - 1f);
				}
			}
		}
		
		random2 = new FButton("Holding_Circle_Unselected.png");
		random2.scale = 0.14f;
		random2.x = (-Futile.screen.halfWidth) + boxXOffset - (1 * xBox);
		random2.y = Futile.screen.halfHeight + player2yOffset + (0 * yBox) - 1f;
		AddChild(random2);
		random2.SignalRelease += Random2Release;
		
		selectedCircle1 = new FSprite("Holding_Circle_Selected.png");
		selectedCircle1.scale = 0.14f;
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (4 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f;
		AddChild(selectedCircle1);
		
		selectedCircle2 = new FSprite("Holding_Circle_Selected.png");
		selectedCircle2.scale = 0.14f;
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (4 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f;
		AddChild(selectedCircle2);
		
		player2Logo = new FSprite("Ship_Select_Player_2.png");
		player2Logo.scale = 0.25f;
		player2Logo.x = -(Futile.screen.halfWidth) + 61f;
		player2Logo.y = Futile.screen.halfHeight - 144f;
		AddChild(player2Logo);
		
		startButton = new FButton("Ship_Select_Start.png");
		startButton.scale = 0.51f;
		startButton.x = Futile.screen.halfWidth - (Futile.screen.width / 6f) + 2f;
		startButton.y = -(Futile.screen.halfHeight) + 45f;
		AddChild(startButton);
		startButton.SignalRelease += StartButtonRelease;
		
		exitButton = new FButton("Ship_Select_Exit.png");
		exitButton.scale = 0.51f;
		exitButton.x = Futile.screen.halfWidth - (Futile.screen.width / 6f) + 2f;
		exitButton.y = -(Futile.screen.halfHeight) + 15f;
		AddChild(exitButton);
		exitButton.SignalRelease += ExitButtonRelease;
		
		plusAsteroids = new FButton("Ship_Select_Asteroids_+.png");
		plusAsteroids.scale = 0.51f;
		plusAsteroids.x = Futile.screen.halfWidth - (Futile.screen.width / 6f) + 2f;
		plusAsteroids.y = 0f;
		AddChild(plusAsteroids);
		plusAsteroids.SignalRelease += moreAsteroids;
		
		minusAsteroids = new FButton("Ship_Select_Asteroids_-.png");
		minusAsteroids.scale = 0.51f;
		minusAsteroids.x = Futile.screen.halfWidth - (Futile.screen.width / 6f) + 2f;
		minusAsteroids.y = -30f;
		AddChild(minusAsteroids);
		minusAsteroids.SignalRelease += lessAsteroids;
		
		noAsteroids = new FButton("Ship_Select_Asteroids_No.png");
		noAsteroids.scale = 0.51f;
		noAsteroids.x = Futile.screen.halfWidth - (Futile.screen.width / 6f) + 2f;
		noAsteroids.y = -59.5f;
		AddChild(noAsteroids);
		noAsteroids.SignalRelease += removeAsteroids;
		
		/* Disabled for Android Release */
		toggleAutoPilot1 = new FButton("YellowButton_normal.png");
		toggleAutoPilot1.scale = 0.51f;
		toggleAutoPilot1.x = -(Futile.screen.halfWidth) + (7 * xBox) - 9.75f;
		toggleAutoPilot1.y = Futile.screen.halfHeight + player1yOffset + (1 * yBox) - 1f;
		AddChild(toggleAutoPilot1);
		toggleAutoPilot1.SignalRelease += toggleAP1;

		APOnLabel1 = new FLabel("FranchiseFont_Scale1", "Turn AP On");
		APOnLabel1.x = toggleAutoPilot1.x;
		APOnLabel1.y = toggleAutoPilot1.y;
		APOnLabel1.scale = 0.5f;
		AddChild(APOnLabel1);

		APOffLabel1 = new FLabel("FranchiseFont_Scale1", "Turn AP Off");
		APOffLabel1.x = toggleAutoPilot1.x;
		APOffLabel1.y = toggleAutoPilot1.y;
		APOffLabel1.scale = 0.5f;
		//AddChild(APOffLabel1);

		toggleAutoPilot2 = new FButton("YellowButton_normal.png");
		toggleAutoPilot2.scale = 0.51f;
		toggleAutoPilot2.x = -(Futile.screen.halfWidth) + (7 * xBox) - 9.75f;
		toggleAutoPilot2.y = Futile.screen.halfHeight + player2yOffset + (1 * yBox) - 1f;
		AddChild(toggleAutoPilot2);
		toggleAutoPilot2.SignalRelease += toggleAP2;

		APOnLabel2 = new FLabel("FranchiseFont_Scale1", "Turn AP On");
		APOnLabel2.x = toggleAutoPilot2.x;
		APOnLabel2.y = toggleAutoPilot2.y;
		APOnLabel2.scale = 0.5f;

		APOffLabel2 = new FLabel("FranchiseFont_Scale1", "Turn AP Off");
		APOffLabel2.x = toggleAutoPilot2.x;
		APOffLabel2.y = toggleAutoPilot2.y;
		APOffLabel2.scale = 0.5f;
		AddChild(APOffLabel2);
		
		#region Ships
		demon1 = new FButton("Demon_Ship.png");
		demon1.scale = 0.1f;
		demon1.x = -(Futile.screen.halfWidth) + boxXOffset - (4 * xBox);
		demon1.y = Futile.screen.halfHeight + player1yOffset + (1 * yBox);
		AddChild(demon1);
		demon1.SignalRelease += Demon1Release;
		
		demon2 = new FButton("Demon_Ship.png");
		demon2.scale = 0.1f;
		demon2.x = -(Futile.screen.halfWidth) + boxXOffset - (4 * xBox);
		demon2.y = Futile.screen.halfHeight + player2yOffset + (1 * yBox);
		AddChild(demon2);
		demon2.SignalRelease += Demon2Release;
		
		angel1 = new FButton("Angel_Ship.png");
		angel1.scale = 0.1f;
		angel1.x = -(Futile.screen.halfWidth) + boxXOffset - (3 * xBox);
		angel1.y = Futile.screen.halfHeight + player1yOffset + (1 * yBox);
		AddChild(angel1);
		angel1.SignalRelease += Angel1Release;
		
		angel2 = new FButton("Angel_Ship.png");
		angel2.scale = 0.1f;
		angel2.x = -(Futile.screen.halfWidth) + boxXOffset - (3 * xBox);
		angel2.y = Futile.screen.halfHeight + player2yOffset + (1 * yBox);
		AddChild(angel2);
		angel2.SignalRelease += Angel2Release;
		
		fatty1 = new FButton("Fatty_Ship.png");
		fatty1.scale = 0.1f;
		fatty1.x = -(Futile.screen.halfWidth) + boxXOffset - (2 * xBox);
		fatty1.y = Futile.screen.halfHeight + player1yOffset + (1 * yBox);
		AddChild(fatty1);
		fatty1.SignalRelease += Fatty1Release;
		
		fatty2 = new FButton("Fatty_Ship.png");
		fatty2.scale = 0.1f;
		fatty2.x = -(Futile.screen.halfWidth) + boxXOffset - (2 * xBox);
		fatty2.y = Futile.screen.halfHeight + player2yOffset + (1 * yBox);
		AddChild(fatty2);
		fatty2.SignalRelease += Fatty2Release;
		
		ghost1 = new FButton("Ghost_Ship.png");
		ghost1.scale = 0.1f;
		ghost1.x = -(Futile.screen.halfWidth) + boxXOffset - (1 * xBox);
		ghost1.y = Futile.screen.halfHeight + player1yOffset + (1 * yBox);
		AddChild(ghost1);
		ghost1.SignalRelease += Ghost1Release;
		
		ghost2 = new FButton("Ghost_Ship.png");
		ghost2.scale = 0.1f;
		ghost2.x = -(Futile.screen.halfWidth) + boxXOffset - (1 * xBox);
		ghost2.y = Futile.screen.halfHeight + player2yOffset + (1 * yBox);
		AddChild(ghost2);
		ghost2.SignalRelease += Ghost2Release;
		
		ninja1 = new FButton("Ninja_Ship.png");
		ninja1.scale = 0.1f;
		ninja1.x = -(Futile.screen.halfWidth) + boxXOffset - (4 * xBox);
		ninja1.y = Futile.screen.halfHeight + player1yOffset + (0 * yBox);
		AddChild(ninja1);
		ninja1.SignalRelease += Ninja1Release;
		
		ninja2 = new FButton("Ninja_Ship.png");
		ninja2.scale = 0.1f;
		ninja2.x = -(Futile.screen.halfWidth) + boxXOffset - (4 * xBox);
		ninja2.y = Futile.screen.halfHeight + player2yOffset + (0 * yBox);
		AddChild(ninja2);
		ninja2.SignalRelease += Ninja2Release;
		
		storm1 = new FButton("Storm_Ship.png");
		storm1.scale = 0.1f;
		storm1.x = -(Futile.screen.halfWidth) + boxXOffset - (3 * xBox);
		storm1.y = Futile.screen.halfHeight + player1yOffset + (0 * yBox);
		AddChild(storm1);
		storm1.SignalRelease += Storm1Release;
		
		storm2 = new FButton("Storm_Ship.png");
		storm2.scale = 0.1f;
		storm2.x = -(Futile.screen.halfWidth) + boxXOffset - (3 * xBox);
		storm2.y = Futile.screen.halfHeight + player2yOffset + (0 * yBox);
		AddChild(storm2);
		storm2.SignalRelease += Storm2Release;
		
		plague1 = new FButton("Plague_Ship.png");
		plague1.scale = 0.1f;
		plague1.x = -(Futile.screen.halfWidth) + boxXOffset - (2 * xBox);
		plague1.y = Futile.screen.halfHeight + player1yOffset + (0 * yBox);
		AddChild(plague1);
		plague1.SignalRelease += Plague1Release;
		
		plague2 = new FButton("Plague_Ship.png");
		plague2.scale = 0.1f;
		plague2.x = -(Futile.screen.halfWidth) + boxXOffset - (2 * xBox);
		plague2.y = Futile.screen.halfHeight + player2yOffset + (0 * yBox);
		AddChild(plague2);
		plague2.SignalRelease += Plague2Release;
		
		randomLabel1 = new FLabel("FranchiseFont_Scale1", "?");
		randomLabel1.x = random1.x;
		randomLabel1.y = random1.y;
		randomLabel1.scale = 0.5f;
		AddChild(randomLabel1);
		
		randomLabel2 = new FLabel("FranchiseFont_Scale1", "?");
		randomLabel2.x = random2.x;
		randomLabel2.y = random2.y;
		randomLabel2.scale = 0.5f;
		AddChild(randomLabel2);
		
		
		#endregion
	}
	
	private void createCircle(float xPos, float yPos) {
		FSprite circle = new FSprite("Holding_Circle_Unselected.png");
		circle.x = xPos;
		circle.y = yPos;
		circle.scale = 0.14f;
		AddChild(circle);
	}
	
	private void StartButtonRelease(FButton button) {
		DogfighterMain.instance.SwitchToGame();
	}
	
	private void ExitButtonRelease(FButton button) {
		DogfighterMain.instance.titleMusic = true;
		DogfighterMain.instance.SwitchToStartMenu();
	}
	
	private void moreAsteroids(FButton button) {
		if (asteroidSpawnRate > 1)
		{
			spawnAsteroids = true;
			asteroidSpawnRate--;
		}
	}
	
	private void lessAsteroids(FButton button) {
		if (asteroidSpawnRate < 7)
			asteroidSpawnRate++;
		else
			spawnAsteroids = false;
	}
	
	private void removeAsteroids(FButton button) {
		asteroidSpawnRate = 6;
		spawnAsteroids = false;
	}
	
	private void toggleAP1(FButton button) {
		if (AP1)
		{
			AP1 = false;
			RemoveChild(APOffLabel1);
			AddChild(APOnLabel1);
		}
		else
		{
			AP1 = true;
			RemoveChild(APOnLabel1);
			AddChild(APOffLabel1);
		}
	}
	
	private void toggleAP2(FButton button) {
		if (AP2)
		{
			AP2 = false;
			RemoveChild(APOffLabel2);
			AddChild(APOnLabel2);
		}
		else
		{
			AP2 = true;
			RemoveChild(APOnLabel2);
			AddChild(APOffLabel2);
		}
	}
	
	private void Demon1Release(FButton button) {
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (1 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f + yBox;
		ship1ID = 2;	
	}
	private void Demon2Release(FButton button) {
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (1 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f + yBox;
		ship2ID = 2;	
	}
	private void Angel1Release(FButton button) {
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (2 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f + yBox;
		ship1ID = 3;	
	}
	private void Angel2Release(FButton button) {
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (2 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f + yBox;
		ship2ID = 3;	
	}
	private void Fatty1Release(FButton button) {
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (3 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f + yBox;
		ship1ID = 4;	
	}
	private void Fatty2Release(FButton button) {
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (3 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f + yBox;
		ship2ID = 4;	
	}
	private void Ghost1Release(FButton button) {
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (4 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f + yBox;
		ship1ID = 5;	
	}
	private void Ghost2Release(FButton button) {
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (4 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f+ yBox;
		ship2ID = 5;	
	}
	private void Ninja1Release(FButton button) {
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (1 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f;
		ship1ID = 6;	
	}
	private void Ninja2Release(FButton button) {
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (1 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f;
		ship2ID = 6;	
	}
	private void Storm1Release(FButton button) {
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (2 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f;
		ship1ID = 7;	
	}
	private void Storm2Release(FButton button) {
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (2 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f;
		ship2ID = 7;
	}
	private void Plague1Release(FButton button) {
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (3 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f;
		ship1ID = 8;	
	}
	private void Plague2Release(FButton button) {
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (3 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f;
		ship2ID = 8;	
	}
	private void Random1Release(FButton button) {
		selectedCircle1.x = -(Futile.screen.halfWidth) - 9.75f + (4 * xBox);
		selectedCircle1.y = Futile.screen.halfHeight + player1yOffset - 1f;
		ship1ID = UnityEngine.Random.Range(2,9);
	}
	private void Random2Release(FButton button) {
		selectedCircle2.x = -(Futile.screen.halfWidth) - 9.75f + (4 * xBox);
		selectedCircle2.y = Futile.screen.halfHeight + player2yOffset - 1f;
		ship2ID = UnityEngine.Random.Range(2,9);
	}
	
	public int getAsteroidRate() {
		return asteroidSpawnRate;	
	}
	
	public bool getSpawnAsteroids() {
		return spawnAsteroids;	
	}
	
	public int getShip1() {
		return ship1ID;
	}
	
	public int getShip2() {
		return ship2ID;	
	}
	
	public bool getAP1() {
		return AP1;
	}
	
	public bool getAP2() {
		return AP2;
	}
}

