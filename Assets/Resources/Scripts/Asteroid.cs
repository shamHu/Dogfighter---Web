using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Asteroid : FSprite {
	
	private const float COR = 0.8f;
	private float mass;
	private float size;
	private int spawnWall; //0 == top, 1 == right, 2 == bottom, 3 == left
	public Vector2 velocity = Vector2.zero;
	private float health;
	
	public Asteroid() : base("Asteroid_1.png") {
		
		size = (UnityEngine.Random.Range(1.0f, 3.0f));
		mass = size;
		health = (int) (size * 10f);
		
		this.scaleX = size / 35f;
		this.scaleY = size / 35f;
		
		spawnWall = UnityEngine.Random.Range(0, 4);
		
		//Random Spawning
		switch(spawnWall) {
		case 0:
			this.x = UnityEngine.Random.Range(-Futile.screen.halfWidth, 
				Futile.screen.halfWidth);
			this.y = Futile.screen.halfHeight;
			velocity = new Vector2(UnityEngine.Random.Range(-300.0f, 400.0f), 
				UnityEngine.Random.Range(-300.0f, -100.0f));
			break;
		case 1:
			this.x = Futile.screen.halfWidth;
			this.y = UnityEngine.Random.Range(-Futile.screen.halfHeight, 
				Futile.screen.halfHeight);
			velocity = new Vector2(UnityEngine.Random.Range(-300f, -100f), 
				UnityEngine.Random.Range(-300f, 300f));
			break;
		case 2:
			this.x = UnityEngine.Random.Range(-Futile.screen.halfWidth, 
				Futile.screen.halfWidth);;
			this.y = -Futile.screen.halfHeight;
			velocity = new Vector2(UnityEngine.Random.Range(-300f, 300f),
				UnityEngine.Random.Range (300f, 100f));
			break;
		case 3:
			this.x = -Futile.screen.halfWidth;
			this.y = UnityEngine.Random.Range(-Futile.screen.halfHeight, 
				Futile.screen.halfHeight);
			velocity = new Vector2(UnityEngine.Random.Range(300f, 100f),
				UnityEngine.Random.Range (-300f, 300f));
			break;
		default: 
			this.x = 0.0f;
			this.y = 0.0f;
			break;
		}
	}
	
	//Unrandomized spawning
	public Asteroid(float asteroidSize, float posX, float posY, 
		Vector2 asteroidVelocity) : base("Gravity_Well_2.png") {
		
		size = asteroidSize;
		mass = size;
		health = (int) (size * 10f);
		
		this.x = posX;
		this.y = posY;
		
		this.scaleX = size / 50f;
		this.scaleY = size / 50f;
		
		velocity = asteroidVelocity;
		
	}
	
	public void Update()
	{
		int playerIndex;
		
		//Collision Detection
		for(int i = 0; i < InGamePage.Ships.Count; i++){
			playerIndex = InGamePage.Ships[i].getPlayer() - 1;
			Ship collidingShip = InGamePage.Ships[playerIndex];
			
			if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
					(y - collidingShip.y) * (y - collidingShip.y)) > (this.Size()+collidingShip.Size())) {
				collidingShip.isNotCollidingAsteroid = true;
			}
						
			if(Mathf.Sqrt((x - collidingShip.x) * (x - collidingShip.x) +
					(y - collidingShip.y) * (y - collidingShip.y)) < (this.Size() + collidingShip.Size())) {
				if(collidingShip.IsNotCollidingAsteroid()) {
					
					float xVelocity2 = (COR*collidingShip.Mass()*(velocity.x -
						collidingShip.XVelocity()) + (mass * velocity.x) + (collidingShip.Mass() * 
						collidingShip.XVelocity()))/(mass + collidingShip.Mass());
					
					float yVelocity2 = (COR*collidingShip.Mass()*(velocity.y -
						collidingShip.YVelocity()) + (mass * velocity.y) + (collidingShip.Mass() * 
						collidingShip.YVelocity()))/(mass + collidingShip.Mass());
					
					float xVelocity1 = (COR*mass*(collidingShip.XVelocity() -
						velocity.x) + (collidingShip.Mass() * collidingShip.XVelocity()) + (mass * 
						velocity.x))/(collidingShip.Mass() + mass);
					
					float yVelocity1 = (COR*mass*(collidingShip.YVelocity() -
						velocity.y) + (collidingShip.Mass() * collidingShip.YVelocity()) + (mass * 
						velocity.y))/(collidingShip.Mass() + mass);
					
					Vector2 curVel = new Vector2(xVelocity1, yVelocity1);
					Vector2 curVel2 = new Vector2(xVelocity2, yVelocity2);
					velocity = curVel;
					collidingShip.velocity = curVel2/2;
					collidingShip.isNotCollidingAsteroid = false;
					//collidingShip.dealDamage(2.0f);
				}
			}
			
		}
		
		//Movement
		x += velocity.x * Time.deltaTime;
		y += velocity.y * Time.deltaTime;
		
		//Removing asteroids once they exit the field
		if(x < -Futile.screen.halfWidth || x > Futile.screen.halfWidth
			|| y < -Futile.screen.halfHeight || y > Futile.screen.halfHeight) {
			this.RemoveFromContainer();
			InGamePage.Asteroids.Remove(this);
		}
	}
	
	public void dealDamage(float damage) {
		health -= damage;
		if (health <= 0)
			destroyAsteroid();
	}
	
	public void destroyAsteroid() {
		this.RemoveFromContainer();
		InGamePage.Asteroids.Remove(this);
	}
	
	public float Mass(){
		return mass;
	}
	
	public float Size(){
		return size;
	}
	
}