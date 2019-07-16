using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
		string j=JsonUtility.ToJson(myOb);
		Debug.Log(j);

		File.WriteAllText(Application.persistentDataPath, j);

		/* File.WriteAllText();

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
