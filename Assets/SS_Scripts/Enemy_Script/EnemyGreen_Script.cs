using UnityEngine;
using System.Collections;

public class EnemyGreen_Script : Enemy
{
	/* 
	public float speed;						//Enemy Ship Speed
	public int health;						//Enemy Ship Health
	public int ScoreValue; 					//How much the Enemy Ship give score after explosion
	public GameObject LaserHit;		//LaserGreenHit Prefab
	public GameObject Explosion;			//Explosion Prefab
	int currentHealth;
	public Rigidbody2D rigidbody2;
	public AudioSource audio2;
	public FollowAPath path;
	public SubBehavior behavior;
    */
	public float fireRate = 1F;				//Fire Rate between Shots
	private float nextFire = 0.0F; 			//First fire & Next fire Time
		public GameObject shot; 				//Fire Prefab
	public Transform shotSpawn;				//Where the Fire Spawn

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
		Fire();
	}

	void Fire()
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
}
