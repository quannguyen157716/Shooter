using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundButton : MonoBehaviour {
	public Sprite on;
	public Sprite off;
	public Button button;

	// Update is called once per frame
	public void SoundButtonClick()
	{
		if(AudioListener.volume==0)
		{
			AudioListener.volume=1;
			button.image.overrideSprite=on;
		}
		else
		{
			AudioListener.volume=0;
			button.image.overrideSprite=off;
		}
	}
}
