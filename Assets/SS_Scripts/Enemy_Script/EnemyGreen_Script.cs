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
	public Rigidbody2D rigidbody2;
	public AudioSource audio2;

	public FollowAPath path;
	public SubBehavior behavior;

	void Awake()
	{
		Load();
	}

	void OnEnable () 
	{
		currentHealth=health;
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

	void Load()
	{
		EnemyInfo info=EnemyCommander.EnemyCommanderInstance.EDictionary[gameObject.tag];
		speed=info.speed;
		health=info.health;
		ScoreValue=info.ScoreValue;
		fireRate=info.fireRate;
	}

	void Update () 
	{
		Fire2();
	}

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == PlayerGun.PlayerGunInstance.shotDealDamage)
		{
			ObjectPooler.ObjectPoolerInstance.GetPooledObject(LaserHit.tag, transform.position,true);
			//Instantiate (LaserHit, transform.position , transform.rotation); 			//Instantiate LaserGreenHit 
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
			shot = ObjectPooler.ObjectPoolerInstance.GetPooledObject(shot.tag, shotSpawn.transform.position,true); 
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
