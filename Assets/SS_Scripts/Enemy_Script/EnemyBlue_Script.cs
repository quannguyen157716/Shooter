using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyBlue_Script : Enemy
{
	/* 
	public float speed; //Enemy Ship Speed
	public int health; //Enemy Ship Health
	int currentHealth;
	public int ScoreValue; //How much the Enemy Ship give score after explosion 
	public GameObject LaserHit; //LaserGreenHit Prefab
	public GameObject Explosion; //Explosion Prefab 
	public Rigidbody2D rigidbody2;
	
	public FollowAPath path;
	public SubBehavior behavior;
	*/

	void Awake()
	{
		Load();
	}

	void OnEnable () 
	{
		currentHealth=health;
		if(path.inPath.InStream)
		{
			path.FollowPath(speed);
		}
		else
		{
			//Debug.Log("Move");
			behavior.MoveStraight(rigidbody2, speed);
		}
		path.inPath.InStream=false;
	}

	//Load initial attribute
	void Load()
	{
		//Debug.Log(EnemyCommander.EnemyCommanderInstance.EDictionary[gameObject.tag]); 
		EnemyInfo info=EnemyCommander.EnemyCommanderInstance.EDictionary[gameObject.tag];
		speed=info.speed;
		health=info.health;
		ScoreValue=info.ScoreValue;
	}
	/* 
	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == PlayerGun.PlayerGunInstance.shotDealDamage)
		{
			ObjectPooler.ObjectPoolerInstance.GetPooledObject(LaserHit.tag, transform.position,true);
			other.gameObject.SetActive(false);
		
			if(currentHealth > 0)
			TakeDamage(PlayerGun.PlayerGunInstance.shotDamage);	

			//Check the Health if less or equal 0
			if(currentHealth <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 									
				Destruct();												
			}
		}
	}
	
	void TakeDamage(int damage)
	{
		currentHealth-=damage;
		//Debug.Log(currentHealth);
	}

	void Destruct()
	{
		SharedValues_Script.score +=ScoreValue; 
		gameObject.SetActive(false);
	}
	*/
}