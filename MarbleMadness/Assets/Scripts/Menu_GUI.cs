/*
 * FILE:        Menu_GUI.cs
 * PROJECT:     Marble Madness - S&GD A1
 * PROGRAMMER:  Eric Lin 7221476
 * DATE:        February 10, 2021
 * DESCRIPTION: This file contains the code that related to the Main Menu UI.
*/

using UnityEngine;
using UnityEngine.UI;

public class Menu_GUI : MonoBehaviour
{
	//Global Variables
	public GUIStyle style;

	public Button StartButton;
	public Button InstructionButton;
	public Button AboutButton;
	public Button QuitButton;

	public GameObject ScorePanel;


	// Use this for initialization
	void Start()
	{
		//event handlers
		Button startbtn = StartButton.GetComponent<Button>();
		Button instructionbtn = InstructionButton.GetComponent<Button>();
		Button aboutbtn = AboutButton.GetComponent<Button>();
		Button quitbtn = QuitButton.GetComponent<Button>();


		startbtn.onClick.AddListener(StartOnClick);
		instructionbtn.onClick.AddListener(InstructionbtnOnClick);
		aboutbtn.onClick.AddListener(AboutOnClick);
		quitbtn.onClick.AddListener(QuitOnClick);	


		//start
		if (!PlayerPrefs.HasKey("score") && !PlayerPrefs.HasKey("time"))
        {
			PlayerPrefs.SetInt("score", 0);
			PlayerPrefs.SetFloat("time", 0);
		}

		if (PlayerPrefs.HasKey("score") && PlayerPrefs.HasKey("time"))
        {
			Text buffer = ScorePanel.GetComponentInChildren<Text>();
			buffer.text = "Highest Score: " + PlayerPrefs.GetInt("score").ToString() + "\n  Corresponding Time Left: " + PlayerPrefs.GetFloat("time").ToString();

		}

		Debug.Log("Buttons all set.");
	}


	/*
     * Event handler Functions
    */
	void StartOnClick()
	{
		Application.LoadLevel("MiniGame");
		//SceneManager.LoadScene
	}

	void InstructionbtnOnClick()
	{
		Application.LoadLevel("InstructionGUI");


		//SceneManager.LoadScene
	}

	void AboutOnClick()
	{
		Application.LoadLevel("AboutGUI");
		//SceneManager.LoadScene
	}


	void QuitOnClick()
	{
		Debug.Log("quit");
		Application.Quit();
	}


}
