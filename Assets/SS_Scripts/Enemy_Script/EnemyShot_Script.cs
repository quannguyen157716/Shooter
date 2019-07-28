using UnityEngine;
using System.Collections;

public class EnemyShot_Script : MonoBehaviour 
{
	//making more shot shot data and enemy data, behaviors and behaviors data
	//Public Var
	public float speed; //EnemyRed Shot Speed
	
	public Rigidbody2D rigidbody2;

	// Use this for initialization
	void Start ()
	{
		//rigidbody2=GetComponent<Rigidbody2D>();
		rigidbody2.velocity = -1 * transform.up * speed; //Give Velocity to the Enemy ship shot
	}
}
