using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

public class AnOb
{
	public int a;
}
[System.Serializable]
public class TestingS{
	public Vector3 position;
	public int health;

}

public class TestingScript : MonoBehaviour {

	void Start () {
		/* myOb.position=new Vector3(1,1,3);
		myOb.health=15;

		string json=JsonUtility.ToJson(myOb,true);
		Debug.Log(json);
		Debug.Log(Application.persistentDataPath);
		File.WriteAllText(Application.persistentDataPath+"/SaveFile.json",json);*/
		string json=File.ReadAllText(Application.persistentDataPath+"/SaveFile.json");
		TestingS myOb=JsonUtility.FromJson<TestingS>(json);
		Debug.Log(myOb.position);
		Debug.Log(myOb.health); 
//https://stackoverflow.com/questions/36239705/serialize-and-deserialize-json-and-json-array-in-unity
//https://stackoverflow.com/questions/52141800/unity-serializing-object-to-json

//
// serialize JSON directly to a file
	}
	void WriteToJson()
	{

	}
	// Update is called once per frame
	void Update () {
		
	}
}
