/*
 * FILE:        AboutScene_GUI.cs
 * PROJECT:     Marble Madness - S&GD A1
 * PROGRAMMER:  Eric Lin 7221476
 * DATE:        February 10, 2021
 * DESCRIPTION: This file contains the code that related to the About Scene UI.
*/

using UnityEngine;
using UnityEngine.UI;

public class AboutScene_GUI : MonoBehaviour
{
    public Button backBtn;
    // Start is called before the first frame update
    void Start()
    {
        Button backbtn = backBtn.GetComponent<Button>();
        backbtn.onClick.AddListener(BackBtnOnClick);
    }

    void BackBtnOnClick()
    {
        Application.LoadLevel("StartGUI");
    }
}
