﻿/// <summary>
/// 2D Space Shooter Example
/// By Bug Games www.Bug-Games.net
/// Programmer: Danar Kayfi - Twitter: @DanarKayfi
/// Special Thanks to Kenney for the CC0 Graphic Assets: www.kenney.nl
/// 
/// This is the GameController Script:
/// - Control The Waves of the asteroid and the enemies
/// 
/// </summary>

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class Buttons
{
	public Button ReplayButton;
	public Button StartButton;
	public Button SettingsButton;
	public GameObject Panel;
}
//Asteroid Properties
[System.Serializable]
public class Asteroid 
{
	public GameObject asteroidBigObj; 		//Object Prefab
	public int Count; 						//Number of the object in 1 wave
	public float SpawnWait; 				//Time to wait before a new spawn
	public float StartWait; 				//Time to Start spawning
	public float WaveWait; 					//Time to wait till a new wave
}

//EnemyBlue Properties
[System.Serializable]
public class EnemyBlue 
{
	public GameObject enemyBlueObj;			//Object Prefab
	public int Count;						//Number of the object in 1 wave
	public float SpawnWait;					//Time to wait before a new spawn
	public float StartWait;					//Time to Start spawning
	public float WaveWait;					//Time to wait till a new wave
}

//EnemyGreen Properties
[System.Serializable]
public class EnemyGreen 
{
	public GameObject enemyGreenObj;		//Object Prefab
	public int Count;						//Number of the object in 1 wave
	public float SpawnWait;					//Time to wait before a new spawn
	public float StartWait;					//Time to Start spawning
	public float WaveWait;					//Time to wait till a new wave
}

[System.Serializable]
public class Hazard
{
	public GameObject hazard;		//Object Prefab
	public int Count;						//Number of the object in 1 wave
	public float SpawnWait;					//Time to wait before a new spawn
	public float StartWait;					//Time to Start spawning
	public float WaveWait;					//Time to wait till a new wave
}

//EnemyRed Properties
[System.Serializable]
public class EnemyRed 
{
	public GameObject enemyRedObj;		//Object Prefab
	public int Count;					//Number of the object in 1 wave
	public float SpawnWait;				//Time to wait before a new spawn
	public float StartWait;				//Time to Start spawning
	public float WaveWait;				//Time to wait till a new wave
}


public class GameController_Script : MonoBehaviour 
{	
	//Public Var
	//public Asteroid asteroid;			//make an Object from Class asteroid
	//public EnemyBlue enemyBlue;			//make an Object from Class enemyBlue
	//public EnemyGreen enemyGreen;		//make an Object from Class enemyGreen
	//public EnemyRed enemyRed;			//make an Object from Class enemyRed

	public Hazard asteroid;
	public Hazard enemyBlue;
	public Hazard enemyGreen;
	public Hazard enemyRed;
	public Vector2 spawnValues;			//Store spawning (x,y) values
	public Buttons ListButton; 
	public GameObject Player;
	
	// Use this for initialization
	void Start ()
	{
		
		ListButton.ReplayButton.gameObject.SetActive(false);
		ListButton.ReplayButton.onClick.AddListener(Replay);
		ListButton.StartButton.gameObject.SetActive(true);
		ListButton.StartButton.onClick.AddListener(StartGame);
	}

	// Update is called once per frame
	void Update () 
	{
		if(SharedValues_Script.gameover)
		{
			ListButton.ReplayButton.gameObject.SetActive(true);
		}
		//Excute when keyboard button R pressed
		/* if(ListButton.ReplayButton)
		{
			SceneManager.LoadScene("Scene_01");
			//Load Level 0 (same Level) to make a restart
		}*/
	}

	void Replay()
	{
		SceneManager.LoadScene("Scene_01");
	}

	void StartGame()
	{
		//StartCoroutine (asteroidSpawnWaves());  	//Start IEnumerator function
		StartCoroutine (enemyBlueSpawnWaves());		//Start IEnumerator function
		//StartCoroutine (enemyGreenSpawnWaves());	//Start IEnumerator function
		//StartCoroutine (enemyRedSpawnWaves());		//Start IEnumerator function
		ListButton.StartButton.gameObject.SetActive(false);
		ListButton.Panel.gameObject.SetActive(false);
		Player.gameObject.SetActive(true);
	}
	//Asteroid IEnumerator Coroutine
	IEnumerator asteroidSpawnWaves()
	{
		yield return new WaitForSeconds (asteroid.StartWait); 															//Wait for Seconds before start the wave

		//Infinite Loop
		while (true)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < asteroid.Count; i++)
			{
				Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;							 								//Default Rotation
				Instantiate (asteroid.hazard, spawnPosition, spawnRotation); 						//Instantiate Object
				yield return new WaitForSeconds (asteroid.SpawnWait); 													//Wait for seconds before spawning the next object
			}
			yield return new WaitForSeconds (asteroid.WaveWait); 														//wait for seconds before the next wave
		}
	}

	//EnemyBlue IEnumerator Coroutine
	IEnumerator enemyBlueSpawnWaves()
	{
		yield return new WaitForSeconds (enemyBlue.StartWait);															//Wait for Seconds before start the wave

		//Infinite Loop
		while (true)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < enemyBlue.Count; i++)
			{
				enemyBlue.hazard=ObjectPooler.SharedInstance.GetPooledObject("Enemy");
				Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;															//Default Rotation
				//Instantiate (enemyBlue.hazard, spawnPosition, spawnRotation);		 //Instantiate Object
				enemyBlue.hazard.transform.position=spawnPosition;
				enemyBlue.hazard.transform.rotation=spawnRotation;
				enemyBlue.hazard.SetActive(true);
				yield return new WaitForSeconds (enemyBlue.SpawnWait);													//Wait for seconds before spawning the next object
			}
			yield return new WaitForSeconds (enemyBlue.WaveWait);														//wait for seconds before the next wave
		}
	}

	//EnemyGreen IEnumerator Coroutine
	IEnumerator enemyGreenSpawnWaves()
	{
		yield return new WaitForSeconds (enemyGreen.StartWait);															//Wait for Seconds before start the wave

		//Infinite Loop
		while (true)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < enemyGreen.Count; i++)
			{
				Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;															//Default Rotation
				Instantiate (enemyGreen.hazard, spawnPosition, spawnRotation);									//Instantiate Object
				yield return new WaitForSeconds (enemyGreen.SpawnWait);													//Wait for seconds before spawning the next object
			}
			yield return new WaitForSeconds (enemyGreen.WaveWait);														//wait for seconds before the next wave
		}
	}

	//EnemyRed IEnumerator Coroutine
	IEnumerator enemyRedSpawnWaves()
	{
		yield return new WaitForSeconds (enemyRed.StartWait);															//Wait for Seconds before start the wave

		//Infinite Loop
		while (true)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < enemyRed.Count; i++)
			{
				Vector2 spawnPosition = new Vector2 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y);		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;															//Default Rotation
				Instantiate (enemyRed.hazard, spawnPosition, spawnRotation);										//Instantiate Object
				yield return new WaitForSeconds (enemyRed.SpawnWait);													//Wait for seconds before spawning the next object
			}
			yield return new WaitForSeconds (enemyRed.WaveWait);														//wait for seconds before the next wave
		}
	}
		
}
