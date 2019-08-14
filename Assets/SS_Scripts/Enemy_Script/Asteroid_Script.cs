using UnityEngine;

public class Asteroid_Script : MonoBehaviour 
{
	//Maximum Speed of the angular velocity
	public float maxTumble; 			
	//Minimum Speed of the angular velocity
	public float minTumble; 	
	//Asteroid Speed		
	public float speed; 	
	//Asteroid Health 		
	public int health; 				
	//LaserGreenHit Prefab	
	public GameObject LaserHit; 	
	//Explosion Prefab
	public GameObject Explosion; 		
	//How much the Asteroid give score after explosion 
	public int ScoreValue; 				
	public Rigidbody2D rigidbody2;

	int currentHealth;
	
	void Awake()
	{
		Load();
	}

	void OnEnable() 
	{
		//Angular movement based on random speed values
		rigidbody2.angularVelocity = Random.Range(minTumble, maxTumble); 		
		rigidbody2.velocity = -1 * transform.up * speed; 
		currentHealth=health;						
	}

	void Load()
	{
		//Debug.Log(EnemyCommander.EnemyCommanderInstance.EDictionary[gameObject.tag]);
		EnemyInfo info=EnemyCommander.EnemyCommanderInstance.EDictionary[gameObject.tag];
		speed=info.speed;
		health=info.health;
		ScoreValue=info.ScoreValue;
	}

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
}