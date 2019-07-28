using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimShot : MonoBehaviour {

	//making more shot shot data and enemy data, behaviors and behaviors data
	//Public Var
	public float speed; //EnemyRed Shot Speed
	public Rigidbody2D rigidbody2;
	Vector2 target;

	// Use this for initialization
	void OnEnable ()
	{
		//rigidbody2=GetComponent<Rigidbody2D>();
		//rigidbody2.velocity = -1 * transform.up * speed; //Give Velocity to the Enemy ship shot
		target=(Player_Script.PlayerInstance.transform.position-transform.position);
		target.Normalize();
		rigidbody2.velocity=target*speed;
		
	}


}
