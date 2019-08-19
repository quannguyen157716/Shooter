using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubBehavior:MonoBehaviour
{

	public void MoveInScene(float speed, Vector3 position)
	{
		position.z=0;
		StartCoroutine(moveInScene(speed, position));
	}
	
	public void MoveStraight(Rigidbody2D rigidbody2, float speed)
	{
		rigidbody2.velocity = -1 * transform.up * speed; 
	}

	public void Patrol(Rigidbody2D rb,float height, float speed, float time)
	{
		StartCoroutine(patrol(rb ,height,speed, time));
	}
	//Move around specific path     
	IEnumerator patrol(Rigidbody2D rb,float height, float speed,float time)
	{
		float duration=0;
		float startTime;
		bool back=true;
		while(duration<time)
		{
			startTime=Time.time;
			if(back)
			{
				transform.position=Vector2.MoveTowards(transform.position,new Vector2(3,height),speed*Time.deltaTime);
				if(transform.position.x==3)
				back=false;
			}
			else
			{
				transform.position=Vector2.MoveTowards(transform.position,new Vector2(-3,height),speed*Time.deltaTime);
				if(transform.position.x==-3)
				back=true;
			}
			yield return new WaitForEndOfFrame();
			duration+=(Time.time-startTime);
			//Debug.Log(duration);      
		}
		rb.velocity = -1 * transform.up * speed;
	}

	IEnumerator moveInScene(float speed, Vector3 position)
	{
		while(transform.position!=position)
		{
			transform.position=Vector2.MoveTowards(transform.position,position,speed*Time.deltaTime);
			yield return new WaitForEndOfFrame();
		}
	}


}
