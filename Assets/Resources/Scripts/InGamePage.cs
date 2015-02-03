using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InGamePage : FContainer {
	
	private FLabel temp;
	private FSprite background;
	private int ship1 = 2;
	private int ship2 = 2;
	public static GravityWell gravWell;
	public static Asteroid asteroid;
	private int asteroidRate = 0;
	private bool spawnAsteroids;
	public static List<Ship> Ships = new List<Ship>();
	public static List<Bullet> Bullets = new List<Bullet>();
	public static List<Asteroid> Asteroids = new List<Asteroid>();
	public static List<bool> LivingPlayers = new List<bool>();
	public static int numPlayers = 2;
	public static int playersLeft;
	public static string winningPlayer;
	public static InGamePage instance;
	public static FLabel escapeMenu;
	public static int frameCount;
	public FButton primaryFireButton;
	public bool primaryPressed = false;
	public FButton secondaryFireButton;
	public bool secondaryPressed = false;
	public Vector3 baseRotation;


	public InGamePage() {
		instance = this;
		
		if (DogfighterMain.instance.musicOn == true)
			FSoundManager.PlayMusic("Music/Corneria", 0.2f);
		
		background = new FSprite("Original_Starfield.png");
		// eventually this will change to 1080
		background.scaleY = (Futile.screen.height / 1050.0f);
		// eventually this will change to 1920
		background.scaleX = (Futile.screen.width / 1680.0f);
		AddChild(background);

		/* Removed for HTML release.
		primaryFireButton = new FButton("Holding_Circle_Unselected.png");
		primaryFireButton.x = 25 - (Futile.screen.width/2);
		primaryFireButton.y = 25 - (Futile.screen.height/2);
		primaryFireButton.scale = 0.25f;
		primaryFireButton.SignalPress += primaryFireButtonPress;
		primaryFireButton.SignalRelease += primaryFireButtonRelease;
		AddChild(primaryFireButton);
		
		secondaryFireButton = new FButton("Holding_Circle_Selected.png");
		secondaryFireButton.x = (Futile.screen.width/2) - 25;
		secondaryFireButton.y = 25 - (Futile.screen.height/2);
		secondaryFireButton.scale = 0.25f;
		secondaryFireButton.SignalPress += secondaryFireButtonPress;
		secondaryFireButton.SignalRelease += secondaryFireButtonRelease;
		AddChild(secondaryFireButton);
		*/
		
		for(int i=0; i<numPlayers; i++) {
			LivingPlayers.Add(true);
		}
		playersLeft = numPlayers;

		escapeMenu = new FLabel("FranchiseFont_Scale1", "Press escape to return to menu");
		escapeMenu.y = -(Futile.screen.halfHeight / 2);
		
		asteroidRate = (40 * ShipSelect.instance.getAsteroidRate());
		spawnAsteroids = ShipSelect.instance.getSpawnAsteroids();
		CreateShips();
		
		frameCount = 0;

		baseRotation = Input.acceleration;
	}
	
	private void primaryFireButtonPress(FButton button)
	{
		primaryPressed = true;
	}
	
	private void primaryFireButtonRelease(FButton button)
	{
		primaryPressed = false;
	}
	
	private void secondaryFireButtonPress(FButton button)
	{
		secondaryPressed = true;
	}
	
	private void secondaryFireButtonRelease(FButton button)
	{
		secondaryPressed = false;
	}
	
	public bool getPrimaryFire()
	{
		return primaryPressed;	
	}
	
	public bool getSecondaryFire()
	{
		return secondaryPressed;	
	}
	
	//Adds and removes listeners for things added to stage
	//and calls HandleUpdate
	override public void HandleAddedToStage() {
		Futile.instance.SignalUpdate += HandleUpdate;
		base.HandleAddedToStage();		
	}
	
	override public void HandleRemovedFromStage() {
		Futile.instance.SignalUpdate -= HandleUpdate;
		base.HandleRemovedFromStage();
	}
	
	private void CreateShips() {
//		for(int i=1; i<=numPlayers; i++) {
//			Ships.Add(Ship.Create(i, i));
//		}
		ship1 = ShipSelect.instance.getShip1();
		ship2 = ShipSelect.instance.getShip2();
		Ships.Add(Ship.Create(ship1, 1));
		Ships.Add(Ship.Create(ship2, 2));

		Ships[0].x = -100.0f;
		Ships[0].rotation = 180.0f;
		Ships[1].x = 100.0f;
		AddChild(Ships[0]);
		AddChild(Ships[1]);
		Ships[0].setAutoPilot(ShipSelect.instance.getAP1());
		Ships[1].setAutoPilot(ShipSelect.instance.getAP2());
		
	}

	public static void checkForWin() {
		playersLeft = 0;
		foreach(bool living in LivingPlayers) {
			if(living == true) {
				playersLeft++;
			}
		}
		if(playersLeft == 1) {
			int winningPlayerIndex = LivingPlayers.IndexOf(true);
			winningPlayer = (winningPlayerIndex + 1).ToString();
			FLabel announceWin = new FLabel("FranchiseFont_Scale1", "PLAYER " + winningPlayer + " WINS!");
			instance.AddChild(announceWin);
			Ships[winningPlayerIndex].destroyShip();
			Ships[winningPlayerIndex].RemoveFromContainer();
			Ships.Clear();
			Bullets.Clear();
			Asteroids.Clear();
			LivingPlayers.Clear();
			instance.AddChild(escapeMenu);
		}
	}
	private static FLabel test;
	//Called once per frame
	private void HandleUpdate() {
		/*Vector3 tilt = Input.acceleration - baseRotation;
		String inputtext = tilt.magnitude.ToString();
		if (test != null)
		RemoveChild(test);
		test = new FLabel("FranchiseFont_Scale1", inputtext);
		AddChild(test);*/

		if(Input.GetKey(KeyCode.Escape)) {
			foreach(Ship ship in Ships) {
				//ship.destroyShip();
				ship.RemoveFromContainer();
			}
			Ships.Clear();
			Bullets.Clear();
			Asteroids.Clear();
			LivingPlayers.Clear();
			DogfighterMain.instance.titleMusic = true;
			DogfighterMain.instance.SwitchToStartMenu();
		}
		
		for(int i=0; i<LivingPlayers.Count; i++) {
			if(LivingPlayers[i]) {
				Ships[i].Update();
			}
		}
		
		for(int i=0; i<Bullets.Count; i++) {
			Bullets[i].Update();
		}
		
		//Generates randomized asteroids
		if(spawnAsteroids && frameCount % asteroidRate == 0){
			Asteroid asteroid = new Asteroid();
			AddChild(asteroid);
			Asteroids.Add(asteroid);
		}
		
		for(int i = 0; i < Asteroids.Count; i++) {
			Asteroids[i].Update();
		}
		
		frameCount++;
	}
	
}
