using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyCommander : MonoBehaviour 
{
	public static EnemyCommander EnemyCommanderInstance;
	public string[] EnemyType;
	//Info of Enemy and their upgrade
	[HideInInspector]
	public Dictionary<string, EnemyInfo> EDictionary;
	[HideInInspector]
	public BossInfo bossInfo;
	void Awake()
	{
		EnemyCommanderInstance=this;
		EDictionary=new Dictionary<string, EnemyInfo>();
		GetEnemyInfo();
	}
	//Get Based enemy info
	void GetEnemyInfo()
	{
		Debug.Log("Enemy info loading");
		string json;
		EnemyInfo ob;
		foreach(string f in EnemyType)
		{
			json=File.ReadAllText(Application.persistentDataPath+"/"+f+".json");
			ob=JsonUtility.FromJson<EnemyInfo>(json);
			EDictionary.Add(f,ob);		
		}

		json=File.ReadAllText(Application.persistentDataPath+"/Boss1.json");
		bossInfo=JsonUtility.FromJson<BossInfo>(json);
		Debug.Log(json);
	}
}
