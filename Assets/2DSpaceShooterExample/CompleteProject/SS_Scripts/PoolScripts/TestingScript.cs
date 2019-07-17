using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class AnOb
{
	public int a;
}
[System.Serializable]
public class TestingS{
	public int level; 
	public int numberOfEnemy;	
}

public class TestingScript : MonoBehaviour {

	TestingS myOb= new TestingS();
	void Start () {
		myOb.level=1;
		myOb.numberOfEnemy=6;
		string j=JsonUtility.ToJson(myOb,true);
		Debug.Log(j);
		Debug.Log(Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath+"/myOb.json", j);
		string content=File.ReadAllText(Application.persistentDataPath+"/myOb.json");
		myOb=JsonUtility.FromJson<TestingS>(content);
		Debug.Log(Application.persistentDataPath);
		//Debug.Log(content);
		//Debug.Log(myOb.level);
		Debug.Log(content.Length);
		Debug.Log(myOb.numberOfEnemy);
		/* File.WriteAllText(); 
//https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
//https://stackoverflow.com/questions/52141800/unity-serializing-object-to-json

//
// serialize JSON directly to a file
		using (StreamWriter file = File.CreateText(@"c:\movie.json"))
{
    JsonSerializer serializer = new JsonSerializer();
    	serializer.Serialize(file, movie);
}*/
	}
	void WriteToJson()
	{

	}
	// Update is called once per frame
	void Update () {
		
	}
}
