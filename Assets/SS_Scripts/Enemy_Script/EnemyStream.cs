﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStream : MonoBehaviour 
{
	public GameObject enemy;
	public Transform[] points;
	Vector2 GizmoPosition;
	Vector2 p,p1,p2,p3;
	public int NumberOfEnemy;
	public bool loop;
	public bool reverse;
	public bool InStream;
	public float TimeBetween;
	void OnEnable()
	{
		StartCoroutine(CreateStream());
	}

	void OnDrawGizmos()
	{
		p=points[0].position;
		p1=points[1].position;
		p2=points[2].position;
		p3=points[3].position;
		for(float t=0; t<=1; t+=0.05f)
		{
			GizmoPosition=Mathf.Pow(1-t,3)*p+
			3*Mathf.Pow(1-t,2)*t*p1+
			3*(1-t)*Mathf.Pow(t,2)*p2+
			Mathf.Pow(t,3)*p3;

			Gizmos.DrawSphere(GizmoPosition,0.2f);
		}

		Gizmos.DrawLine(p,p1);
		Gizmos.DrawLine(p2,p3);
	}
	//
	IEnumerator CreateStream()
	{
		//Debug.Log("Stream"); 
		for(int i=0; i<NumberOfEnemy;i++)
		{
			enemy=ObjectPooler.ObjectPoolerInstance.GetPooledObject(enemy.tag, p,false);
			if(enemy!=null)
			{
				//follow path 
				FollowAPath TestC=enemy.GetComponent<FollowAPath>();
				TestC.inPath.path=points;
				TestC.inPath.InStream=InStream;
				TestC.inPath.loop=loop;
				TestC.inPath.pathReverse=reverse;
				TestC.gameObject.SetActive(true);
			}
			else
			Debug.Log("No object");
			yield return new WaitForSeconds(TimeBetween);
		}
		gameObject.SetActive(false);
	}
}
