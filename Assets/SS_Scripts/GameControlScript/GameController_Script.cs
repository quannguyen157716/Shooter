using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;


/* [System.Serializable]
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
	public float StartWait;					
	public float WaveWait;					//Time to wait till a new wave
	public float x_pos, y_pos; //position to spawn 
}
[System.Serializable]
public class SpawnInfo
{
	public string ID;
	public string enemyTag; //tag of enemy to spawn
	public string typeOfSpawn;
	public int numberOfWave; //number of wave =0 it is individual spawn
	public int numberOfObject; //number of object in one wave
	public float start_Wait; //Time to Start spawning
	public float RandomSpawnWaitMin;  //Time to wait before a new spawn
	public float RandomSpawnWaitMax;
	public float wavewaitMin;  //Time to wait till a new wave
	public float wavewaitMax;
	public Vector2 position;  //position to spawn 
}
//Stream of enemy(Enemey object, Spawn_Position, Behaviour) 
//player info: playership, score, bullet type, skin

public class GameController_Script : MonoBehaviour 
{	
	//public Asteroid asteroid;			//make an Object from Class asteroid
	//public EnemyBlue enemyBlue;			//make an Object from Class enemyBlue
	//public EnemyGreen enemyGreen;		//make an Object from Class enemyGreen
	//public EnemyRed enemyRed;			//make an Object from Class enemyRed
	public static GameController_Script GameControllerInstance;
	public Hazard asteroid;
	public Hazard enemyBlue;
	public Hazard enemyGreen;
	public Hazard enemyRed;
	public Vector2 spawnValues;			//Store spawning (x,y) values
	public GameObject Player;
	public GameObject UI_controller;
	UICOntroller UIControllerS;
	SpawnInfo s;
	
	// Use this for initialization
	void Start ()
	{
		GameControllerInstance=this;
		UICOntroller.UIControllerInstance.ListElements.ReplayButton.gameObject.SetActive(false);
		UICOntroller.UIControllerInstance.ListElements.ReplayButton.gameObject.SetActive(false);
		UICOntroller.UIControllerInstance.ListElements.ReplayButton.onClick.AddListener(Replay);
		UICOntroller.UIControllerInstance.ListElements.StartButton.onClick.AddListener(StartGame);
		UICOntroller.UIControllerInstance.ListElements.InGamePanel.gameObject.SetActive(false);
	}

	// Update is called once per frame
	void Update () 
	{
		if(SharedValues_Script.gameover)
		{
			//ListButton.ReplayButton.gameObject.SetActive(true);
			UICOntroller.UIControllerInstance.ListElements.ReplayButton.gameObject.SetActive(true);
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
		//StartCoroutine (enemyBlueSpawnWaves());		//Start IEnumerator function
		//StartCoroutine (StreamOfEnemy(3,new Vector2(0,5), enemyBlue.hazard));
		//StartCoroutine (enemyGreenSpawnWaves());	//Start IEnumerator function
		//StartCoroutine (enemyRedSpawnWaves());		//Start IEnumerator function 
		GetInfo();
		StartCoroutine(enemyRandomSpawn(s));
		UICOntroller.UIControllerInstance.ListElements.MainMenuPanel.gameObject.SetActive(false);
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
				
				Vector2 spawnPosition = new Vector2 (Random.Range (-enemyBlue.x_pos, enemyBlue.x_pos), enemyBlue.y_pos);		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;			
				ObjectPooler.ObjectPoolerInstance.GetPooledObject("EnemyBlue",spawnPosition);												//Default Rotation
				//Instantiate (enemyBlue.hazard, spawnPosition, spawnRotation);	
				enemyBlue.SpawnWait=Random.Range(enemyBlue.RandomMin, enemyBlue.RandomMax);
				//enemyBlue.hazard.transform.position=spawnPosition;
				//enemyBlue.hazard.transform.rotation=spawnRotation;
				//enemyBlue.hazard.SetActive(true);
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
			for (int i = 0; i < enemyBlue.Count; i++)
			{
				
				Vector2 spawnPosition = new Vector2 (Random.Range (-enemyGreen.x_pos, enemyGreen.x_pos), enemyGreen.y_pos);		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;				
				ObjectPooler.ObjectPoolerInstance.GetPooledObject("EnemyGreen",spawnPosition);											//Default Rotation
				//Instantiate (enemyBlue.hazard, spawnPosition, spawnRotation);	
				enemyGreen.SpawnWait=Random.Range(enemyGreen.RandomMin, enemyGreen.RandomMax);
				//enemyGreen.hazard.transform.position=spawnPosition;
				//enemyGreen.hazard.transform.rotation=spawnRotation;
				//enemyGreen.hazard.SetActive(true);
				yield return new WaitForSeconds (enemyGreen.SpawnWait);													//Wait for seconds before spawning the next object
			}
			yield return new WaitForSeconds (enemyGreen.WaveWait);											//wait for seconds before the next wave
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
				Instantiate (enemyRed.hazard, spawnPosition, spawnRotation);			//Instantiate Object
				yield return new WaitForSeconds (enemyRed.SpawnWait);				//Wait for seconds before spawning the next object
			}
			yield return new WaitForSeconds (enemyRed.WaveWait);			//wait for seconds before the next wave
		}
	}
	
	//Spawn enemy as stream
	public IEnumerator streamOfEnemySpawn(int numberOfWave,Vector2 SpawnPos, GameObject gObj)
	{
		yield return new WaitForSeconds (asteroid.StartWait); 				//Wait for Seconds before start the wave

		//Infinite Loop
		while (numberOfWave!=0)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < asteroid.Count; i++)
			{
				Vector2 spawnPosition = SpawnPos;		//Random Spawn Position
				Quaternion spawnRotation = Quaternion.identity;				//Default Rotation
				Instantiate (gObj, spawnPosition, spawnRotation); 						
				yield return new WaitForSeconds (asteroid.SpawnWait); 		//Wait for seconds before spawning the next object
			}
			numberOfWave--;
			if(numberOfWave==0)
			{
				Debug.Log("Stop Stream");
				StopCoroutine("StreamOfEnemy");
			}
			yield return new WaitForSeconds (asteroid.WaveWait); 			//wait for seconds before the next wave
		}
	}

	//Random spawn from range of position
	public IEnumerator enemyRandomSpawn(SpawnInfo ifo)
	{
		float numberofWave=0;
		yield return new WaitForSeconds (ifo.start_Wait);		//Wait for Seconds before start the wave
		//Infinite Loop 
		while (numberofWave <ifo.numberOfWave)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < ifo.numberOfObject; i++)
			{
				Vector2 spawnPosition = new Vector2 (Random.Range (-ifo.position.x, ifo.position.x), ifo.position.y);		
				Quaternion spawnRotation = Quaternion.identity;			
				ObjectPooler.ObjectPoolerInstance.GetPooledObject(ifo.enemyTag, spawnPosition);											
				float spawnWait=Random.Range(ifo.RandomSpawnWaitMin, ifo.RandomSpawnWaitMax);
				yield return new WaitForSeconds (spawnWait);													
			}
			numberofWave++;
			Debug.Log("Wave: "+numberofWave);
			Debug.Log("Time: "+ Time.time);
			float waveWait=Random.Range(ifo.wavewaitMin, ifo.RandomSpawnWaitMax);
			yield return new WaitForSeconds (waveWait);		//wait for seconds before the next wave
		}
	}	

	//wait to spawn an object at specific location after specific time
	public IEnumerator singleSpawn(SpawnInfo ifo)
	{
		yield return new WaitForSeconds (ifo.start_Wait);		//Wait for Seconds before start the wave			
		ObjectPooler.ObjectPoolerInstance.GetPooledObject(ifo.enemyTag, ifo.position);											

	}
	void GetInfo()
	{
		string json=File.ReadAllText(Application.persistentDataPath+"/S00.json");
		s=JsonUtility.FromJson<SpawnInfo>(json);
	}

	
}
