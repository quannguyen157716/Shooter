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
/* 
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
*/
//Stream of enemy(Enemey object, Spawn_Position, Behaviour) 
//player info: playership, score, bullet type, skin

public class GameController_Script : MonoBehaviour 
{	
	//public Asteroid asteroid;			//make an Object from Class asteroid
	//public EnemyBlue enemyBlue;			//make an Object from Class enemyBlue
	//public EnemyGreen enemyGreen;		//make an Object from Class enemyGreen
	//public EnemyRed enemyRed;			//make an Object from Class enemyRed
	public static GameController_Script GameControllerInstance;
	//public Hazard asteroid;
	//public Hazard enemyBlue;
	//public Hazard enemyGreen;
	//public Hazard enemyRed;
	//public Vector2 spawnValues;			//Store spawning (x,y) values
	public GameObject Player;
	//public GameObject UI_controller;
	//UICOntroller UIControllerS;
	SpawnInfo s;
	
	// Use this for initialization
	void Start ()
	{
		//AudioListener.volume=0;
		GameControllerInstance=this;
		UICOntroller.UIControllerInstance.ListElements.ReplayButton.gameObject.SetActive(false);
		UICOntroller.UIControllerInstance.ListElements.ReplayButton.gameObject.SetActive(false);
		UICOntroller.UIControllerInstance.ListElements.ReplayButton.onClick.AddListener(Replay);
		UICOntroller.UIControllerInstance.ListElements.StartButton.onClick.AddListener(StartGame);
		UICOntroller.UIControllerInstance.ListElements.InGamePanel.gameObject.SetActive(false);
		//Debug.Log("GameController");
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
		//StartCoroutine(enemyRandomSpawn(s));
		UICOntroller.UIControllerInstance.ListElements.MainMenuPanel.gameObject.SetActive(false);
		UICOntroller.UIControllerInstance.ListElements.InGamePanel.gameObject.SetActive(true);
		Player.gameObject.SetActive(true);
		LevelBuilder.LevelBuilderInstance.StartLevel();
	}
	
	//EnemyBlue IEnumerator Coroutine
	/* IEnumerator enemyBlueSpawnWaves()
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
				enemyBlue.SpawnWait=Random.Range(enemyBlue.RandomMin, enemyBlue.RandomMax);
				yield return new WaitForSeconds (enemyBlue.SpawnWait);													//Wait for seconds before spawning the next object
			}
			yield return new WaitForSeconds (enemyBlue.WaveWait);														//wait for seconds before the next wave
		}
	}*/

	//Spawn enemy as stream
	//public IEnumerator streamOfEnemySpawn(int numberOfWave,Vector2 SpawnPos, GameObject gObj)
	//{

	//}

	//Random spawn from range of position
	public IEnumerator enemyRandomSpawn(SpawnInfo ifo)
	{
		//Debug.Log("New cor");
		float startTime=Time.time;
		float duration=0;
		//Debug.Log("StarTime: " +startTime);
		float numberofWave=0;
		//yield return new WaitForSeconds (ifo.start_Wait);		//Wait for Seconds before start the wave
		//Infinite Loop 
		while (numberofWave <ifo.numberOfWave)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < ifo.numberOfObject; i++)
			{
				Vector2 spawnPosition = new Vector2 (Random.Range (-ifo.position.x, ifo.position.x), ifo.position.y);		
				Quaternion spawnRotation = Quaternion.identity;			
				ObjectPooler.ObjectPoolerInstance.GetPooledObject(ifo.enemyTag, spawnPosition,true);											
				float spawnWait=Random.Range(ifo.RandomSpawnWaitMin, ifo.RandomSpawnWaitMax);
				yield return new WaitForSeconds (spawnWait);													
			}
			numberofWave++;
			Debug.Log("Wave "+numberofWave);
			float waveWait=Random.Range(ifo.wavewaitMin, ifo.RandomSpawnWaitMax);
			yield return new WaitForSeconds (waveWait);		//wait for seconds before the next wave
			duration+=(Time.time-startTime);
			startTime=Time.time;
			//Debug.Log("EndTime: " +Time.time);
			//Debug.Log("CurrentDuration "+duration );
			if(duration>ifo.duration)
			{
				yield break;
			}
		}
		ifo.spawnEnd=true;
	}	

	public IEnumerator enemyHorizontalRandomSpawn(SpawnInfo ifo)
	{
		float startTime=Time.time;
		float duration=0;
		float numberofWave=0;
		//yield return new WaitForSeconds (ifo.start_Wait);		//Wait for Seconds before start the wave
		//Infinite Loop 
		while (numberofWave <ifo.numberOfWave)
		{
			//Spawn Specific number of Objects in 1 wave
			for (int i = 0; i < ifo.numberOfObject; i++)
			{
				Vector2 spawnPosition = new Vector2 (ifo.position.x, Random.Range(2,ifo.position.y));		
				Quaternion spawnRotation = Quaternion.identity;			
				ObjectPooler.ObjectPoolerInstance.GetPooledObject(ifo.enemyTag, spawnPosition,true);											
				float spawnWait=Random.Range(ifo.RandomSpawnWaitMin, ifo.RandomSpawnWaitMax);
				yield return new WaitForSeconds (spawnWait);													
			}
			numberofWave++;
			Debug.Log("Wave: "+numberofWave);
			float waveWait=Random.Range(ifo.wavewaitMin, ifo.RandomSpawnWaitMax);
			yield return new WaitForSeconds (waveWait);		//wait for seconds before the next wave
			duration+=(Time.time-startTime);
			startTime=Time.time;
			//Debug.Log("EndTime: " +Time.time);
			//Debug.Log("CurrentDuration "+duration );
			if(duration>ifo.duration)
			{
				yield break;
			}
		}
		ifo.spawnEnd=true;
	}	

	//wait to spawn an object at specific location after specific time
	public void singleSpawn(SpawnInfo ifo)
	{
		Debug.Log("ss");
		//yield return new WaitForSeconds (ifo.start_Wait);					
		ObjectPooler.ObjectPoolerInstance.GetPooledObject(ifo.enemyTag, ifo.position,true);											
	}
	void GetInfo()
	{
		string json=File.ReadAllText(Application.persistentDataPath+"/S00.json");
		s=JsonUtility.FromJson<SpawnInfo>(json);
	}

	
}
