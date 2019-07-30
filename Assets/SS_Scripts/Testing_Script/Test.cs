using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	//[Tooltip("Enemy's prefab")]
    public GameObject enemy;

    //[Tooltip("a number of enemies in the wave")]
    public int count;

    //[Tooltip("path passage speed")]
    public float speed;

    //[Tooltip("time between emerging of the enemies in the wave")]
    public float timeBetween;

    //[Tooltip("points of the path. delete or add elements to the list if you want to change the number of the points")]
    public Transform[] pathPoints;

    public Vector2 a,b,c;
  
   
    //[Tooltip("color of the path in the Editor")]
    public Color pathColor = Color.yellow;
       public bool testMode;

	public Vector2 QuadraticCurve(Vector2 a, Vector2 b, Vector2 c, float t)
	{
		Vector2 p0=Vector2.Lerp(a,b,t);
		Vector2 p1=Vector2.Lerp(b,c,t);
		return Vector2.Lerp(p0,p1,t);
	}

	void Awake()
	{
		Debug.Log(QuadraticCurve(pathPoints[0].position,pathPoints[1].position,pathPoints[2].position,0.5f));
	}

	void OnDrawGizmos()  
    {
		a=pathPoints[0].position;
		b=pathPoints[1].position;
		c=pathPoints[2].position;

        Gizmos.color=pathColor;
		//Gizmos.DrawLine(pathPoints[0].position,pathPoints[1].position);
		//Gizmos.DrawLine(pathPoints[1].position,pathPoints[2].position);
		Gizmos.DrawLine(a,QuadraticCurve(a,b,c,0.5f));
		Gizmos.DrawLine(c,QuadraticCurve(a,b,c,0.5f));
    }
}
