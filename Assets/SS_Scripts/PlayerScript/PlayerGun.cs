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

	public int level;
}
public class PlayerGun : MonoBehaviour {
	public static PlayerGun PlayerGunInstance;
	public GameObject shotSpawn;
	public GameObject LshotSpawn;
	public GameObject RshotSpawn;
	GameObject shot;
	[HideInInspector]

	public string shotType="PlayerRegularShot";
	[HideInInspector]
	public string shotDealDamage;//name that enemy will dectect to get hit
	float nextFire=0f;	//First fire & Next fire Time
	float fireRate=1f;	//Fire Rate between Shots
	public int shotDamage;
	//Gun level up? increase rate of fire lv1(starting point) lv2 increase rate fire, level 3:firing extra rounds(no time to do this) 
	public int level=1;
	public string[] Gun;
	Dictionary<string, PlayerWeapon> gunDictionary;//current player's arsenal save all upgrade here
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
		if(level==1)
		Fire1();
		else
		Fire2();
	}
	/* 
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
	}*/

	void Fire1()
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

	void Fire2()
	{
		if(Time.time >nextFire)
		{
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			GameObject shot1 = ObjectPooler.ObjectPoolerInstance.GetPooledObject(shotType, LshotSpawn.transform.position,true); 
			GameObject shot2 = ObjectPooler.ObjectPoolerInstance.GetPooledObject(shotType, RshotSpawn.transform.position,true); 
  			if (shot1 == null ||shot2==null)  
			{
				return;
			}
		}
	}
	//get based guns 
	void GetGunConfig()
	{
		string json;
		PlayerWeapon ob;
		foreach(string f in Gun)
		{
			json=File.ReadAllText(Application.persistentDataPath+"/"+f+".json");
			ob=JsonUtility.FromJson<PlayerWeapon>(json);
			gunDictionary.Add(ob.shotType,ob);
		}
		ob=gunDictionary["PlayerRegularShot"];
		shotType=ob.shotType;
		nextFire=ob.nextFire;
		fireRate=ob.fireRate;
		shotDamage=ob.shotDamage;
		shotDealDamage=shotType;
		level=ob.level;
		Debug.Log("ok");
	}

	public void ChangeShotType(string ShotName)
	{
		PlayerWeapon ob;
		ob=gunDictionary[ShotName];
		shotType=ob.shotType;
		nextFire=ob.nextFire;
		fireRate=ob.fireRate;
		shotDamage=ob.shotDamage;
		shotDealDamage=shotType;
		level=ob.level;
		if(shotType=="PlayerTracingShot")
		shotDealDamage="TracingHead";
	}

	public void UpgradeGun()
	{
		Debug.Log(shotType);
		gunDictionary[shotType].level=level;
	}

}
