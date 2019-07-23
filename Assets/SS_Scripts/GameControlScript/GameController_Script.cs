using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Asteroid Properties
/* [System.Serializable]
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
*/
[System.Serializable]
public class Hazard
{
	public GameObject hazard;		//Object Prefab
	public int Count;						//Number of the object in 1 wave
	public float RandomMin, RandomMax;
	public float SpawnWait;					//Time to wait before a new spawn
	public float StartWait;					//Time to Start spawning
	public float WaveWait;					//Time to wait till a new wave
	public float x_pos, y_pos; //position to spawn //
}
//Stream of enemy(Enemey object, Spawn_Position, Behaviour) 
//player info: playership, score, bullet type, skin
public class GameController_Script : MonoBehaviour 
{	
	//public Asteroid asteroid;			//make an Object from Class asteroid
	//public EnemyBlue enemyBlue;			//make an Object from Class enemyBlue
	//public EnemyGreen enemyGreen;		//make an Object from Class enemyGreen
	//public EnemyRed enemyRed;			//make an Object from Class enemyRed
	public Hazard asteroid;
	public Hazard enemyBlue;
	public Hazard enemyGreen;
	public Hazard enemyRed;
	public Vector2 spawnValues;			//Store spawning (x,y) values
	public GameObject Player;
	public GameObject UI_controller;
	UICOntroller UIControllerS;
	
	// Use this for initialization
	void Start ()
	{
		UIControllerS=UI_controller.GetComponent<UICOntroller>();
		UIControllerS.ListElements.ReplayButton.gameObject.SetActive(false);
		UIControllerS.ListElements.ReplayButton.onClick.AddListener(Replay);
		UIControllerS.ListElements.StartButton.onClick.AddListener(StartGame);
		UIControllerS.ListElements.InGamePanel.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(SharedValues_Script.gameover)
		{
			//ListButton.ReplayButton.gameObject.SetActive(true);
			UIControllerS.ListElements.ReplayButton.gameObject.SetActive(true);
		}
	}

	public void Replay()
	{
		SceneManager.LoadScene("Scene_01");
		//StartGame(); 
	}

	public void StartGame()
	{
		//StartCoroutine (asteroidSpawnWaves());  	//Start IEnumerator function
		StartCoroutine (enemyBlueSpawnWaves());		//Start IEnumerator function
		//StartCoroutine (StreamOfEnemy(3,new Vector2(0,5), enemyBlue.hazard));
		//StartCoroutine (enemyGreenSpawnWaves());	//Start IEnumerator function
		//StartCoroutine (enemyRedSpawnWaves());		//Start IEnumerator function 
		UIControllerS.ListElements.MainMenuPanel.gameObject.SetActive(false);
		Player.gameObject.SetActive(true);
	}
	//Regular spawn
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
				Instantiate (asteroid.hazard, spawnPosition, spawnRotation); 						
				yield return new WaitForSeconds (asteroid.SpawnWait); 													//Wait for seconds before spawning the next object
			}
			yield return new WaitForSeconds (asteroid.WaveWait); 														//wait for seconds before the next wave
		}
	}

	IEnumerator StreamOfEnemy(int numberOfWave,Vector2 SpawnPos, GameObject gObj)
	{
		yield return new WaitForSeconds (asteroid.StartWait); 															//Wait for Seconds before start the wave

		//Infinite Loop
		while (numberOfWave!=0)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < asteroid.Count; i++)
			{
				Vector2 spawnPosition = SpawnPos;		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;							 								//Default Rotation
				Instantiate (gObj, spawnPosition, spawnRotation); 						
				yield return new WaitForSeconds (asteroid.SpawnWait); 													//Wait for seconds before spawning the next object
			}
			numberOfWave--;
			if(numberOfWave==0)
			{
				Debug.Log("Stop Stream");
				StopCoroutine("StreamOfEnemy");
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
				enemyBlue.hazard=BlueEnemyPooler.SharedBlueEnemyPool.GetPooledObject("EnemyBlue");
				Vector2 spawnPosition = new Vector2 (Random.Range (-enemyBlue.x_pos, enemyBlue.x_pos), enemyBlue.y_pos);		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;															//Default Rotation
				//Instantiate (enemyBlue.hazard, spawnPosition, spawnRotation);	
				enemyBlue.SpawnWait=Random.Range(enemyBlue.RandomMin, enemyBlue.RandomMax);
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
		
	void LevelBuilder()
	{
		
	}
}
