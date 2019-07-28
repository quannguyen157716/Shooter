using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level
{
	public string name;
    public SpawnInfo[] events;
	public float[] timeToStartEachEvent; 
}

public class LevelBuilder : MonoBehaviour {
	List<Level> lv;
	IEnumerator RunLevel()
	{
		foreach(Level l in lv)
		{
			for(int i=0; i<l.events.Length; i++)
			{
				yield return new WaitForSeconds (l.timeToStartEachEvent[i]);
				if(l.events[i].typeOfSpawn=="enemyRandomSpawn")
				StartCoroutine(GameController_Script.GameControllerInstance.enemyRandomSpawn(l.events[i]));
				else if(l.events[i].typeOfSpawn=="singleSpawn")
				StartCoroutine(GameController_Script.GameControllerInstance.singleSpawn(l.events[i]));
				//else if(l.events[i].typeOfSpawn=="streamOfEnemySpawn")
				//StartCoroutine(GameController_Script.GameControllerInstance.streamOfEnemySpawn(l.events[i]));
			}
		}
	}

	void loadLevel()
	{
		
	}
}
