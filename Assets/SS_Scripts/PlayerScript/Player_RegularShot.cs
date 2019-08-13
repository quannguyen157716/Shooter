using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RegularShot : MonoBehaviour 
{
	public int damage;
	public float speed; 
	public Rigidbody2D rigidbody2;
	
	void OnEnable()
	{
		//Give Velocity to the Player ship shot
		rigidbody2.velocity = transform.up * speed; 
	}

	
}
