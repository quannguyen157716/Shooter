using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Level
{
	public string name;
    public SpawnInfo[] events;
	public float[] timeToStartEachEvent;
}

public class LevelBuilder : MonoBehaviour {
	List<Level> lv;

	public int numberOflevel;
	void Start()
	{
		lv=new List<Level>();
		loadLevel();
		StartCoroutine(RunLevel());
		Debug.Log("Run level");
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
				Debug.Log("Event "+i );
				if(l.events[i].typeOfSpawn=="enemyRandomSpawn")
				{
					StartCoroutine(GameController_Script.GameControllerInstance.enemyRandomSpawn(l.events[i]));
				}	
				else if(l.events[i].typeOfSpawn=="singleSpawn")
				{
					StartCoroutine(GameController_Script.GameControllerInstance.singleSpawn(l.events[i]));
				}
				Debug.Log(i+" "+l.events[i].spawnEnd);
				//else if(l.events[i].typeOfSpawn=="streamOfEnemySpawn") 
				//StartCoroutine(GameController_Script.GameControllerInstance.streamOfEnemySpawn(l.events[i]));
			}
			yield return new WaitUntil(()=>l.events[i-1].spawnEnd==true);
			Debug.Log("End Level");
		}
	}

	void loadLevel()
	{
		Debug.Log("Load Level");
	    for(int i=0; i<numberOflevel;i++)
		{
			string json=File.ReadAllText(Application.persistentDataPath+"/Level_"+(i+1)+".json");
			Level l=JsonUtility.FromJson<Level>(json);
			Debug.Log(l.name);
			lv.Add(l);
		}
	}
}
