using System.Collections;
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
}
public class UICOntroller : MonoBehaviour {
	public UIElements ListElements;
	public GameObject GameController;
	GameController_Script gameCScript;
	
}
