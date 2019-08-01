using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	GameObject enemy;
	public Transform[] points;
	Vector2 GizmoPosition;
	Vector2 p,p1,p2,p3;
	public int NumberOfEnemy;
	
	public float TimeBetween;

	void Start()
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

	IEnumerator CreateStream()
	{
		Debug.Log("Stream");
		for(int i=0; i<NumberOfEnemy;i++)
		{
			
			enemy=ObjectPooler.ObjectPoolerInstance.GetPooledObject("EnemyBlue", points[0].position,false);
			EnemyBlue_Script TestC=enemy.GetComponent<EnemyBlue_Script>();
			TestC.path=points;
			TestC.SetPath();
			TestC.InStream=false;
			TestC.gameObject.SetActive(true);
			yield return new WaitForSeconds(TimeBetween);
		}
	}
}	
