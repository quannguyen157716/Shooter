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
	string shotType="PlayerRegularShot";
	float nextFire=0f;	//First fire & Next fire Time
	float fireRate=1f;	//Fire Rate between Shots
	public int shotDamage;
	AudioSource audio2;
	void Start()
	{
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
		string json=File.ReadAllText(Application.persistentDataPath+"/PlayerTracingGun.json");
		PlayerWeapon ob=JsonUtility.FromJson<PlayerWeapon>(json);
		shotType=ob.shotType;
		nextFire=ob.nextFire;
		fireRate=ob.fireRate;
		shotDamage=ob.shotDamage;
		//shot = BulletPooler.SharedBulletPool.GetPooledObject(shotType);
	}
}
