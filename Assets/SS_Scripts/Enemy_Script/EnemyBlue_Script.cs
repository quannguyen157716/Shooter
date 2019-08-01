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
	
	
	[HideInInspector]
	public Transform[] path;
	//public int NumberOfWave=1;
	int pathToGo;
	float tParam;
	Vector2 objPostion;
	
	[HideInInspector]
	public bool InStream=false;
	

	void OnEnable () 
	{
		currentHealth=health;
		//rigidbody2=GetComponent<Rigidbody2D>();   
		//Move();
		if(InStream)
		StartCoroutine(GoByPath(pathToGo));
		else
		{
			//Debug.Log("Move");
			Move();
		}
		
		InStream=false;
	}

	public void SetPath()
	{
		pathToGo=0;
		tParam=0f;
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

	IEnumerator GoByPath(int pathNumber)
	{
		
		//coroutine=false;
		/* 
		Vector2 p=path[pathNumber].GetChild(0).position;
		Vector2 p1=path[pathNumber].GetChild(1).position;
		Vector2 p2=path[pathNumber].GetChild(2).position;
		Vector2 p3=path[pathNumber].GetChild(3).position;
		*/
		Vector2 p=path[0].position;
		Vector2 p1=path[1].position;
		Vector2 p2=path[2].position;
		Vector2 p3=path[3].position;
		//Debug.Log(NumberOfWave);
		//Debug.Log("Moving");
		while(tParam<1)
		{
			tParam+=Time.deltaTime * (speed/10);
			objPostion=Mathf.Pow(1-tParam,3)*p+
			3*Mathf.Pow(1-tParam,2)*tParam*p1+
			3*(1-tParam)*Mathf.Pow(tParam,2)*p2+
			Mathf.Pow(tParam,3)*p3;

			transform.position= objPostion;
			yield return new WaitForEndOfFrame();
		}
		tParam=0f;
		gameObject.SetActive(false);
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