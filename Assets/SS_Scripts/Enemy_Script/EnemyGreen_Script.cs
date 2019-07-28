using UnityEngine;
using System.Collections;

public class EnemyGreen_Script : MonoBehaviour 
{

	//Public Var
	public float speed;						//Enemy Ship Speed
	public int health;						//Enemy Ship Health
	public int ScoreValue; 					//How much the Enemy Ship give score after explosion
	public float fireRate = 1F;				//Fire Rate between Shots
	private float nextFire = 0.0F; 			//First fire & Next fire Time
	public GameObject LaserHit;		//LaserGreenHit Prefab
	public GameObject Explosion;			//Explosion Prefab
	public GameObject shot; 				//Fire Prefab
	public Transform shotSpawn;				//Where the Fire Spawn
	int currentHealth;
	//Private Var
	Rigidbody2D rigidbody2;
	AudioSource audio2;

	// Use this for initialization
	void OnEnable () 
	{
		currentHealth=health;
		rigidbody2=GetComponent<Rigidbody2D>();
		audio2=GetComponent<AudioSource>();
		rigidbody2.velocity = -1 * transform.up * speed;	//Enemy Ship Movement
	}

	// Update is called once per frame
	void Update () 
	{
		Fire2();
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
			TakeDamage(PlayerGun.PlayerGunInstance.shotDamage);													//Decrement Health by 1
			
			//Check the Health if less or equal 0
			if(currentHealth <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 			//Instantiate Explosion							
				Destruct();												
			}
		}
	}

	void Fire()
	{

		if(Time.time >nextFire)
		{Debug.Log("Fire");
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			shot = EnemyBulletPool.SharedEnemyBulletPool.GetPooledObject("Fireball"); 
  			if (shot != null) 
			{
				
   			shot.transform.position = shotSpawn.transform.position;
    		shot.transform.rotation = shotSpawn.transform.rotation;
    		shot.SetActive(true);
			audio2.Play();
			}
			else
			return;
		}
	}

	void Fire2()
	{
		if(Time.time >nextFire)
		{
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			shot = ObjectPooler.ObjectPoolerInstance.GetPooledObject("Fireball", shotSpawn.transform.position); 
  			if (shot != null) 
			audio2.Play();
			else
			return;
		}
	}

	void TakeDamage(int damage)
	{
		currentHealth-=damage;
	}

	void Destruct()
	{
		SharedValues_Script.score +=ScoreValue; //Increment score by ScoreValue 
		gameObject.SetActive(false);//return to pool
	}
}
