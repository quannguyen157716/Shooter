using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_RegularShot : MonoBehaviour {
	public float speed; //EnemyRed Shot Speed
	Rigidbody2D rigidbody2;

	// Use this for initialization
	void Start ()
	{
		rigidbody2=GetComponent<Rigidbody2D>();
		rigidbody2.velocity = -1 * transform.up * speed; //Give Velocity to the Enemy ship shot
	}
}
