using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour {
	public Camera OrthoCamera;
	//Supported screen size: 16:9/ 16:10/3:2/4:3/18:9(this is my phone) 
	//Camera size:			 6.3 /5.7/5.3/ 4.7/ 7.1
	void Start () {
		//OrthoCamera.orthographic=true;
		//OrthoCamera.orthographicSize=10.0f;
		Debug.Log("Width: "+Screen.width);
		Debug.Log("Height: "+Screen.height);
		Debug.Log(Camera.main.aspect);
		if(Camera.main.aspect>=0.7)
		{
			Debug.Log("4:3");
			OrthoCamera.orthographicSize=4.7f;
		}
		else if (Camera.main.aspect >= 0.6)
		{
			Debug.Log("3:2");
			OrthoCamera.orthographicSize=5.3f;
		}
		else if (Camera.main.aspect >=0.5 )
		{
			Debug.Log("16:9");
			OrthoCamera.orthographicSize=6.3f;
		}
	}

}
