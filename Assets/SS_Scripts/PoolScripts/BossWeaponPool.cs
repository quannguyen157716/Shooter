using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaponPool : MonoBehaviour 
{
	public List<Pool> pools;
	public Dictionary<string, Queue<GameObject>> poolDictionary;
	public static BossWeaponPool BossWeaponPoolInstance;
	void Awake()
	{
		BossWeaponPoolInstance=this;
		poolDictionary=new Dictionary<string, Queue<GameObject>>();
		foreach (Pool p in pools)
		{
			Queue<GameObject> objectPool=new Queue<GameObject>();
			for(int i=0; i< p.amountToPool; i++)
			{
				GameObject obj =Instantiate(p.objectToPool);
				obj.transform.parent=gameObject.transform;
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
