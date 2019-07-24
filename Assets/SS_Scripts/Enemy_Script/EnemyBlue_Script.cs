using UnityEngine;
using System.Collections;

public class EnemyBlue_Script : MonoBehaviour 
{
	//Public Var
	public float speed; //Enemy Ship Speed
	public int health; //Enemy Ship Health
	int currentHealth;
	public int ScoreValue; //How much the Enemy Ship give score after explosion
	public GameObject LaserGreenHit; //LaserGreenHit Prefab
	public GameObject Explosion; //Explosion Prefab
	
	public Rigidbody2D rigidbody2;
	
	// Use this for initialization
	void OnEnable () 
	{
		currentHealth=health;
		//rigidbody2=GetComponent<Rigidbody2D>();      
		rigidbody2.velocity = -1 * transform.up * speed; //Enemy Ship Movement
	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "PlayerRegularShot" || other.tag=="PlayerBurstShot" ||other.tag=="TracingHead")
		{
			Instantiate (LaserGreenHit, transform.position , transform.rotation); 			//Instantiate LaserGreenHit 
			//Destroy(other.gameObject); 														//Destroy the Other (PlayerLaser)
			other.gameObject.SetActive(false);
			//Check the Health if greater than 0
			if(currentHealth > 0)
			currentHealth-=DataController_Script.playerDamage; 																	//Decrement Health by 1
			
			//Check the Health if less or equal 0
			if(currentHealth <= 0)
			{
				Debug.Log(currentHealth); 
				Instantiate (Explosion, transform.position , transform.rotation); 			//Instantiate Explosion
				SharedValues_Script.score +=ScoreValue; 									//Increment score by ScoreValue
				//Destroy(gameObject);														//Destroy The Object (Enemy Ship)
				gameObject.SetActive(false);//return to pool
			}
		}
	}

	void TakeDamage(string playerShot)
	{
		
	}
}