using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPoolItem 
{
	public GameObject objectToPool;
	public int amountToPool;
	public bool ExpandPool=false;
}
[System.Serializable]
public class Pool
{
	public string tag;
	public GameObject objectToPool;
	public int amountToPool;
}
public class ObjectPooler : MonoBehaviour 
{
	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;
	public static ObjectPooler ObjectPoolerInstance;

	void Awake()
	{
		ObjectPoolerInstance=this;
		poolDictionary=new Dictionary<string, Queue<GameObject>>();
		foreach (Pool p in pools)
		{
			Queue<GameObject> objectPool=new Queue<GameObject>();
			for(int i=0; i< p.amountToPool; i++)
			{
				GameObject obj =Instantiate(p.objectToPool);
				obj.SetActive(false);
				objectPool.Enqueue(obj);
			}
			poolDictionary.Add(p.tag, objectPool);
		}
	}
	
	public GameObject GetPooledObject(string tag, Vector2 position, bool Enable)
	{
		//Debug.Log(tag+": " +poolDictionary[tag].Count);
		if(!poolDictionary.ContainsKey(tag) || poolDictionary[tag].Count==0)
		{
			Debug.Log("Out of obj");
			return null; 
		}
	
		GameObject obj= poolDictionary[tag].Dequeue();
		if(!obj.activeInHierarchy)
		{
			obj.transform.position=position;
			obj.transform.rotation=Quaternion.identity;
			if(Enable)
			obj.SetActive(true);
			poolDictionary[tag].Enqueue(obj);
			return obj;
		}
		else
		{
			poolDictionary[tag].Enqueue(obj);
			return null;
		}
	}
}
