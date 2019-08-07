using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeShot : MonoBehaviour {
	public string shot;
	//public Button b;
	public void ChangeGun()
	{
		PlayerGun.PlayerGunInstance.ChangeShotType(shot);
		Debug.Log("changeGun");
	}
}
