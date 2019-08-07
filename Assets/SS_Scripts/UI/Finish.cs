using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour {
	public void Conntinue()
	{
		UICOntroller.UIControllerInstance.ListElements.WeaponPanel.SetActive(false);
	}
}
