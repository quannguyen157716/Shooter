﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class Level
{
	public string name;
    public SpawnInfo[] events;
	public float[] timeToStartEachEvent;
}

public class LevelBuilder : MonoBehaviour {

	public static LevelBuilder LevelBuilderInstance;
	[Tooltip ("List of level and their configuration")]
	public List<Level> lv;
	[Tooltip ("List of level to load")]
	public string[] LevelName;
	int numberOflevel;

	
	void Awake()
	{
		LevelBuilderInstance=this;
		lv=new List<Level>();
		numberOflevel=LevelName.Length;
		loadLevel();
		//StartCoroutine(RunLevel());
		//Debug.Log("Run level");
	}
	IEnumerator RunLevel()
	{
		int i;
		Debug.Log("Level start");
		foreach(Level l in lv)
		{
			Debug.Log(l.name);
			for(i=0; i<l.events.Length; i++)
			{
				yield return new WaitForSeconds (l.timeToStartEachEvent[i]);
				Debug.Log("Event "+l.events[i].ID );
				if(l.events[i].typeOfSpawn=="enemyRandomSpawn")
				{
					StartCoroutine(GameController_Script.GameControllerInstance.enemyRandomSpawn(l.events[i]));
				}	
				else if(l.events[i].typeOfSpawn=="singleSpawn")
				{
					GameController_Script.GameControllerInstance.singleSpawn(l.events[i]);
				}
				else if(l.events[i].typeOfSpawn=="enemyHorizontalRandomSpawn")
				{
					StartCoroutine(GameController_Script.GameControllerInstance.enemyHorizontalRandomSpawn(l.events[i]));
				}
				//else if(l.events[i].typeOfSpawn=="streamOfEnemySpawn") 
				//StartCoroutine(GameController_Script.GameControllerInstance.streamOfEnemySpawn(l.events[i]));
			}
			Debug.Log(i+" "+l.events[i-1].spawnEnd);
			yield return new WaitUntil(()=>l.events[i-1].spawnEnd==true);//make sure levels do not mix
			Debug.Log("End Level");
		}
	}

	public void StartLevel()
	{
		StartCoroutine(RunLevel());
	}
	void loadLevel()
	{
		Debug.Log("Load Level");
	    for(int i=0; i<numberOflevel;i++)
		{
			string json=File.ReadAllText(Application.persistentDataPath+"/"+LevelName[i]+".json");
			Level l=JsonUtility.FromJson<Level>(json);
			Debug.Log(l.name);
			lv.Add(l);
		}
	}
}
