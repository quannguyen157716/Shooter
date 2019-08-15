using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTest : MonoBehaviour {
	[HideInInspector]
	public Transform[] path;
	//public int NumberOfWave=1;
	int pathToGo;
	float tParam;
	Vector2 objPostion;
	float speedInStream;
	public float speedOutStream;
	[HideInInspector]
	public bool InStream=false;
	public Rigidbody2D rb;
	void OnEnable()
	{
		if(InStream)
		StartCoroutine(GoByPath(pathToGo));
		else
		Move();
		InStream=false;
	}

	public void SetPath()
	{

		pathToGo=0;
		tParam=0f;
		speedInStream=0.2f;
		
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
		Debug.Log("Moving");
		while(tParam<1)
		{
			tParam+=Time.deltaTime * speedInStream;
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
		//coroutine=true;
	void Move()
	{
		rb.velocity = -1 * transform.up * speedOutStream;
	}
}
