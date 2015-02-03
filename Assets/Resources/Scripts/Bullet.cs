using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : FSprite {

	private int player;
	private Ship owningShip;
	public float angle;
	private float speed;
	public Vector2 velocity = Vector2.zero;
	private float damage;
	private int lifespan;
	private int framecount;
	private bool friendlyfire;
	private float wellMass = 8000.0f;
	private float distance;
	private Vector2 gravity;
	private int playerIndex;
	private float size;
	
	//Missing private variables here
	
	public Bullet(string Image) : base(Image) {
	}

	public static Bullet Create(int BulletId, int PlayerId, float shotAngle) {
		switch(BulletId) {
		case 1 :
				return new Rocket(PlayerId);
		case 2 :
				return new GravityWell(PlayerId);
		case 3 :
				return new Flame(PlayerId, shotAngle);
		case 4 :
				return new Halo(PlayerId);
		case 5: 
				return new FatShot(PlayerId, shotAngle);
		case 6:
				return new Thrust(PlayerId, shotAngle);
		case 7:
				return new GhostBeam(PlayerId);
		case 8:
				return new NinjaStar(PlayerId);
		case 9:
				return new StormMissile(PlayerId);
		case 10:
				return new IonCannon(PlayerId);
		case 11:
				return new PlagueShot(PlayerId);
		case 12:
				return new BioBomb(PlayerId);
		case 13:
				return new AgentShot(PlayerId);
		default:
				return new Energyball(PlayerId);
		}	
	}
		
	internal class Energyball : Bullet {
		public Energyball(int player) : base("Energy_Ball.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 4f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 300.0f;
			lifespan = 50;
			damage = 10.0f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			//Other Energyball Values
		}
	}
	
	internal class Rocket : Bullet {
		public Rocket(int player) : base("Basic_Missile.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 2f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 100.0f;
			lifespan = 300;
			damage = 50.0f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = (angle * 180.0f / Mathf.PI);
			//Other Energyball Values
		}
	}
	
	internal class GravityWell : Bullet {
		public GravityWell(int player) : base("Gravity_Well_2.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 16f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 100.0f;
			lifespan = 300;
			damage = 50.0f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = (angle * 180.0f / Mathf.PI);
		}
		
		override public void Update(){
			
			Move();
			//x += velocity.x * Time.deltaTime;
			//y += velocity.y * Time.deltaTime;
			
			if(framecount > 75){
				velocity *= 0.0f;
			}
			
			//Apply gravity if in range
			for(int i=0; i<InGamePage.Ships.Count; i++) {
				playerIndex =InGamePage.Ships[i].getPlayer() - 1;
				Ship ship = InGamePage.Ships[playerIndex];
				String shipType = ship.getShipType();
				if(shipType.Equals("Demon")){
					continue;
				}
				distance = Mathf.Sqrt((x - ship.x) * (x - ship.x) +
						(y - ship.y) * (y - ship.y));
				if( distance < 1500.0f && distance > 10.0f) {				
					angle = Mathf.Atan2(ship.PosY() - y,ship.PosX() - x);
					angle -= Mathf.PI / 2;	
					angle *= -1;	
					gravity = new Vector2(Mathf.Sin(angle) * wellMass * Time.deltaTime, 
						Mathf.Cos(angle) * wellMass * Time.deltaTime);
					gravity.Normalize();
					gravity *= (wellMass /(distance*distance));			
					ship.velocity -= gravity;
				}
			}
			
			for(int i=0; i<InGamePage.Bullets.Count; i++) {
				Bullet bullet = InGamePage.Bullets[i];
				distance = Mathf.Sqrt((x - bullet.x) * (x - bullet.x) +
						(y - bullet.y) * (y - bullet.y));
				if( distance < 1000.0f && distance > 10.0f) {				
					angle = Mathf.Atan2(bullet.PosY() - y,bullet.PosX() - x);
					angle -= Mathf.PI / 2;	
					angle *= -1;
					gravity = new Vector2(Mathf.Sin(angle) * wellMass * Time.deltaTime, 
						Mathf.Cos(angle) * wellMass * Time.deltaTime);
					gravity.Normalize();
					gravity *= (wellMass /(distance*distance));			
					bullet.velocity -= gravity;
				}
			}
			checkFrameCount();
			
			//Screen Wrapping
			bulletScreenWrapping();
			
			rotation -= 7.1f;
			//framecount++;
		}
	}

	internal class Flame : Bullet {
		public Flame(int player, float shotAngle) : base("Fire_1.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 6f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 150.0f;
			lifespan = 18;
			damage = 4f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle() + (shotAngle * (Mathf.PI / 180f));
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = (angle * 180.0f / Mathf.PI);
			alpha = 0.5f;
			//Other Energyball Values
		}
	}

	internal class Halo : Bullet {
		public Halo(int player) : base("Halo_1.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 10f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 250.0f;
			lifespan = 100;
			damage = 20.0f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			}
		}
	
	internal class FatShot : Bullet {
		public FatShot(int player, float shotAngle) : base("Fatty_Shot.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 4f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 250.0f;
			lifespan = 30;
			damage = 15f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle() + 
				(shotAngle * (Mathf.PI / 180f));
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			}
		}
	
	internal class Thrust : Bullet {
		public Thrust(int player, float shotAngle) : base("Fatty_Thrust.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 6f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 10f;
			lifespan = 45;
			damage = 15f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle() + 
				(shotAngle * (Mathf.PI / 180f)) + (Mathf.PI);
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180f + (angle * 180.0f / Mathf.PI);
			}
		}
	
	internal class GhostBeam : Bullet {
		public GhostBeam(int player) : base("Ghost_Beam.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 5f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 400.0f;
			lifespan = 20;
			damage = 2.5f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			//Other Energyball Values
		}
		
		override public void detectShipCollision() {
			for(int i=0; i<InGamePage.Ships.Count; i++) {
				playerIndex = InGamePage.Ships[i].getPlayer() - 1;
				Ship collidingShip = InGamePage.Ships[playerIndex];
				if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
						(y - collidingShip.y) * (y - collidingShip.y)) < this.getSize() + collidingShip.Size()) {
					if((playerIndex == (player - 1) && friendlyfire) || playerIndex != (player - 1)) {
						InGamePage.Ships[player - 1].healShields((damage / 2));
						InGamePage.Ships[playerIndex].dealDamage(damage);
						this.RemoveFromContainer();
						InGamePage.Bullets.Remove(this);
					}
				}
			}
		}
	}
	
	internal class NinjaStar : Bullet {
		public NinjaStar(int player) : base("Ninja_Star.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 5f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 450.0f;
			lifespan = 50;
			damage = 20.0f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			}
		
		override public void Move() {
			x += velocity.x * Time.deltaTime;
			y += velocity.y * Time.deltaTime;
			
			rotation += 30.0f;
		}
		
		override public void detectShipCollision() {
			for(int i=0; i<InGamePage.Ships.Count; i++) {
			playerIndex =InGamePage.Ships[i].getPlayer() - 1;
			Ship collidingShip = InGamePage.Ships[playerIndex];
				if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
						(y - collidingShip.y) * (y - collidingShip.y)) < this.getSize() + collidingShip.Size()) {
					if((playerIndex == (player - 1) && friendlyfire) || playerIndex != (player - 1)) {
						if (DogfighterMain.instance.soundOn)
							FSoundManager.PlaySound("Sounds/ninjaprimaryexplosion");
						Bullet Explosion = new NinjaExplosion(this.player, x, y);
						InGamePage.instance.AddChild(Explosion);
						InGamePage.Bullets.Add(Explosion);
						this.RemoveFromContainer();
						InGamePage.Bullets.Remove(this);
					}
				}
			}
		}
	}

	internal class NinjaExplosion : Bullet {
		public NinjaExplosion(int player, float xPos, float yPos) : base("Ninja_Star_Explosion.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 10f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 0;
			lifespan = 25;
			damage = 1f;
			friendlyfire = true;
			angle = InGamePage.Ships[player - 1].Angle();
			x = xPos;
			y = yPos;
		}
		
		override public void detectShipCollision() {
			for(int i=0; i<InGamePage.Ships.Count; i++) {
				playerIndex =InGamePage.Ships[i].getPlayer() - 1;
				Ship collidingShip = InGamePage.Ships[playerIndex];
				if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
						(y - collidingShip.y) * (y - collidingShip.y)) < this.getSize() + collidingShip.Size()) {
					if((playerIndex == (player - 1) && friendlyfire) || playerIndex != (player - 1)) {
						InGamePage.Ships[playerIndex].dealDamage(damage);
					}
				}
			}	
		}
	}
	
	internal class StormMissile : Bullet {
		public StormMissile(int player) : base("Storm_Missile.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 4f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 100.0f;
			lifespan = 150;
			damage = 15f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			//velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.x = (Mathf.Sin(angle) * speed);
			//velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			velocity.y = (Mathf.Cos(angle) * speed);
			rotation = (angle * 180.0f / Mathf.PI);
			
			
			//Other Energyball Values
		}
		
		override public void Move() {
			Ship target;
			if (player == 1)
				target = InGamePage.Ships[1];
			else
				target = InGamePage.Ships[0];
			float newAngle = Mathf.Atan2(y - target.PosY(), x - target.PosX());
			newAngle -= Mathf.PI / 2;	
			newAngle *= -1;
			Vector2 homing = new Vector2(Mathf.Sin(newAngle), 
				Mathf.Cos(newAngle));
			homing.Normalize();
			
			this.velocity -= (5f * homing);
			
			x += velocity.x * Time.deltaTime;
			y += velocity.y * Time.deltaTime;
			
			rotation = -((Mathf.Atan2(velocity.y, velocity.x) * 180.0f / Mathf.PI) - 90);
		}
	}
	
	internal class IonCannon : Bullet {
		public IonCannon(int player) : base("Storm_Ion_Cannon.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 6f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 400.0f;
			lifespan = 50;
			damage = 15f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			//Other Energyball Values
		}
		
		override public void detectShipCollision() {
		for(int i=0; i<InGamePage.Ships.Count; i++) {
			playerIndex =InGamePage.Ships[i].getPlayer() - 1;
			Ship collidingShip = InGamePage.Ships[playerIndex];
			if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
					(y - collidingShip.y) * (y - collidingShip.y)) < this.getSize() + collidingShip.Size()) {
				if((playerIndex == (player - 1) && friendlyfire) || playerIndex != (player - 1)) {
					InGamePage.Ships[playerIndex].dealDamage(damage);
					InGamePage.Ships[playerIndex].inflictSlow(4f, 300);
					this.RemoveFromContainer();
					InGamePage.Bullets.Remove(this);
				}
			}
		}	
	}
	}
	
	internal class PlagueShot : Bullet {
		public PlagueShot(int player) : base("Plague_Main_Fire.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 6f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 400.0f;
			lifespan = 50;
			damage = 15f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			//Other Energyball Values
		}
		
		override public void detectShipCollision() {
			for(int i=0; i<InGamePage.Ships.Count; i++) {
				playerIndex =InGamePage.Ships[i].getPlayer() - 1;
				Ship collidingShip = InGamePage.Ships[playerIndex];
				if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
						(y - collidingShip.y) * (y - collidingShip.y)) < this.getSize() + collidingShip.Size()) {
					if((playerIndex == (player - 1) && friendlyfire) || playerIndex != (player - 1)) {
						if(InGamePage.Ships[playerIndex].isPoisoned() == 0){
							InGamePage.Ships[playerIndex].dealDamage(damage);
							this.RemoveFromContainer();
							InGamePage.Bullets.Remove(this);
						}
						else {
							InGamePage.Ships[playerIndex].dealDamage(2 * damage);
							this.RemoveFromContainer();
							InGamePage.Bullets.Remove(this);	
						}
					}
				}
			}
		}
	}
	
	internal class BioBomb : Bullet {
		public BioBomb(int player) : base("Plague_Bio_Bomb.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 8f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 200.0f;
			lifespan = 50;
			damage = 10.0f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			//Other Energyball Values
		}
		
		override public void detectShipCollision() {
			for(int i=0; i<InGamePage.Ships.Count; i++) {
			playerIndex =InGamePage.Ships[i].getPlayer() - 1;
			Ship collidingShip = InGamePage.Ships[playerIndex];
				if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
						(y - collidingShip.y) * (y - collidingShip.y)) < this.getSize() + collidingShip.Size()) {
					if((playerIndex == (player - 1) && friendlyfire) || playerIndex != (player - 1)) {
						Bullet Explosion = new BioExplosion(this.player, x, y);
						InGamePage.instance.AddChild(Explosion);
						InGamePage.Bullets.Add(Explosion);
						this.RemoveFromContainer();
						InGamePage.Bullets.Remove(this);
					}
				}
			}
		}
	}
	
	internal class BioExplosion : Bullet {
		public BioExplosion(int player, float xPos, float yPos) : base("Plague_Bio_Explosion.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 4f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 0;
			lifespan = 50;
			damage = 0f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = xPos;
			y = yPos;
		}
		
		override public void detectShipCollision() {
		for(int i=0; i<InGamePage.Ships.Count; i++) {
			playerIndex =InGamePage.Ships[i].getPlayer() - 1;
			Ship collidingShip = InGamePage.Ships[playerIndex];
			if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
					(y - collidingShip.y) * (y - collidingShip.y)) < this.getSize() + collidingShip.Size()) {
				if((playerIndex == (player - 1) && friendlyfire) || playerIndex != (player - 1)) {
					InGamePage.Ships[playerIndex].inflictPoison(0.05f, 800);
				}
			}
		}	
	}
	}
	
	internal class AgentShot : Bullet {
		public AgentShot(int player) : base("Agent_Primary.png") {
			this.player = player;
			owningShip = InGamePage.Ships[player - 1];
			size = 4f;
			scaleX = size / 30f;
			scaleY = size / 30f;
			speed = 500.0f;
			lifespan = 25;
			damage = 25.0f;
			friendlyfire = false;
			angle = InGamePage.Ships[player - 1].Angle();
			x = (owningShip.PosX() + (Mathf.Sin(angle) * owningShip.Size()));
			y = (owningShip.PosY() + (Mathf.Cos(angle) * owningShip.Size()));
			velocity.x = (Mathf.Sin(angle) * speed) + owningShip.XVelocity();
			velocity.y = (Mathf.Cos(angle) * speed) + owningShip.YVelocity();
			rotation = 180.0f + (angle * 180.0f / Mathf.PI);
			//Other Energyball Values
		}
	}
	//Other Bullet Types
	
	public virtual void Update() {

		Move();

		bulletScreenWrapping();
		
		checkFrameCount();

		//Collision with ship
		detectShipCollision();
		
		//Collision with asteroid
		detectAsteroidCollision();

	}
	
	public virtual void Move() {
		//Movement
		x += velocity.x * Time.deltaTime;
		y += velocity.y * Time.deltaTime;	
	}
	
	public virtual void bulletScreenWrapping() {
		//Screen Wrapping
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
	
	public virtual void checkFrameCount() {
		this.framecount++;
		if(framecount == lifespan) {
			this.RemoveFromContainer();
			InGamePage.Bullets.Remove(this);
		}
	}
	
	public virtual void detectShipCollision() {
		for(int i=0; i<InGamePage.Ships.Count; i++) {
			playerIndex =InGamePage.Ships[i].getPlayer() - 1;
			Ship collidingShip = InGamePage.Ships[playerIndex];
			if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
					(y - collidingShip.y) * (y - collidingShip.y)) < this.getSize() + collidingShip.Size()) {
				if((playerIndex == (player - 1) && friendlyfire) || playerIndex != (player - 1)) {
					InGamePage.Ships[playerIndex].dealDamage(damage);
					this.RemoveFromContainer();
					InGamePage.Bullets.Remove(this);
				}
			}
		}	
	}
	
	public virtual void detectAsteroidCollision() {
		for(int i=0; i<InGamePage.Asteroids.Count; i++) {
			Asteroid collidingAsteroid = InGamePage.Asteroids[i];
			if(Mathf.Sqrt((x - collidingAsteroid.x) * (x - collidingAsteroid.x) +
					(y - collidingAsteroid.y) * (y - collidingAsteroid.y)) 
						< this.getSize() + collidingAsteroid.Size()) {
				InGamePage.Asteroids[i].dealDamage(damage);
				this.RemoveFromContainer();
				InGamePage.Bullets.Remove(this);
			}
		}
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
	
	public void setAngle(float newAngle) {
		angle = newAngle;	
	}
	
	public float getSize() {
		return size;	
	}
	
}