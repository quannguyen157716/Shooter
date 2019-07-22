using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PlayerShot
{
	public string shotTag;//for prefab
	public int damage;
	public float speed;
}
public class PlayerGun : MonoBehaviour {
	PlayerShot Regular;
	GameObject shotSpawn;
	GameObject shot;
	public float nextFire;	//First fire & Next fire Time
	public float fireRate;	//Fire Rate between Shots
	AudioSource audio2;
	void Start()
	{
		shotSpawn=GameObject.FindGameObjectWithTag("PlayerShotSpawn");
		audio2=GetComponent<AudioSource>();
	}

	void Update () 
	{
		Fire();
	}
	void Fire()
	{
		if(Time.time >nextFire)
		{
			nextFire = Time.time + fireRate;//fire after ''firerate'' time from the time of last frame
			shot = BulletPooler.SharedBulletPool.GetPooledObject("PlayerRegularShot"); 
  			if (shot != null) 
			{
   			shot.transform.position = shotSpawn.transform.position;
    		shot.transform.rotation = shotSpawn.transform.rotation;
    		shot.SetActive(true);
			audio2.Play();
			}
		}
	}
}
