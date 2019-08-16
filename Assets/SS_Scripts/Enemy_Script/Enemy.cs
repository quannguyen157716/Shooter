using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy: MonoBehaviour
{
	public float speed; 
	public int health; 
	protected int currentHealth;
	public int ScoreValue; 
	public GameObject LaserHit; 
	public GameObject Explosion;  
	public Rigidbody2D rigidbody2;
	public AudioSource audio2;
	public FollowAPath path;
	public SubBehavior behavior;

	//detect player shot when get hit
	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == PlayerGun.PlayerGunInstance.shotDealDamage)
		{
			//get shot from pool to fire                                                                                       
			ObjectPooler.ObjectPoolerInstance.GetPooledObject(LaserHit.tag, transform.position,true);
			//Destroy the Other (PlayerShot)
			other.gameObject.SetActive(false);
			
			//Check the Health 
			if(currentHealth > 0)
			TakeDamage(PlayerGun.PlayerGunInstance.shotDamage);													
			
			//Check the Health if less or equal 0
			if(currentHealth <= 0)
			{
				//Instantiate explosion
				Instantiate (Explosion, transform.position , transform.rotation); 							
				Destruct();												
			}
		}
	}
	public void TakeDamage(int damage)
	{
		currentHealth-=damage;
	}
	public void Destruct()
	{
		SharedValues_Script.score +=ScoreValue; //Increment score by ScoreValue 
		gameObject.SetActive(false);//return to pool  
	}
}
