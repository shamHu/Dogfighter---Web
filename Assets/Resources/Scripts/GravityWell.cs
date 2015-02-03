using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GravityWell : FSprite {
	
	private float angle;
	private float bodyMass = 15000.0f;
	float distance;
	private Vector2 gravity;
	
	public GravityWell() : base("Gravity_Well_1.png") {
		
		this.scaleX = 0.45f;
		this.scaleY = 0.45f;

	/*public static Bullet Create(int BulletId, int PlayerId) {
		switch(BulletId) {
		case 2 :
				return new Rocket(PlayerId);
		default:
			return new Energyball(PlayerId);
		}*/
	}
	
	public void Update() {
		int playerIndex;
		
		//Apply gravity if in range
		for(int i=0; i<InGamePage.Ships.Count; i++) {
			playerIndex =InGamePage.Ships[i].getPlayer() - 1;
			Ship ship = InGamePage.Ships[playerIndex];
			distance = Mathf.Sqrt((x - ship.x) * (x - ship.x) +
					(y - ship.y) * (y - ship.y));
			if( distance < 1000.0f) {				
				angle = Mathf.Atan2(ship.PosY() - y,ship.PosX() - x);
				angle -= Mathf.PI / 2;	
				angle *= -1;	
				gravity = new Vector2(Mathf.Sin(angle) * bodyMass * Time.deltaTime, 
					Mathf.Cos(angle) * bodyMass * Time.deltaTime);
				gravity.Normalize();
				gravity *= (bodyMass /(distance*distance));			
				ship.velocity -= gravity;
			}
			Debug.Log(ship.velocity.magnitude);
			Debug.Log(distance);
		}
		
		for(int i=0; i<InGamePage.Bullets.Count; i++) {
			Bullet bullet = InGamePage.Bullets[i];
			distance = Mathf.Sqrt((x - bullet.x) * (x - bullet.x) +
					(y - bullet.y) * (y - bullet.y));
			if( distance < 1000.0f) {				
				angle = Mathf.Atan2(bullet.PosY() - y,bullet.PosX() - x);
				angle -= Mathf.PI / 2;	
				angle *= -1;
				gravity = new Vector2(Mathf.Sin(angle) * bodyMass * Time.deltaTime, 
					Mathf.Cos(angle) * bodyMass * Time.deltaTime);
				gravity.Normalize();
				gravity *= (bodyMass /(distance*distance));			
				bullet.velocity -= gravity;
			}
		}
		rotation -= 1.1f;
	}
}