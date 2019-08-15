using UnityEngine;
using System.Collections;

public class EnemyShot_Script : MonoBehaviour 
{
	public float speed; //EnemyRed Shot Speed
	
	public Rigidbody2D rigidbody2;

	// Use this for initialization
	void OnEnable ()
	{
		//rigidbody2=GetComponent<Rigidbody2D>();
		rigidbody2.velocity = -1 * transform.up * speed; //Give Velocity to the Enemy ship shot
	}
}
