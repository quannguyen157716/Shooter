/// <summary>
/// 2D Space Shooter Example
/// By Bug Games www.Bug-Games.net
/// Programmer: Danar Kayfi - Twitter: @DanarKayfi
/// Special Thanks to Kenney for the CC0 Graphic Assets: www.kenney.nl
/// 
/// This is the Explosions Script:
/// - Play Explosion Sound
/// 
/// </summary>

using UnityEngine;
using System.Collections;

public class explosions_script : MonoBehaviour 
{
	AudioSource audio2;
	// Use this for initialization
	void Start () 
	{
		audio2=GetComponent<AudioSource>();
		audio2.Play(); //Play Explosion Sound
		Destroy(gameObject,3);
	}
}
