﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

	bool isPaused=false;
	public void OnPause()
	{
		Debug.Log("Pause");
		if(isPaused)
		{
			Time.timeScale=1;
			isPaused=false;
		}
		else
		{
			Time.timeScale=0;
			isPaused=true;
		}
	}
}