using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShot : MonoBehaviour 
{
	public string shot;
	
	public void ChangeGun()
	{
		PlayerGun.PlayerGunInstance.ChangeShotType(shot);
		Debug.Log("changeGun");
	}
}
