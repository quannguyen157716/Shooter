using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossWeapon
{
	public float MachineGunFireRate;
	public float CentralGunFireRate;
	public int health;
	public float speed;
}

public class Boss_1_Script : MonoBehaviour 
{
	public GameObject RightMachineGun;
	public GameObject LeftMachineGun;
	public GameObject RightCentralGun;
	public GameObject LeftCentralGun;
	public GameObject MachineGunRound;
	public GameObject NormalRound;
	public GameObject LaserBeam;

	BossWeapon weapon;

	public SubBehavior behavior;
	float CentralGunNextFire;
	float MachineGunNextFire;
	Vector3 euler;
	public float MachineGunFireRate;
	public float CentralGunFireRate;
	public int health;
	public float speed;
	bool switchGun=true;
	public Rigidbody2D rigidbody2;
	void Start()
	{
		behavior.Patrol(rigidbody2,3.4f,speed, 150);
	}
	//Update is called once per frame  
	void Update () 
	{
		//transform.position = Vector3.MoveTowards(transform.position, new Vector2(0,-7f), 1.5f * Time.deltaTime);  
		FireMaChineGun(true, true);
		FireCentralGun();
	}

	void FireCentralGun()
	{
		if(Time.time>CentralGunNextFire)
		{
			CentralGunNextFire=Time.time+CentralGunFireRate;

			if(switchGun)
			{
				NormalRound=BossWeaponPool.BossWeaponPoolInstance.GetPooledObject("EnemyShot", RightCentralGun.transform.position, true);
				switchGun=false;
			}
			else
			{
				NormalRound=BossWeaponPool.BossWeaponPoolInstance.GetPooledObject("EnemyShot", LeftCentralGun.transform.position, true);
				switchGun=true;
			}
		}
	}

	void FireMaChineGun(bool LeftGun, bool RightGun)
	{
		if(Time.time > MachineGunNextFire)
		{
			MachineGunNextFire=Time.time+MachineGunFireRate;

			MachineGunRound=BossWeaponPool.BossWeaponPoolInstance.GetPooledObject("MachineGunRound", RightMachineGun.transform.position, false);
			if(MachineGunRound!=null && RightGun)
			{
				MachineGunRound.transform.eulerAngles=RandomRotation(MachineGunRound);
				MachineGunRound.SetActive(true);
			}
			else
			{
				Debug.Log("out of bullet");
				return;
			}
			
			MachineGunRound=BossWeaponPool.BossWeaponPoolInstance.GetPooledObject("MachineGunRound", LeftMachineGun.transform.position, false);
			if(MachineGunRound!=null && LeftGun)
			{
				MachineGunRound.transform.eulerAngles=RandomRotation(MachineGunRound);
				MachineGunRound.SetActive(true);
			}
			else
			{
				Debug.Log("out of bullet");
				return;
			}
		}
	}

	//pass in machinegunRound   
	Vector3 RandomRotation(GameObject obj)
	{
		euler=obj.transform.eulerAngles;
		euler.z=Random.Range(-20f,20f);
		return euler;
	}
}
