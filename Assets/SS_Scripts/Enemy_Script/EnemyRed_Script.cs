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
	public Rigidbody2D rigidbody2;
	public AudioSource audio2;
	
	public StreamPara inPath;
	public FollowAPath path;
	public SubBehavior behavior;

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
		Fire2();
	}
/* 
	void Move()
	{
		rigidbody2.velocity = -1 * transform.up * speed;
		//yield return new WaitForSeconds (2);
		//rigidbody2.velocity =Vector2.zero;
		//yield return new WaitForSeconds (40);
		//rigidbody2.velocity = -1 * transform.up * speed;
	}
*/
	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == "PlayerRegularShot" || other.tag=="PlayerBurstShot" ||other.tag=="TracingHead")
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
/* 
	IEnumerator GoByPath(int pathNumber)
	{
		bool repeat=true;
		//coroutine=false;
		/* 
		Vector2 p=path[pathNumber].GetChild(0).position;
		Vector2 p1=path[pathNumber].GetChild(1).position;
		Vector2 p2=path[pathNumber].GetChild(2).position;
		Vector2 p3=path[pathNumber].GetChild(3).position;
		
		Vector2 p=inPath.path[0].position;
		Vector2 p1=inPath.path[1].position;
		Vector2 p2=inPath.path[2].position;
		Vector2 p3=inPath.path[3].position;
		Debug.Log(inPath.loop);

		while(repeat)
		{
			while(inPath.tParam<1)
			{
			inPath.tParam+=Time.deltaTime * (speed/10);
			inPath.objPostion=Mathf.Pow(1-inPath.tParam,3)*p+
			3*Mathf.Pow(1-inPath.tParam,2)*inPath.tParam*p1+
			3*(1-inPath.tParam)*Mathf.Pow(inPath.tParam,2)*p2+
			Mathf.Pow(inPath.tParam,3)*p3;

			transform.position= inPath.objPostion;
			yield return new WaitForEndOfFrame();
			}

			inPath.tParam=0;

			if(inPath.pathReverse)
			{
				while(inPath.tParam<1)
				{
				inPath.tParam+=Time.deltaTime * (speed/10);
				inPath.objPostion=Mathf.Pow(1-inPath.tParam,3)*p3+
				3*Mathf.Pow(1-inPath.tParam,2)*inPath.tParam*p2+
				3*(1-inPath.tParam)*Mathf.Pow(inPath.tParam,2)*p1+
				Mathf.Pow(inPath.tParam,3)*p;

				transform.position= inPath.objPostion;
				yield return new WaitForEndOfFrame();
				}
			}
			inPath.tParam=0f;
			
			if(!inPath.loop)
			{
				repeat=inPath.loop;
				Debug.Log("notLoop");
			}
		}
		gameObject.SetActive(false);
	}

	public void SetPath()
	{
		inPath.pathToGo=0;
		inPath.tParam=0f;
	}
*/	
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
