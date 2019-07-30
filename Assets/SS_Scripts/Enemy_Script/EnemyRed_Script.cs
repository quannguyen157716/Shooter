using UnityEngine;
using System.Collections;

public class EnemyRed_Script : MonoBehaviour 
{

	//Public Var
	public float speed;						//Enemy Ship Speed
	public int health;						//Enemy Ship Health
	public int ScoreValue;					//How much the Enemy Ship give score after explosion
	public float fireRate = 0.5F;			//Fire Rate between Shots

	//Private Var
	private float nextFire = 0.0F;			//First fire & Next fire Time
	int currentHealth;
	public GameObject LaserHit;		//LaserGreenHit Prefab
	public GameObject Explosion;			//Explosion Prefab
	
	public GameObject shot;					//Fire Prefab
	public Transform shotSpawn;				//Where the Fire Spawn
	Rigidbody2D rigidbody2;
	AudioSource audio2;
	//
	// Use this for initialization
	void Start () 
	{
		currentHealth=health;
		rigidbody2=GetComponent<Rigidbody2D>();
		audio2=GetComponent<AudioSource>();
		StartCoroutine(Move());
			//Enemy Ship Movement
	}

	// Update is called once per frame
	void Update () 
	{
		/* //Excute When the Current Time is bigger than the nextFire time
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate; 									//Increment nextFire time with the current system time + fireRate
			Instantiate (shot , shotSpawn.position ,shotSpawn.rotation); 		//Instantiate fire shot 
			audio2.Play ();														//Play Fire sound
		}*/
		Fire2();
	}

	IEnumerator Move()
	{
		rigidbody2.velocity = -1 * transform.up * speed;
		yield return new WaitForSeconds (Random.Range(1,3));
		rigidbody2.velocity =Vector2.zero;
		yield return new WaitForSeconds (Random.Range(1,5));
		rigidbody2.velocity = -1 * transform.up * speed;
	}
	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
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

	void Fire2()
	{
		if(Time.time >nextFire)
		{
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			shot = ObjectPooler.ObjectPoolerInstance.GetPooledObject(shot.tag, shotSpawn.transform.position); 
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
