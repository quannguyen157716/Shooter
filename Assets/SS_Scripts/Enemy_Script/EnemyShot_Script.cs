using UnityEngine;
using System.Collections;

public class EnemyShot_Script : MonoBehaviour 
{
	public float speed; //EnemyRed Shot Speed
	
	public Rigidbody2D rigidbody2;
	Vector2 destination;
	// Use this for initialization
	void OnEnable ()
	{
		destination.x=transform.position.x;
		destination.y=transform.position.y-20;
		//rigidbody2=GetComponent<Rigidbody2D>();
		rigidbody2.velocity = -1 * transform.up * speed; //Give Velocity to the Enemy ship shot
	}
}
