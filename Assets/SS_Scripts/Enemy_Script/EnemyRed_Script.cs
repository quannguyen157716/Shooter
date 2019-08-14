using UnityEngine;
using System.Collections;

public class EnemyRed_Script : Enemy
{
	/* 
	public float speed;						//Enemy Ship Speed
	public int health;						//Enemy Ship Health
	public int ScoreValue;					//How much the Enemy Ship give score after explosion
	int currentHealth;
	public GameObject LaserHit;		//LaserGreenHit Prefab
	public GameObject Explosion;			//Explosion Prefab
	public Rigidbody2D rigidbody2;
	public AudioSource audio2;
	public FollowAPath path;
	public SubBehavior behavior;
	*/

	//Time between Shots
	public float fireRate = 0.5F;			
	//Time to shot next
	private float nextFire = 0.0F;			
	//Shot Prefab
	public GameObject shot;					
	//Where the Fire Spawn
	public Transform shotSpawn;				
	
	void Awake()
	{
		Load();
	}

	void Load()
	{
		EnemyInfo info=EnemyCommander.EnemyCommanderInstance.EDictionary[gameObject.tag];
		speed=info.speed;
		health=info.health;
		ScoreValue=info.ScoreValue;
		fireRate=info.fireRate;
	}

	void OnEnable () 
	{
		currentHealth=health;
		if(path.inPath.InStream)
		{
			//StartCoroutine(GoByPath(inPath.pathToGo));
			path.FollowPath(speed);
		}
		else
		{
			behavior.Patrol(rigidbody2,Random.Range(3f,4f),speed, 60);
		}
		path.inPath.InStream=false;
	}

	// Update is called once per frame
	void Update () 
	{
		Fire();
	}

	void Fire()
	{
		if(Time.time >nextFire)
		{
			nextFire = Time.time + fireRate;
			shot = ObjectPooler.ObjectPoolerInstance.GetPooledObject(shot.tag, shotSpawn.transform.position,true); 
			if (shot != null) 
			audio2.Play();
			else
			return;
		}
	}
	/* 
	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == PlayerGun.PlayerGunInstance.shotDealDamage)
		{
			//get shot from pool to fire
			ObjectPooler.ObjectPoolerInstance.GetPooledObject(LaserHit.tag, transform.position,true);
			//Destroy the Other (PlayerLaser)
			other.gameObject.SetActive(false);
			//Check the Health if greater than 0

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

	
	
	void TakeDamage(int damage)
	{
		currentHealth-=damage;
	}

	void Destruct()
	{
		//Increment score by ScoreValue 
		SharedValues_Script.score +=ScoreValue; 
		//return to pool
		gameObject.SetActive(false);
	}
	*/
}
