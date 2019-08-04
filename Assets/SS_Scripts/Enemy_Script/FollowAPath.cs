using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StreamPara
{
	//InPathconfiguration
	
	[HideInInspector]
	public Transform[] path;
	
	[HideInInspector]
	public int pathToGo;//
	
	[HideInInspector]
	public float tParam;
	
	[HideInInspector]
	public Vector2 objPostion;
	[HideInInspector]
	public bool InStream=false;
	[HideInInspector]
	public bool pathReverse=false;
	[HideInInspector]
	public bool loop=true;
}

public class FollowAPath : MonoBehaviour {
	public StreamPara inPath; 
	void OnEnable()
	{
		inPath.pathToGo=0;
		inPath.tParam=0f;
	}

	public void FollowPath(float speed)
	{
		StartCoroutine(GoByPath(inPath.pathToGo, speed));
	}
	IEnumerator GoByPath(int pathNumber, float speed)
	{
		bool repeat=true;
		//coroutine=false;
		/* 
		Vector2 p=path[pathNumber].GetChild(0).position;
		Vector2 p1=path[pathNumber].GetChild(1).position;
		Vector2 p2=path[pathNumber].GetChild(2).position;
		Vector2 p3=path[pathNumber].GetChild(3).position;
		*/
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
	}
}
