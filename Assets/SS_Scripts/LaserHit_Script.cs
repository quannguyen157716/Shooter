using UnityEngine;
using System.Collections;

public class LaserHit_Script : MonoBehaviour 
{
	public ParticleSystem p;

	void Update()
	{
		if(p.isStopped)
		gameObject.SetActive(false);
	}
}
