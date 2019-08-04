using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class PlayerWeapon
{
	public string shotType;//for prefab
	public float nextFire;	//First fire & Next fire Time
	public float fireRate;	//Fire Rate between Shots
	public int shotDamage;
	public float shotSpeed;
}
public class PlayerGun : MonoBehaviour {
	public static PlayerGun PlayerGunInstance;
	public GameObject shotSpawn;
	GameObject shot;
	[HideInInspector]
	public string shotType="PlayerRegularShot";
	float nextFire=0f;	//First fire & Next fire Time
	float fireRate=1f;	//Fire Rate between Shots
	public int shotDamage;
	public string[] Gun;
	Dictionary<string, PlayerWeapon> gunDictionary;
	AudioSource audio2;
	void Awake()
	{
		gunDictionary=new Dictionary<string, PlayerWeapon>();
		PlayerGunInstance=this;
		audio2=GetComponent<AudioSource>();
		GetGunConfig();
	}

	void Update () 
	{
		Fire2();
	}
	void Fire()
	{
		
		if(Time.time >nextFire)
		{
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			shot = BulletPooler.SharedBulletPool.GetPooledObject(shotType); 
  			if (shot != null) 
			{
   			shot.transform.position = shotSpawn.transform.position;
    		shot.transform.rotation = shotSpawn.transform.rotation;
    		shot.SetActive(true);
			audio2.Play();
			}
			else
			return;
		}
	}

	void Fire2()
	{
		if(Time.time >nextFire)
		{
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			shot = ObjectPooler.ObjectPoolerInstance.GetPooledObject(shotType, shotSpawn.transform.position,true); 
  			if (shot != null) 
			{
				audio2.Play();
			}
			else
			return;
		}
	}
	void GetGunConfig()
	{
		string json;
		PlayerWeapon ob;
		foreach(string f in Gun)
		{
			json=File.ReadAllText(Application.persistentDataPath+"/"+f+".json");
			ob=JsonUtility.FromJson<PlayerWeapon>(json);
			gunDictionary.Add(f,ob);
		}
		ob=gunDictionary["PlayerRegularGun"];
		shotType=ob.shotType;
		nextFire=ob.nextFire;
		fireRate=ob.fireRate;
		shotDamage=ob.shotDamage;
		Debug.Log("ok");
	}
}
