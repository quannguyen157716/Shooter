using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class EnemyCommander : MonoBehaviour {
	public static EnemyCommander EnemyCommanderInstance;
	public string[] EnemyType;
	[HideInInspector]
	public Dictionary<string, EnemyInfo> EDictionary;//Enemy upgrade goes here
	void Awake()

	{
		EnemyCommanderInstance=this;
		EDictionary=new Dictionary<string, EnemyInfo>();
		GetEnemyInfo();
	}
	//Get Based enemy info
	void GetEnemyInfo()
	{
		string json;
		EnemyInfo ob;
		foreach(string f in EnemyType)
		{
			json=File.ReadAllText(Application.persistentDataPath+"/"+f+".json");
			ob=JsonUtility.FromJson<EnemyInfo>(json);
			EDictionary.Add(f,ob);		
		}
	}
}
