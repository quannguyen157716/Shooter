﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class UIElements
{
	public Button ReplayButton;
	public Button StartButton;
	public Button SettingsButton;
	public GameObject MainMenuPanel;
	public GameObject InGamePanel;
	public GameObject WeaponPanel;
}
public class UICOntroller : MonoBehaviour 
{
	public static UICOntroller UIControllerInstance;
	public UIElements ListElements;
	
	void Awake()
	{
		UIControllerInstance=this;
	}
}
