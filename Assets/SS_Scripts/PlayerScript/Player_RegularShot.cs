using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RegularShot : MonoBehaviour {
	public int damage;
	public float speed; //Speed of the velocity 
	Rigidbody2D rigidbody2;
	
	void OnEnable()
	{
		rigidbody2=GetComponent<Rigidbody2D>();
		rigidbody2.velocity = transform.up * speed; //Give Velocity to the Player ship shot
	}

	
}
