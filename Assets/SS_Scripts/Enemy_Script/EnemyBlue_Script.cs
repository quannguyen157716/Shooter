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
	//General configuration
	public float speed; //Enemy Ship Speed
	public int health; //Enemy Ship Health
	int currentHealth;
	public int ScoreValue; //How much the Enemy Ship give score after explosion
	public GameObject LaserHit; //LaserGreenHit Prefab
	public GameObject Explosion; //Explosion Prefab
	public Rigidbody2D rigidbody2;
	
	//public StreamPara inPath;
	public FollowAPath path;
	public SubBehavior behavior;

	int test;
	void OnEnable () 
	{
		currentHealth=health;
		//rigidbody2=GetComponent<Rigidbody2D>();   
		//Move();
		if(path.inPath.InStream)
		{
			//StartCoroutine(GoByPath(inPath.pathToGo));
			path.FollowPath(speed);
		}
		else
		{
			//Debug.Log("Move");
			behavior.MoveStraight(rigidbody2, speed);
		}
		path.inPath.InStream=false;
	}

/* 	public void SetPath()
	{
		inPath.pathToGo=0;
		inPath.tParam=0f;
	}*/

	//Called when the Trigger entered
	void OnTriggerEnter2D(Collider2D other)
	{
		//Excute if the object tag was equal to one of these
		if(other.tag == PlayerGun.PlayerGunInstance.shotType)
		{
			ObjectPooler.ObjectPoolerInstance.GetPooledObject(LaserHit.tag, transform.position,true);
			//Instantiate (LaserHit, transform.position , transform.rotation); 			//Instantiate LaserGreenHit 
			//Destroy(other.gameObject); 														//Destroy the Other (PlayerLaser)
			other.gameObject.SetActive(false);
			//Check the Health if greater than 0
			if(currentHealth > 0)
			TakeDamage(PlayerGun.PlayerGunInstance.shotDamage);	

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
		
		//Vector2 p=path[pathNumber].GetChild(0).position;
		//Vector2 p1=path[pathNumber].GetChild(1).position;
		//Vector2 p2=path[pathNumber].GetChild(2).position;
		//Vector2 p3=path[pathNumber].GetChild(3).position;
		
		Vector2 p=inPath.path[0].position;
		Vector2 p1=inPath.path[1].position;
		Vector2 p2=inPath.path[2].position;
		Vector2 p3=inPath.path[3].position;
//		Debug.Log(inPath.loop);
		//Debug.Log(NumberOfWave);
		//Debug.Log("Moving");
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
				//Debug.Log("notLoop");
			}
		}
		gameObject.SetActive(false);
} */
/* 
	void Move()
	{
		rigidbody2.velocity = -1 * transform.up * speed; //Enemy Ship Movement
	}
*/
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