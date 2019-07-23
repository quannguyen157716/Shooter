using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
[System.Serializable]
public class PlayerWeapon
{
	public string shotTag;//for prefab
	public float nextFire;	//First fire & Next fire Time
	public float fireRate;	//Fire Rate between Shots
}
public class PlayerGun : MonoBehaviour {
	public GameObject shotSpawn;
	GameObject shot;
	string shotTag="PlayerRegularShot";
	float nextFire=0f;	//First fire & Next fire Time
	float fireRate=1f;	//Fire Rate between Shots
	AudioSource audio2;
	void Start()
	{
		//shotSpawn=GameObject.FindGameObjectWithTag("PlayerShotSpawn");
		audio2=GetComponent<AudioSource>();
		GetGunConfig();
	}

	void Update () 
	{
		Fire();
	}
	void Fire()
	{
		
		if(Time.time >nextFire)
		{
			Debug.Log(fireRate);
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			shot = BulletPooler.SharedBulletPool.GetPooledObject(shotTag); 
  			if (shot != null) 
			{
   			shot.transform.position = shotSpawn.transform.position;
    		shot.transform.rotation = shotSpawn.transform.rotation;
    		shot.SetActive(true);
			audio2.Play();
			}
		}
	}

	void GetGunConfig()
	{
		string json=File.ReadAllText(Application.persistentDataPath+"/PlayerTracingShot.json");
		PlayerWeapon ob=JsonUtility.FromJson<PlayerWeapon>(json);
		shotTag=ob.shotTag;
		nextFire=ob.nextFire;
		fireRate=ob.fireRate;
		shot = BulletPooler.SharedBulletPool.GetPooledObject(shotTag);
		
	}
}
