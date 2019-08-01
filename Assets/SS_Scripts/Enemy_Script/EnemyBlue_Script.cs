using UnityEngine;
using System.Collections;
public class Enemy
{
	public float speed; //Enemy Ship Speed
	public int health; //Enemy Ship Health
	public int ScoreValue; //How much the Enemy Ship give score after explosion
}
public class EnemyBlue_Script : MonoBehaviour 
{
	//Public Var
	public float speed; //Enemy Ship Speed
	public int health; //Enemy Ship Health
	int currentHealth;
	public int ScoreValue; //How much the Enemy Ship give score after explosion
	public GameObject LaserHit; //LaserGreenHit Prefab
	public GameObject Explosion; //Explosion Prefab
	public Rigidbody2D rigidbody2;
	
	bool Instream=false;
	// Use this for initialization
	void OnEnable () 
	{
		currentHealth=health;
		//rigidbody2=GetComponent<Rigidbody2D>();   
		Move();
	}



	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "PlayerRegularShot" || other.tag=="PlayerBurstShot" ||other.tag=="TracingHead")
		{
			Instantiate (LaserHit, transform.position , transform.rotation); 			//Instantiate LaserGreenHit 
			//Destroy(other.gameObject); 														//Destroy the Other (PlayerLaser)
			other.gameObject.SetActive(false);
			//Check the Health if greater than 0
			if(currentHealth > 0)
			TakeDamage(PlayerGun.PlayerGunInstance.shotDamage);	//Decrement Health by 1
			
			//Check the Health if less or equal 0
			if(currentHealth <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 			//Instantiate Explosion							
				Destruct();												
			}
		}
	}

	void Move()
	{
		rigidbody2.velocity = -1 * transform.up * speed; //Enemy Ship Movement
	}
	void TakeDamage(int damage)
	{
		currentHealth-=damage;
		//Debug.Log(currentHealth);
	}

	void Destruct()
	{
		SharedValues_Script.score +=ScoreValue; //Increment score by ScoreValue 
		gameObject.SetActive(false);//return to pool
	}
}