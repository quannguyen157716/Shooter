using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TracingShot : MonoBehaviour {
	Transform Target;
	public int damage;
	public float speed; //Speed of the velocity 
	public Rigidbody2D rigidbody2;
	// Use this for initialization
	void OnEnable () {
		if(!transform.GetChild(0).gameObject.activeInHierarchy)
		transform.GetChild(0).gameObject.SetActive(true);
//		PlayerGun.PlayerGunInstance.shotDamage=damage;
	}
	
	// Update is called once per frame 
	void Update () {
		if(!transform.GetChild(0).gameObject.activeInHierarchy)
		gameObject.SetActive(false);
		if(Target==null)
		{
			rigidbody2.velocity=transform.up *speed;
			return;
		}
		Vector2 direction =(Vector2)Target.position-rigidbody2.position;
		//Debug.Log("Before: "+ direction);
		direction.Normalize();
		//Debug.Log("After: "+direction);
		float rotateAmount=Vector3.Cross(direction,transform.up).z;
		rigidbody2.angularVelocity=-rotateAmount*200f;
		rigidbody2.velocity=transform.up *speed;
		
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag=="EnemyBlue" || other.tag=="Asteroid")
		Target=other.gameObject.transform;
	}
}
