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
	public GameObject LaserGreenHit;		//LaserGreenHit Prefab
	public GameObject Explosion;			//Explosion Prefab
	public GameObject shot; 				//Fire Prefab
	public Transform shotSpawn;				//Where the Fire Spawn
	
	//Private Var
	Rigidbody2D rigidbody2;
	AudioSource audio2;

	// Use this for initialization
	void OnEnable () 
	{
		rigidbody2=GetComponent<Rigidbody2D>();
		audio2=GetComponent<AudioSource>();
		rigidbody2.velocity = -1 * transform.up * speed;	//Enemy Ship Movement
	}

	// Update is called once per frame
	void Update () 
	{
		//Excute When the Current Time is bigger than the nextFire time
		if (Time.time > nextFire)
		{
			nextFire = Time.time + fireRate; 									//Increment nextFire time with the current system time + fireRate
			Instantiate (shot , shotSpawn.position ,shotSpawn.rotation); 		//Instantiate fire shot 
			audio2.Play (); 														//Play Fire sound
		}
	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == "PlayerRegularShot" || other.tag=="PlayerBurstShot" ||other.tag=="TracingHead")
		{
			Instantiate (LaserGreenHit, transform.position , transform.rotation); 		//Instantiate LaserGreenHit 
			//Destroy(other.gameObject); 													//Destroy the Other (PlayerLaser)
			other.gameObject.SetActive(false); //return to pool
			//Check the Health if greater than 0
			if(health > 0)
				health--; 																//Decrement Health by 1

			//Check the Health if less or equal 0
			if(health <= 0)
			{
				Instantiate (Explosion, transform.position , transform.rotation); 		//Instantiate Explosion
				SharedValues_Script.score +=ScoreValue; 								//Increment score by ScoreValue
				//Destroy(gameObject); 													//Destroy The Object (Enemy Ship)
				gameObject.SetActive(false); //return to pool
			}
		}
		
	}
}
