using UnityEngine;
using System.Collections;

public class explosions_script : MonoBehaviour 
{
	AudioSource audio2;
	// Use this for initialization
	void Start () 
	{
		audio2=GetComponent<AudioSource>();
		audio2.Play(); 
		Destroy(gameObject,3);
	}
}
