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
	public float WaveWait;					//Time to wait till a new wave f
	public float x_pos, y_pos; //position to spawn 
}
*/
//Stream of enemy(Enemey object, Spawn_Position, Behaviour) 
//player info: playership, score, bullet type, skin

public class GameController_Script : MonoBehaviour 
{	
	public GameObject Boss;
	public GameObject PowerUp;
	public static GameController_Script GameControllerInstance;
	public GameObject Player;
	public AudioSource audios;
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
	}
	//Start the Game
	public void StartGame()
	{
		UICOntroller.UIControllerInstance.ListElements.MainMenuPanel.gameObject.SetActive(false);
		UICOntroller.UIControllerInstance.ListElements.InGamePanel.gameObject.SetActive(true);
		Player.gameObject.SetActive(true);
		LevelBuilder.LevelBuilderInstance.StartLevel();
	}
	
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

	//Random spawn from range of position
	public IEnumerator EnemyRandomSpawn(SpawnInfo ifo)
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
			Debug.Log("CurrentDuration "+duration );
			if(duration>ifo.duration)
			{
				ifo.spawnEnd=true;// not run out of wave
				yield break;
			}
		}
		
		if(duration<ifo.duration)
		{
			yield return new WaitForSeconds(ifo.duration-duration);
			ifo.spawnEnd=true;//run out of wave
		}
		
	}	

	//Spawm PowerUp
	public void SpawnPowerUp()
	{
		Vector3 spawnPosition=new Vector3(Random.Range(-3,3),6);
		Instantiate(PowerUp,spawnPosition,Quaternion.identity);
	}
	//Spawn enemy horizontally
	public IEnumerator EnemyHorizontalRandomSpawn(SpawnInfo ifo)
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
				ifo.spawnEnd=true;
				yield break;
			}
		}
		
		if(duration<ifo.duration)
		{
			yield return new WaitForSeconds(ifo.duration-duration);
			ifo.spawnEnd=true;//run out of wave
		}
	}	

	//wait to spawn an object at specific location after specific time
	public void SingleSpawn(SpawnInfo ifo)
	{
		//Debug.Log("ss"); 
		ObjectPooler.ObjectPoolerInstance.GetPooledObject(ifo.enemyTag, ifo.position,true);	
		ifo.spawnEnd=true;										
	}
	//Spawn boss
	public void BossSpawn()
	{

	}
}
