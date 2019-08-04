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

public class Boss_1_Script : MonoBehaviour {
	public GameObject RightMachineGun;
	public GameObject LeftMachineGun;
	public GameObject RightCentralMachineGun;
	public GameObject LeftCentralMachineGun;
	public GameObject MachineGunRound;
	public GameObject NormalRound;
	public GameObject LaserBeam;

	public BossWeapon weapon;

	public SubBehavior behavior;
	void OnEnable () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}



	void FireMaChineGun()
	{

	}
}
