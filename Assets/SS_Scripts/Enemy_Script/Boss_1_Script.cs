using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossInfo
{
	public float MachineGunFireRate;
	public float CentralGunFireRate;
	public float SpreadingGunFireRate;
	public int health;
	public float speed;
	public int ScoreValue;
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
	public GameObject Explosion;

	public GameObject WeaponPool;
	//BossInfo bossIfo;

	public SubBehavior behavior;
	float CentralGunNextFire;
	float MachineGunNextFire;
	float SpreadingGunNextFire;
	Vector3 euler;
	public float MachineGunFireRate;
	public float CentralGunFireRate;
	public float SpreadingGunFireRate;
	public int health;
	public float speed;
	public int ScoreValue;
	bool switchGun=true;
	public Rigidbody2D rigidbody2;

	void OnEnable()
	{
		Instantiate(WeaponPool,transform.position,Quaternion.identity);
		Load();
		behavior.Patrol(rigidbody2,3.8f,speed, 180);
	}
	//Update is called once per frame 
	void Update () 
	{
		//transform.position = Vector3.MoveTowards(transform.position, new Vector2(0,-7f), 1.5f * Time.deltaTime);  
		//FireMaChineGun(true, true); 
		//FireCentralGun(); 
		FireSpreadingGun(); 
	}

	void FireCentralGun()
	{
		if(Time.time>CentralGunNextFire)
		{
			CentralGunNextFire=Time.time+0.5f;

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

	void FireSpreadingGun()
	{
		if(Time.time> SpreadingGunNextFire)
		{
			SpreadingGunNextFire=Time.time+SpreadingGunFireRate;
			for(int i=1; i<6; i++)
			{
				MachineGunRound=BossWeaponPool.BossWeaponPoolInstance.GetPooledObject("MachineGunRound", transform.position, false);
				if(MachineGunRound==null)
				{
					Debug.Log("Out of bullet"); 
					return;
				}
				Vector3 e=MachineGunRound.transform.eulerAngles;
				e.z=-90+(i*30);
				MachineGunRound.transform.eulerAngles=e;
				MachineGunRound.SetActive(true);
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

	//Load boss info
	void Load()
	{
		BossInfo bossIfo=EnemyCommander.EnemyCommanderInstance.bossInfo;
		MachineGunFireRate=bossIfo.MachineGunFireRate;
		CentralGunFireRate=bossIfo.CentralGunFireRate;
		SpreadingGunFireRate=bossIfo.SpreadingGunFireRate;
		health=bossIfo.health;
		speed=bossIfo.speed;
	}
	Vector3 RandomRotation(GameObject obj)
	{
		euler=obj.transform.eulerAngles;
		euler.z=Random.Range(-20f,20f);
		return euler;
	}

	public void OnTriggerEnter2D(Collider2D other)
	{
		if(other.tag == PlayerGun.PlayerGunInstance.shotDealDamage)
		{
			//get shot from pool to fire                                                                                       
			ObjectPooler.ObjectPoolerInstance.GetPooledObject("HitEffect", transform.position,true);
			//Destroy the Other (PlayerShot)
			other.gameObject.SetActive(false);
			
			//Check the Health 
			if(health > 0)
			TakeDamage(PlayerGun.PlayerGunInstance.shotDamage);													
			
			//Check the Health if less or equal 0
			if(health<= 0)
			{
				//Instantiate explosion
				Instantiate (Explosion, transform.position , transform.rotation); 							
				Destruct();												
			}
		}
	}

	public void TakeDamage(int damage)
	{
		health-=damage;
	}
	public void Destruct()
	{
		SharedValues_Script.score +=ScoreValue; 
		Destroy(gameObject);
		BossWeaponPool.BossWeaponPoolInstance.Destroy();
	}
}
