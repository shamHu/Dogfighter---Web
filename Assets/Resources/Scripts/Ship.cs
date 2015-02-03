using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : FSprite {

	private bool autoPilot = false;
	Ship target;
	private float randomNum;
	private float randomNum2;

	private int player;
	private string shipimage;
	private String primaryFire;
	private String secondaryFire;
	private string primarySound;
	private string secondarySound;
	private int bulletid;
	private int secondaryid;
	private float primaryCost;
	private float secondaryCost;
	private float angle;
	private float maxSpeed;
	private float maxSpeedBase;
	private float thrust;
	private float thrustBase;
	private float size;
	private float turnRate;
	private float turnRateBase;
	private int firerate;
	private int secondaryfirerate;
	private float health;
	private float maxHealth;
	private float shields;
	private float maxShields;
	private float energy;
	private float maxEnergy;
	private float mass;
	private const float COR = 0.8f; //Coefficient of Restitution
	private FSprite healthBar;
	private FSprite shieldBar;
	private FSprite energyBar;
	private Bullet bullet;
	private string shipType;
	private KeyCode[] playerkeys = new KeyCode[6];
	private double frameCount;
	private double frameCountSecondary;
	private double autoPilotframeCount;
	private double autoRotateFC;
	public Vector2 velocity = Vector2.zero;
	public Vector2 curVel = Vector2.zero;
	public Vector2 curVel2 = Vector2.zero;
	private bool isNotColliding = true;
	public bool isNotCollidingAsteroid = true;
	private bool invulnerable;
	private float damageReduction = 0f;
	private bool slowed = false;
	private float poison = 0f;
	private int slowCounter;
	private int poisonCounter;
	private bool cloned = false;
	private FSprite altSprite; //temporary location for this still
	public Vector3 tilt;
	private FLabel playerLabel;
	//Missing private variables here

	public Ship(string Image) : base(Image) {
	}

	public static Ship Create(int shipId, int playerId) {
		switch(shipId) {
		case 1:
			return new Scout(playerId);
		case 2:
			return new Demon(playerId);
		case 3:
			return new Angel(playerId);
		case 4:
			return new Fatty(playerId);
		case 5:
			return new Ghost(playerId);
		case 6:
			return new Ninja(playerId);
		case 7:
			return new Storm(playerId);
		case 8:
			return new Plague(playerId);
		case 9:
			return new Agent(playerId);
		default:
			return new Cruiser(playerId);
		}
	}
	
	internal class Scout : Ship {
		public Scout(int player) : base("Bio_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 20f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "";
			secondarySound = "";
			bulletid = 0;
			secondaryid = 1;
			primaryCost = 1.0f;
			secondaryCost = 25.0f;
			BindKeys();
			maxSpeedBase = 175.0f;
			maxSpeed = maxSpeedBase;
			thrustBase = 200.0f;
			thrust = thrustBase;
			maxHealth = 130;
			maxShields = 30;
			turnRateBase = 5.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			mass = 3.0f;
			InGamePage.instance.AddChild(healthBar);
			firerate = 2;
			secondaryfirerate = 1;
			shipType = "Scout";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//Other Ship Values
		}
		
	}
	
	internal class Cruiser : Ship {
		public Cruiser(int player) : base("BY_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 20f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "";
			secondarySound = "";
			bulletid = 0;
			secondaryid = 1;
			primaryCost = 1.0f;
			secondaryCost = 50.0f;
			BindKeys();
			maxSpeedBase = 150.0f;
			maxSpeed = maxSpeedBase;
			thrustBase = 500.0f;
			thrust = thrustBase;
			maxHealth = 200;
			maxShields = 50;
			turnRateBase = 4.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			mass = 5.5f;
			InGamePage.instance.AddChild(healthBar);
			firerate = 1;
			secondaryfirerate = 1;
			shipType = "Cruiser";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//Other Ship Values
		}
		
	}

	internal class Demon : Ship {
		public Demon(int player) : base("Demon_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 20f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "Sounds/demonprimary";
			secondarySound = "Sounds/demonsecondary";
			bulletid = 3;
			secondaryid = 2;
			primaryCost = 1.25f;
			secondaryCost = 20.0f;
			BindKeys();
			maxSpeedBase = 150.0f;
			maxSpeed = maxSpeedBase;
			thrustBase = 300.0f;
			thrust = thrustBase;
			maxHealth = 150;
			maxShields = 30;
			turnRateBase = 4.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			InGamePage.instance.AddChild(healthBar);
			mass = 5.0f;
			firerate = 15;
			secondaryfirerate = 1;
			shipType = "Demon";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//Other Ship Values
		}
		
		override public void PrimaryFire() {
		if(frameCount > (30 / firerate)) {
			if(energy >= primaryCost){
				if (DogfighterMain.instance.soundOn)
					FSoundManager.PlaySound(primarySound, 1f);
				bullet = Bullet.Create(bulletid, player, 
						UnityEngine.Random.Range(-5f, 5f));
				InGamePage.instance.AddChild(bullet);
				InGamePage.Bullets.Add(bullet);
				frameCount = 0;
				energy -= primaryCost;
			}
		}
	}
		
		override public void SecondaryFire(){
			if(frameCountSecondary > (90 / secondaryfirerate)) {
				if(energy >= secondaryCost) {
					if (DogfighterMain.instance.soundOn)
						FSoundManager.PlaySound(secondarySound, 3f);
					energy -= secondaryCost;
					bullet = Bullet.Create(secondaryid, player, 0f);
					frameCountSecondary = 0;
					
					foreach(Ship ship in InGamePage.Ships)
						ship.HandleRemovedFromContainer();
						
					InGamePage.instance.AddChild(bullet);
					
					foreach(Ship ship in InGamePage.Ships)
						InGamePage.instance.AddChild(ship);
					
					InGamePage.Bullets.Add(bullet);
				}
			}
		}
	}

	internal class Angel : Ship {
		public Angel(int player) : base("Angel_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 20f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "Sounds/angelprimary";
			secondarySound = "Sounds/angelsecondary";
			bulletid = 4;
			primaryCost = 10f;
			secondaryCost = 0.5f;
			BindKeys();
			maxSpeedBase = 250.0f;
			maxSpeed = maxSpeedBase;
			thrustBase = 200.0f;
			thrust = thrustBase;
			maxHealth = 100;
			maxShields = 30;
			turnRateBase = 4.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			InGamePage.instance.AddChild(healthBar);
			mass = 5.0f;
			firerate = 1;
			secondaryfirerate = 1;
			shipType = "Angel";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//FSprite altSprite; 
			//Other Ship Values
		}
		
		override public void SecondaryFire() {
			if(energy > secondaryCost + 10f) {
				if(!invulnerable) {
					if (DogfighterMain.instance.soundOn)
						FSoundManager.PlaySound(secondarySound, 3f);
					invulnerable = true;
					altSprite = 
						new FSprite("Angel_Ship_Plus_Invulnerability.png");
					altSprite.scaleX = size / 100;
					altSprite.scaleY = size / 100;
					InGamePage.instance.RemoveChild(this);
					InGamePage.instance.AddChild(altSprite);
					altSprite.x = x;
					altSprite.y = y;
					altSprite.rotation = rotation;
					energy -= 5f;
				}
				else{
					energy -= secondaryCost;
					altSprite.x = x;
					altSprite.y = y;
					altSprite.rotation = rotation;
				}
			}
			else {
				invulnerable = false;
				altSprite.RemoveFromContainer();
				InGamePage.instance.AddChild(this);
			}
		}
		
		override public void endSecondaryFire() {
			if(invulnerable) {
				invulnerable = false;
				altSprite.RemoveFromContainer();
				InGamePage.instance.AddChild(this);
			}
		}
	}
	
	internal class Fatty : Ship {
		public Fatty(int player) : base("Fatty_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 24f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "Sounds/fattyprimary";
			secondarySound = "Sounds/fattysecondary";
			bulletid = 5;
			secondaryid = 6;
			primaryCost = 10f;
			secondaryCost = 2f;
			BindKeys();
			maxSpeedBase = 150.0f;
			maxSpeed = maxSpeedBase;
			thrustBase = 125.0f;
			thrust = thrustBase;
			maxHealth = 175;
			maxShields = 30;
			turnRateBase = 3.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			InGamePage.instance.AddChild(healthBar);
			mass = 5.0f;
			firerate = 1;
			secondaryfirerate = 20;
			shipType = "Fatty";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//FSprite altSprite; 
			//Other Ship Values
		}
		
		override public void PrimaryFire() {
			if(frameCount > (30 / firerate)) {
				if(energy >= primaryCost){
					if (DogfighterMain.instance.soundOn)
						FSoundManager.PlaySound(primarySound, 3f);
					for(float i = -3f; i <= 3f; i += 1f)
					{
						bullet = Bullet.Create(bulletid, player, (i * 30));
						InGamePage.instance.AddChild(bullet);
						InGamePage.Bullets.Add(bullet);
					}
					frameCount = 0;
					energy -= primaryCost;
				}
			}
		}
		
		override public void SecondaryFire() {
			if(frameCountSecondary > (90 / secondaryfirerate)) { 
				if(energy >= secondaryCost){
					if (DogfighterMain.instance.soundOn)
						FSoundManager.PlaySound(secondarySound, 3f);
					bullet = Bullet.Create(secondaryid, player, 
						UnityEngine.Random.Range(-5f, 5f));
					InGamePage.instance.AddChild(bullet);
					InGamePage.Bullets.Add(bullet);
					frameCountSecondary = 0;
					
					velocity.x += (8 * (Mathf.Sin(angle) * thrust * Time.deltaTime) / mass);
					velocity.y += (8 * (Mathf.Cos(angle) * thrust * Time.deltaTime) / mass);
					
					if(velocity.magnitude > maxSpeed) {
						velocity.Normalize();
						velocity *= maxSpeed;
					}
					
					energy -= secondaryCost;
				}
			}
		}
	}
	
	internal class Ghost : Ship {
		public Ghost(int player) : base("Ghost_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 16f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "Sounds/ghostprimary";
			secondarySound = "Sounds/ghostsecondary";
			bulletid = 7;
			primaryCost = 1.25f;
			secondaryCost = 0.5f;
			BindKeys();
			maxSpeedBase = 175.0f;
			maxSpeed = maxSpeedBase;
			thrustBase = 300.0f;
			thrust = thrustBase;
			maxHealth = 90;
			maxShields = 50;
			turnRateBase = 6.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			mass = 3.0f;
			InGamePage.instance.AddChild(healthBar);
			firerate = 16;
			shipType = "Ghost";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//Other Ship Values
		}
		
		//overriding primary fire just to decrease the volume of the Ghost Beam attack. 
		override public void PrimaryFire() {
			if(frameCount > (30 / firerate)) {
				if(energy >= primaryCost){
					if (DogfighterMain.instance.soundOn)
						FSoundManager.PlaySound(primarySound, 0.1f);
					bullet = Bullet.Create(bulletid, player, 0f);
					InGamePage.instance.AddChild(bullet);
					InGamePage.Bullets.Add(bullet);
					frameCount = 0;
					energy -= primaryCost;
				}
			}	
		}
		
		override public void SecondaryFire() {
			if(energy > secondaryCost + 10f) {
				if(damageReduction == 0) {
					if (DogfighterMain.instance.soundOn)
						FSoundManager.PlaySound(secondarySound, 3f);
					damageReduction = 100f;
					altSprite = 
						new FSprite("Ghost_Ship_Stealth.png");
					altSprite.scaleX = size / 100;
					altSprite.scaleY = size / 100;
					InGamePage.instance.RemoveChild(this);
					InGamePage.instance.RemoveChild(this.healthBar);
					InGamePage.instance.RemoveChild(this.energyBar);
					InGamePage.instance.RemoveChild(this.shieldBar);
					InGamePage.instance.AddChild(altSprite);
					altSprite.x = x;
					altSprite.y = y;
					altSprite.rotation = rotation;
					energy -= 5f;
				}
				else{
					energy -= secondaryCost;
					altSprite.x = x;
					altSprite.y = y;
					altSprite.rotation = rotation;
				}
			}
			else {
				damageReduction = 0f;
				InGamePage.instance.AddChild(this);
				InGamePage.instance.AddChild(this.healthBar);
				InGamePage.instance.AddChild(this.energyBar);
				InGamePage.instance.AddChild(this.shieldBar);
				altSprite.RemoveFromContainer();
			}
		}
		
		override public void endSecondaryFire() {
			if(damageReduction != 0f) {
				damageReduction = 0f;
				altSprite.RemoveFromContainer();
				InGamePage.instance.AddChild(this);
				InGamePage.instance.AddChild(this.healthBar);
				InGamePage.instance.AddChild(this.energyBar);
				InGamePage.instance.AddChild(this.shieldBar);
			}
		}
		
	}
			
	internal class Ninja : Ship {
		public Ninja(int player) : base("Ninja_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 16f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "Sounds/ninjaprimary";
			secondarySound = "Sounds/ninjasecondary";
			bulletid = 8;
			primaryCost = 10.0f;
			secondaryCost = 50.0f;
			BindKeys();
			maxSpeedBase = 175f;
			maxSpeed = maxSpeedBase;
			thrustBase = 300.0f;
			thrust = thrustBase;
			maxHealth = 100;
			maxShields = 30;
			turnRateBase = 6.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			mass = 3.0f;
			InGamePage.instance.AddChild(healthBar);
			firerate = 1;
			secondaryfirerate = 1;
			shipType = "Ninja";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//Other Ship Values
		}
		
		override public void SecondaryFire() {
			
			if(frameCountSecondary > (90 / secondaryfirerate)) { 
				if(energy >= secondaryCost){
					if (DogfighterMain.instance.soundOn)
						FSoundManager.PlaySound(secondarySound, 3f);
					for(int i = 0; i < 100; i++)
					{
						updateStats();
						checkScreenWrap();
						x += Mathf.Sin(angle);
						y += Mathf.Cos(angle);
						
					}
					energy -= secondaryCost;
				}
				frameCountSecondary = 0;
			}
		}
	}
	
	internal class Storm : Ship {
		public Storm(int player) : base("Storm_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 16f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "Sounds/stormprimary";
			secondarySound = "Sounds/stormsecondary";
			bulletid = 9;
			secondaryid = 10;
			primaryCost = 10.0f;
			secondaryCost = 20.0f;
			BindKeys();
			maxSpeedBase = 150f;
			maxSpeed = maxSpeedBase;
			thrustBase = 200.0f;
			thrust = thrustBase;
			maxHealth = 130;
			maxShields = 30;
			turnRateBase = 4.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			mass = 3.0f;
			InGamePage.instance.AddChild(healthBar);
			firerate = 1;
			secondaryfirerate = 1;
			shipType = "Storm";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//Other Ship Values
		}
		
	}
	
	internal class Plague : Ship {
		public Plague(int player) : base("Plague_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 20f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "";
			secondarySound = "";
			bulletid = 11;
			secondaryid = 12;
			primaryCost = 2.0f;
			secondaryCost = 50.0f;
			BindKeys();
			maxSpeedBase = 150f;
			maxSpeed = maxSpeedBase;
			thrustBase = 200.0f;
			thrust = thrustBase;
			maxHealth = 130;
			maxShields = 30;
			turnRateBase = 5.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			mass = 3.0f;
			InGamePage.instance.AddChild(healthBar);
			firerate = 1;
			secondaryfirerate = 1;
			shipType = "Plague";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//Other Ship Values
		}
		
	}
	
	internal class Agent : Ship {
		public Agent(int player) : base("Agent_Ship.png") {
			playerLabel = new FLabel("FranchiseFont_Scale1", player.ToString());
			playerLabel.scale = 0.3f;		
			InGamePage.instance.AddChild(playerLabel);
			size = 20f;
			scaleX = size / 100;
			scaleY = size / 100;
			this.player = player;
			primarySound = "";
			secondarySound = "";
			bulletid = 13;
			//secondaryid = 14;
			primaryCost = 5.0f;
			secondaryCost = 25.0f;
			BindKeys();
			maxSpeedBase = 175.0f;
			maxSpeed = maxSpeedBase;
			thrustBase = 200.0f;
			thrust = thrustBase;
			maxHealth = 130;
			maxShields = 30;
			turnRateBase = 5.1f;
			turnRate = turnRateBase;
			health = maxHealth;
			healthBar = new FSprite("Health_Bar_Red.png");
			healthBar.scaleX = (health / (maxHealth * 3f));
			healthBar.scaleY = 0.2f;
			shields = maxShields;
			shieldBar = new FSprite("Health_Bar_Green.png");
			shieldBar.scaleX = (shields / (maxShields * 3f));
			shieldBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(shieldBar);
			mass = 3.0f;
			InGamePage.instance.AddChild(healthBar);
			firerate = 1;
			secondaryfirerate = 1;
			shipType = "Agent";
			maxEnergy = 100.0f;
			energy = maxEnergy;
			energyBar = new FSprite("Health_Bar_1.png");
			energyBar.scaleX = (energy / (maxEnergy * 3f));
			energyBar.scaleY = 0.2f;
			InGamePage.instance.AddChild(energyBar);
			invulnerable = false;
			//Other Ship Values
		}
		
		override public void SecondaryFire() {
			if (!cloned) {
				cloned = true;
				frameCountSecondary = 0;
			}
		}
		
		override public void secondarySpecial() {
			if (cloned) {
				FSprite agentCloud = new FSprite("Agent_Cloud.png");
				float currHealth = health;
				float currEnergy = energy;
				float currShields = shields;
				float xPos = this.x;
				float yPos = this.y;
				float currRotation = rotation;
				Vector2 currVector = velocity;
				
				if (frameCountSecondary == 0){
					//FSprite agentCloud = new FSprite("Agent_Cloud.png");
					agentCloud.x = this.x;
					agentCloud.y = this.y;
					agentCloud.scale = 0.5f;
					InGamePage.instance.AddChild(agentCloud);
					//InGamePage.Ships.Remove(this);
					//this.RemoveFromContainer();
					healthBar.RemoveFromContainer();
					shieldBar.RemoveFromContainer();
					energyBar.RemoveFromContainer();
					//InGamePage.instance.RemoveChild(this);
				}
				else if (frameCountSecondary == 120) {
					InGamePage.instance.RemoveChild(agentCloud);
					agentCloud.RemoveFromContainer();
					Ship newAgent1 = Ship.Create(9, player);
					Ship newAgent2 = Ship.Create(9, player);
					InGamePage.Ships.Remove(this);
					InGamePage.Ships.Add(newAgent1);
					InGamePage.Ships.Add(newAgent2);
					newAgent1.x = (xPos + Mathf.Cos(rotation));
					newAgent1.y = (yPos - Mathf.Sin(rotation));
					newAgent2.x = (xPos - Mathf.Cos(rotation));
					newAgent2.y = (yPos + Mathf.Sin(rotation));
					
					newAgent1.health = newAgent2.health = currHealth;
					newAgent1.energy = newAgent2.energy = currEnergy;
					newAgent1.shields = newAgent2.shields = currShields;
					newAgent1.rotation = newAgent2.rotation = currRotation;
					newAgent1.velocity = newAgent2.velocity = currVector;
					InGamePage.instance.AddChild(newAgent1);
					InGamePage.instance.AddChild(newAgent2);
				}
			}
		}
	}

	public virtual void Update() {

		playerLabel.x = x;
		playerLabel.y = y - size - 13f;

		tilt = Input.acceleration - InGamePage.instance.baseRotation;

		updateStats();

		//Handles rotation
		Rotate();

		if(autoPilot)
		{
			if(autoRotateFC > 30)
			{
				randomNum2 = UnityEngine.Random.Range (0f, 100f);
				autoRotateFC = 0;
			}
		}

		//Movement
		Move();
		
		//Acceleration
		if (autoPilot) {
			Accelerate();
		}
		else {
			if(Input.GetKey(playerkeys[0]))
				Accelerate();
			else
			{				
				if(tilt.magnitude >= 0.1f){
					Accelerate();
				}
			}
		}
		       
		//Firing
		if (autoPilot) {
			if (autoPilotframeCount >= 60)
			{
				if (randomNum < 75f) {
					PrimaryFire ();
				}
				else {
					SecondaryFire ();
				}

				if (autoPilotframeCount > 180)
				{
					autoPilotframeCount = 0;
					endSecondaryFire();
					randomNum = UnityEngine.Random.Range(0f, 100f);
				}
			}

			/*if (autoPilotframeCount > 180);
			{
				autoPilotframeCount = 0;
				endSecondaryFire();
				randomNum = UnityEngine.Random.Range(0f, 100f);
			}*/

		}
		else {
			if(Input.GetKey(playerkeys[4]))
				PrimaryFire ();
			else if (InGamePage.instance.getPrimaryFire() == true)
			{
				PrimaryFire();
			}	
			
			if(Input.GetKey(playerkeys[5]))
				SecondaryFire();
			else if (InGamePage.instance.getSecondaryFire() == true)
			{
				SecondaryFire();	
			}
			else
				endSecondaryFire();
		}

		secondarySpecial();
		
		//Ship on Ship Collision
		ResolveCollision();

		//Keeps the ship on the screen
		checkScreenWrap();
		
		checkSlow();
		
		checkPoison();

		frameCount++;
		frameCountSecondary++;
		autoPilotframeCount++;
		autoRotateFC++;
	}
		public void ResolveCollision() {
		int playerIndex;
		
		foreach(Ship ship in InGamePage.Ships) {
			if(ship.Equals(this)){
				continue;
			}
			playerIndex = ship.getPlayer() - 1;
			Ship collidingShip = InGamePage.Ships[playerIndex];
			
			if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
					(y - collidingShip.y) * (y - collidingShip.y)) > (this.Size()+collidingShip.Size())) {
				isNotColliding = true;
			}
			
			if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
					(y - collidingShip.y) * (y - collidingShip.y)) < (this.Size()+collidingShip.Size())) {
				if(isNotColliding) {
					
					float xVelocity1 = (COR*collidingShip.Mass()*(velocity.x -
						collidingShip.XVelocity()) + (mass * velocity.x) + (collidingShip.Mass() * 
						collidingShip.XVelocity()))/(mass + collidingShip.Mass());
					
					float yVelocity1 = (COR*collidingShip.Mass()*(velocity.y -
						collidingShip.YVelocity()) + (mass * velocity.y) + (collidingShip.Mass() * 
						collidingShip.YVelocity()))/(mass + collidingShip.Mass());
					
					float xVelocity2 = (COR*mass*(collidingShip.XVelocity() -
						velocity.x) + (collidingShip.Mass() * collidingShip.XVelocity()) + (mass * 
						velocity.x))/(collidingShip.Mass() + mass);
					
					float yVelocity2 = (COR*mass*(collidingShip.YVelocity() -
						velocity.y) + (collidingShip.Mass() * collidingShip.YVelocity()) + (mass * 
						velocity.y))/(collidingShip.Mass() + mass);
					
					curVel = new Vector2(xVelocity1, yVelocity1);
					curVel2 = new Vector2(xVelocity2, yVelocity2);
					//curVel = new Vector2(velocity.x, velocity.y);
					//curVel2 = new Vector2(collidingShip.XVelocity(), collidingShip.YVelocity());
					velocity = curVel2;
					collidingShip.velocity = curVel;
					isNotColliding = false;
				}
			}
			
		}
		
	}
	
	public virtual void updateStats() {
		//makes healthBar follow ship
		healthBar.x = x;
		healthBar.y = y + size + 5f;
		playerLabel.x = x;
		playerLabel.y = y - size - 13f;
		
		//makes shieldBar follow ship
		shieldBar.x = x;
		shieldBar.y = y + size + 8f;
		
		//makes energyBar follow ship
		energyBar.x = x;
		energyBar.y = y - size - 5f;

		//Handles energy regeneration
		if(energy < maxEnergy)
			energy += 0.1f;
		
		//Handles shield regeneration
		if(shields < maxShields)
			shields += 0.02f;
		
		//Handles energyBar resizing
		energyBar.scaleX = (energy / (maxEnergy * 3f));
		
		//Handles shieldBar resizing
		shieldBar.scaleX = (shields / (maxShields * 3f));	
	}
	
	public virtual void Rotate() {
		if (player == 1) {
			target = InGamePage.Ships[1];
		}
		else {
			target = InGamePage.Ships[0];
		}
		if (!autoPilot) {
			if(Input.GetKey(playerkeys[3])) {
				rotation += turnRate;
			}
			else if(Input.GetKey(playerkeys[1])) {
				rotation -= turnRate;
			}
			else if (Input.acceleration.magnitude != 0)
			{
				float tiltRotationAngle = ((angle + (Mathf.PI / 2 )) +  
				                           (Mathf.Atan2(tilt.y, tilt.x)) 
				                           % (Mathf.PI * 2));

				if(0 <= tiltRotationAngle &&
				   tiltRotationAngle < (Mathf.PI)){
					rotation += turnRate;
				}
				else if (tiltRotationAngle <= (Mathf.PI * -1)) {
					rotation += turnRate;
				}
				else {
					rotation -= turnRate;
				}
			}
		}
		else {
			float apRotationAngle = (((angle - ( Mathf.PI / 2 )) + 
			                         Mathf.Atan2(y - target.PosY(), x - target.PosX())) % 
			                         (Mathf.PI * 2));
			if (randomNum2 < 90f)
			{
				if(0 <= apRotationAngle &&
				   apRotationAngle < (Mathf.PI)){
					rotation += turnRate;
				}
				else if (apRotationAngle <= (Mathf.PI * -1)) {
					rotation += turnRate;
				}
				else {
					rotation -= turnRate;
				}
			}
			else
			{
				if(0 <= apRotationAngle &&
				   apRotationAngle < (Mathf.PI)){
					rotation -= turnRate;
				}
				else if (apRotationAngle <= (Mathf.PI * -1)) {
					rotation -= turnRate;
				}
				else {
					rotation += turnRate;
				}
			}
		}

		angle = rotation * (Mathf.PI / 180.0f);
	}
	
	public virtual void Move() {
		x += velocity.x * Time.deltaTime;
		y += velocity.y * Time.deltaTime;	
	}
	
	public virtual void Accelerate() {
		velocity.x += (Mathf.Sin(angle) * thrust * Time.deltaTime) / (mass / 2);
		velocity.y += (Mathf.Cos(angle) * thrust * Time.deltaTime) / (mass / 2);
			
		if(velocity.magnitude > maxSpeed) {
			velocity.Normalize();
			velocity *= maxSpeed;
		}	
	}

	public virtual void PrimaryFire() {
		if(frameCount > (30 / firerate)) {
			if(energy >= primaryCost){
				if (DogfighterMain.instance.soundOn)
					FSoundManager.PlaySound(primarySound, 3f);
				bullet = Bullet.Create(bulletid, player, 0f);
				InGamePage.instance.AddChild(bullet);
				InGamePage.Bullets.Add(bullet);
				frameCount = 0;
				energy -= primaryCost;
			}
		}
	}

	public virtual void SecondaryFire() {
		if(frameCountSecondary > (90 / secondaryfirerate)) { 
			if(energy >= secondaryCost){
				if (DogfighterMain.instance.soundOn)
					FSoundManager.PlaySound(secondarySound, 3f);
				bullet = Bullet.Create(secondaryid, player, 0f);
				InGamePage.instance.AddChild(bullet);
				InGamePage.Bullets.Add(bullet);
				frameCountSecondary = 0;
				energy -= secondaryCost;
			}
		}
	}
	
	public virtual void endSecondaryFire() {
	
	}
	
	public virtual void secondarySpecial() {
		
	}
	
	public virtual void checkScreenWrap() {
		if(x > Futile.screen.halfWidth) {
			x = -Futile.screen.halfWidth;
		}
		if(x < -Futile.screen.halfWidth) {
			x = Futile.screen.halfWidth;
		}
		if(y > Futile.screen.halfHeight) {
			y = -Futile.screen.halfHeight;
		}
		if(y < -Futile.screen.halfHeight) {
			y = Futile.screen.halfHeight;
		}	
	}

	public void dealDamage(float damage) {
		if(invulnerable == false) {
			float temp = (damage * ((100 - damageReduction) / 100f));
			while (shields > 0 && temp > 0) {
				shields--;
				temp--;
			}
			if(temp > 0)
				health -= temp;
		}
		healthBar.scaleX = (health / (maxHealth * 3f));
		//Handles ship death
		if(health <= 0) {
			destroyShip();
		}
	}
	
	public void healDamage(float heal) {
		while (health < maxHealth && heal > 0) {
			heal--;
			health++;
		}
		healthBar.scaleX = (health / (maxHealth * 6));
	}
	
	public void healShields(float heal) {
		while (shields < maxShields && heal > 0) {
			heal--;
			shields++;
		}
		shieldBar.scaleX = (shields / (maxShields * 3f));
	}
	
	public void inflictSlow(float slowAmount, int duration) {
		slowCounter = duration;

		if (slowed == false) {
			maxSpeed = maxSpeed / slowAmount;
			thrust = thrust / slowAmount;
			turnRate = turnRate / slowAmount;
			velocity = velocity / slowAmount;
		}
		slowed = true;
	}
	
	public void checkSlow() {
		if (slowCounter > 0)
			slowCounter--;
		else {
			slowed = false;
			maxSpeed = maxSpeedBase;
			thrust = thrustBase;
			turnRate = turnRateBase;
		}
	}
	
	public void inflictPoison(float damage, int duration) {
		poisonCounter = duration;
		poison = damage;
	}
	
	public void checkPoison() {
		dealDamage(poison);
		poisonCounter--;
		if (poisonCounter == 0)
			poison = 0;
	}

	public void destroyShip() {
		this.RemoveFromContainer();
		healthBar.RemoveFromContainer();
		energyBar.RemoveFromContainer();
		InGamePage.LivingPlayers[player - 1] = false;
		InGamePage.checkForWin();
	}

	//Temporary static key array until PlayerPrefs implemented
	private void BindKeys() {
		if(player == 1) {
			playerkeys[0] = KeyCode.W;
			playerkeys[1] = KeyCode.A;
			playerkeys[2] = KeyCode.S;
			playerkeys[3] = KeyCode.D;
			playerkeys[4] = KeyCode.Space;
			playerkeys[5] = KeyCode.LeftShift;
		} else {
			playerkeys[0] = KeyCode.UpArrow;
			playerkeys[1] = KeyCode.LeftArrow;
			playerkeys[2] = KeyCode.DownArrow;
			playerkeys[3] = KeyCode.RightArrow;
			playerkeys[4] = KeyCode.Period;
			playerkeys[5] = KeyCode.Comma;
		}
	}
	
	public int getPlayer() {
		return player;
	}

	public float Size() {
		return size;
	}

	public float PosX() {
		return x;
	}
	
	public float PosY() {
		return y;
	}
	
	public float Angle() {
		return angle;
	}

	public float XVelocity() {
		return velocity.x;
	}

	public float YVelocity() {
		return velocity.y;
	}
	
	public string getShipType(){
		return shipType;
	}				
	public float Mass() {
		return mass;
	}

	public bool IsNotCollidingAsteroid() {
		return isNotCollidingAsteroid;
	}
	
	public float isPoisoned() {
		return poison;	
	}

	public void setAutoPilot(bool setAP) {
		autoPilot = setAP;
	}

	public bool getAutoPilot() {
		return autoPilot;
	}

}