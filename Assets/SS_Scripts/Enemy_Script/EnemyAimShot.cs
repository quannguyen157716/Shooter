using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAimShot : MonoBehaviour 
{
	public float speed; 
	public Rigidbody2D rigidbody2;
	Vector2 target;

	// Use this for initialization
	void OnEnable ()
	{
		//rigidbody2=GetComponent<Rigidbody2D>();
		//rigidbody2.velocity = -1 * transform.up * speed; //Give Velocity to the Enemy ship shot
		try
		{
			target=(Player_Script.PlayerInstance.transform.position-transform.position);
		}
		catch(System.NullReferenceException)
		{
			rigidbody2.velocity = -1 * transform.up * speed;
			return;
		}
		target.Normalize();
		rigidbody2.velocity=target*speed;
	}
}
