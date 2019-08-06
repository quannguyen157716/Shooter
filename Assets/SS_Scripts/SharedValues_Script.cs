﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SharedValues_Script : MonoBehaviour 
{
	//Public Var
	public Text scoreText; 				//GUI Score
	public Text GameOverText; 			//GUI GameOver
	public Text FinalScoreText; 			//GUI Final Score
	public GameObject GameOverPanel;
	//public Text ReplayText; 				//

	//Public Shared Var
	public static int score = 0; 			//Total in-game Score
	public static bool gameover = false; 	//GameOver Trigger

	// Use this for initialization
	void Start () 
	{
		gameover = false; 					//return the Gameover trigger to its initial state when the game restart
		score = 0; 							//return the Score to its initial state when the game restart
	}

	// Fixed Update is called one per specific time
	void Update ()
	{
		scoreText.text = "Score: " + score; 			//Update the GUI Score
		//Excute when the GameOver Trigger is True
		if (gameover == true)
		{
			GameOverPanel.SetActive(true);	
			UICOntroller.UIControllerInstance.ListElements.InGamePanel.SetActive(false);
			GameOverText.text = "GAME OVER"; 			//Show GUI GameOver
			FinalScoreText.text = "" + score; 			//Show GUI FinalScore
		}
	}
}
