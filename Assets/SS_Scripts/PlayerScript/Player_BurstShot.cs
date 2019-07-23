using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BurstShot : MonoBehaviour {
	public int damage;
	public float speed; //Speed of the velocity 
	public Rigidbody2D rigidbody2;
	
	void OnEnable()
	{
		DataController_Script.playerDamage=damage;
		//rigidbody2=GetComponent<Rigidbody2D>();
		rigidbody2.angularVelocity = 30f;
		rigidbody2.velocity = transform.up * speed; //Give Velocity to the Player ship shot
	}
}
