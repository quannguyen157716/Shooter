using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour {
	//public Text t;
	public Camera OrthoCamera;
	float asp;
	//Supported screen size: 16:9/ 16:10/3:2/4:3/18:9(this is my phone) 
	//Camera size:			 6.3 /5.7   /5.3/ 4.7/ 7.1
	void Start () {
		//OrthoCamera.orthographic=true;
		//OrthoCamera.orthographicSize=10.0f;
	float h=(float)Screen.height;
	float w=(float)Screen.width;
	asp=h/w;
	if (asp>=1.9)
	{
		//Debug.Log("18:9");
		OrthoCamera.orthographicSize=7.1f;
	}	
	else if (asp >= 1.7)
	{
    	//Debug.Log("16:9");
		OrthoCamera.orthographicSize=6.3f;
	}	
	else if (asp >= 1.5)
	{
    	//Debug.Log("3:2");
		OrthoCamera.orthographicSize=5.3f;
	}
	else
	{
		//Debug.Log("4:3");
		OrthoCamera.orthographicSize=4.7f;
	}

	//t.text="width: "+ w+ " height: "+ h+" asp: "+asp;
    }

}
