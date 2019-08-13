using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class SharedValues_Script : MonoBehaviour 
{
	public Text scoreText; 				
	public Text GameOverText; 			
	public Text FinalScoreText; 			
	public GameObject GameOverPanel;
	public static int score = 0; 			
	public static bool gameover = false; 	

	// Use this for initialization
	void Start () 
	{
		gameover = false; 					
		score = 0; 							
	}

	// Fixed Update is called one per specific time 
	void Update ()
	{
		scoreText.text = "Score: " + score; 			
		if (gameover == true)
		{
			GameOverPanel.SetActive(true);	
			UICOntroller.UIControllerInstance.ListElements.InGamePanel.SetActive(false);
			GameOverText.text = "GAME OVER"; 			
			FinalScoreText.text = "" + score; 			
		}
	}
}
