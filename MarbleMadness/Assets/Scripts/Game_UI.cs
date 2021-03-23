/*
 * FILE:        Game_UI.cs
 * PROJECT:     Marble Madness - S&GD A1
 * PROGRAMMER:  Eric Lin 7221476
 * DATE:        February 10, 2021
 * DESCRIPTION: This file contains the code that related to the Game scene UI.
*/

using UnityEngine;
using UnityEngine.UI;

public class Game_UI : MonoBehaviour
{
    //Global Variables
    public GameObject menuPanel;
    public Button menuButton;
    public Button DevButton;
    public Button WinRestartButton;
    public Button WinBackButton;
    public Button closePanelButton;
    public GameObject DevPanel;
    public Button DevCloseButton;


    // Start is called before the first frame update
    void Start()
    {
        //event handlers
        Button menubtn = menuButton.GetComponent<Button>();
        Button devbtn = DevButton.GetComponent<Button>();
        Button winrestartbtn = WinRestartButton.GetComponent<Button>();
        Button winbackbtn = WinBackButton.GetComponent<Button>();
        Button closebtn = closePanelButton.GetComponent<Button>();
        Button devclosebtn = DevCloseButton.GetComponent<Button>();

        menubtn.onClick.AddListener(MenuOnClick);
        devbtn.onClick.AddListener(DevOnClick);
        winrestartbtn.onClick.AddListener(WinRestartOnClick);
        winbackbtn.onClick.AddListener(WinBackOnClick);
        closebtn.onClick.AddListener(CloseOnClick);
        devclosebtn.onClick.AddListener(DevCloseOnClick);

        closebtn.gameObject.SetActive(false);
        DevPanel.SetActive(false);


    }


    /*
     * Event handler Functions
    */
    void CloseOnClick()
    {
        menuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    void MenuOnClick()
    {
        menuPanel.SetActive(true);
        Text pasueText = menuPanel.GetComponentInChildren<Text>();
        pasueText.text = "Game Paused";
        //closebtn.gameObject.SetActive(false);
        Button closebtn = closePanelButton.GetComponent<Button>();
        closebtn.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    void DevOnClick()
    {
        DevPanel.SetActive(true);
    }

    void WinRestartOnClick()
    {
        Time.timeScale = 1;
        Application.LoadLevel("MiniGame");
    }

    void WinBackOnClick()
    {
        Time.timeScale = 1;
        Application.LoadLevel("StartGUI");
    }

    void DevCloseOnClick()
    {
        DevPanel.SetActive(false);
    }


}
