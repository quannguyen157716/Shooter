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

	void Start()
	{
		lv=new List<Level>();
		loadLevel();
		StartCoroutine(RunLevel());
		Debug.Log("Run level");
	}
	IEnumerator RunLevel()
	{
		Debug.Log("Level start");
		foreach(Level l in lv)
		{
			Debug.Log("Level1" );
			for(int i=0; i<l.events.Length; i++)
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
				
				//else if(l.events[i].typeOfSpawn=="streamOfEnemySpawn")
				//StartCoroutine(GameController_Script.GameControllerInstance.streamOfEnemySpawn(l.events[i]));
			}
		}
	}

	void loadLevel()
	{
		string json=File.ReadAllText(Application.persistentDataPath+"/Level1.json");
		Level l=JsonUtility.FromJson<Level>(json);
		Debug.Log(l.name);
		lv.Add(l);
	}
}
