/*
 * FILE:        PlayerController.cs
 * PROJECT:     Marble Madness - S&GD A1
 * PROGRAMMER:  Eric Lin 7221476
 * DATE:        February 10, 2021
 * DESCRIPTION: This file contains the codes that related to the marble controller.
*/

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    //Global Variables
    public float speed = 10;

    private Rigidbody rb;
    private float movementX;
    private float movementY;

    private int count;
    //public TextMeshProUGUI countText;
    //public TextMeshProUGUI timerText;
    //public GameObject winPanel;

    private int current_level;
    private float[] covermovemnt = { 80,150,80 };
    //public GameObject level1cover;
    //public GameObject level2cover;
    //public GameObject level3cover;

    //public AudioSource collectaudio;
    //public AudioSource fallingaudio;

    private float timer;
    private bool timerflag;

    //public GameObject level1;
    //public GameObject level2;
    //public GameObject level3;
    //public Button TiltBtn;
    //public Text TiltText;
    //public Button DevJumpBtn;

    private bool status = false;
    private bool moveflag = false;

    private PhotonView PhotonView;
    //======= Prefab ===============
    private GameObject countText;
    private GameObject timerText;
    private GameObject winPanel;
    private GameObject level1cover;
    private GameObject level2cover;
    private GameObject level3cover;

    private GameObject collectaudio;
    private GameObject fallingaudio;
    private GameObject level1;
    private GameObject level2;
    private GameObject level3;
    private GameObject TiltBtn;
    private GameObject TiltText;
    private GameObject DevJumpBtn;

    private GameObject Camera;
    private Vector3 offset;


    // Start is called before the first frame update
    void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        

        countText = GameObject.Find("Canvas/CountText");
        timerText = GameObject.Find("Canvas/TimerPanel/TimerText");
        winPanel = GameObject.Find("Canvas/WinPanel");
        level1cover = GameObject.Find("Level1/Grounds/hole1cover");
        level2cover = GameObject.Find("Level2/Grounds/hole2cover");
        level3cover = GameObject.Find("Level3/Grounds/hole3cover");
        collectaudio = GameObject.Find("CollectAudio");
        fallingaudio = GameObject.Find("FallingAudio");
        level1 = GameObject.Find("Level1");
        level2 = GameObject.Find("Level2");
        level3 = GameObject.Find("Level3");
        TiltBtn = GameObject.Find("Canvas/TiltBtn");
        TiltText = GameObject.Find("Canvas/TiltBtn/Text");
        DevJumpBtn = GameObject.Find("Canvas/DevPanel/DevJumpBtn");

        Camera = GameObject.Find("Main Camera");

        offset = Camera.transform.position - transform.position;



        rb = GetComponent<Rigidbody>();
        count = 0;
        current_level = 1;

        SetCountText();
        winPanel.SetActive(false);
        timer = 90;
        timerflag = true;

        //event handlers
        Button tiltbtn = TiltBtn.GetComponent<Button>();
        tiltbtn.onClick.AddListener(TiltBtnOnClick);

        Button devjumpbtn = DevJumpBtn.GetComponent<Button>();
        devjumpbtn.onClick.AddListener(LevelJumpOnClick);

    }

    //Keyboard Inputs handler
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;

    }

    /*
     * FUNCTION:    void SetCountText()
     * DESCRIPTION: Updating the count box and detect if the player wins the game
    */
    void SetCountText()
    {
        if (PhotonView.IsMine)
        {
            countText.GetComponent<TextMeshProUGUI>().text = "Count: " + count.ToString();

        }

        if (count >= 30)
        {
            Text winText = winPanel.GetComponentInChildren<Text>();
            winText.text = "You collected all 30 pickups and win the game!";
            winPanel.SetActive(true);
            timerflag = false;
            Time.timeScale = 0;
            if(PlayerPrefs.GetInt("score")<= count && PlayerPrefs.GetFloat("time") <= timer) // && PlayerPrefs.GetFloat("time")<=timer
            {
                Debug.Log(PlayerPrefs.GetFloat("time").ToString());
                PlayerPrefs.SetInt("score", count);
                PlayerPrefs.SetFloat("time", timer);
            }

        }
    }

    void FixedUpdate()
    {
        if(timerflag==true) //check the timer
        {
            timer -= Time.deltaTime;
            timerText.GetComponent<TextMeshProUGUI>().text = "Time Left: " + timer.ToString("0.00");
            if (timer < 0)  //run out of the time
            {
                Time.timeScale = 0;
                winPanel.SetActive(true);
                Text outTimeText = winPanel.GetComponentInChildren<Text>();
                outTimeText.text = "Run out of time. Please try again";
                if (PlayerPrefs.GetInt("score") <= count) 
                {
                    Debug.Log(count.ToString());
                    PlayerPrefs.SetInt("score", count);
                    PlayerPrefs.SetFloat("time", timer);
                }
            }
        }
        
        //marble moving 
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rb.AddForce(movement * speed);

        //Hole cover movement on each level
        if (count == 10)
        {
            if (covermovemnt[0] >= 0)
            {
                level1cover.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
                covermovemnt[0] -= 1;
            }

        }
        if (count == 20)
        {
            if (covermovemnt[1] >= 0)
            {
                level2cover.transform.Translate(new Vector3(1, 0, 0) * Time.deltaTime);
                covermovemnt[1] -= 1;
            }

        }

        // Level Tilting
        if(moveflag == true)
        {
            if (status == true)
            {
                TiltText.GetComponent<Text>().text = "Click me again to restore!";
                level1.transform.Rotate(new Vector3(-25, 0, 0) * Time.deltaTime);
                level2.transform.Rotate(new Vector3(-25, 0, 0) * Time.deltaTime);
                level3.transform.Rotate(new Vector3(-25, 0, 0) * Time.deltaTime);
                if (level1.transform.rotation.eulerAngles.x < 335)
                {
                    moveflag = false;
                }
            }
            else
            {
                TiltText.GetComponent<Text>().text = "There is no spoon! ";
                level1.transform.Rotate(new Vector3(25, 0, 0) * Time.deltaTime);
                level2.transform.Rotate(new Vector3(25, 0, 0) * Time.deltaTime);
                level3.transform.Rotate(new Vector3(25, 0, 0) * Time.deltaTime);
                if (level1.transform.rotation.eulerAngles.x >359)
                {
                    moveflag = false;
                }
            }
        }
        
    }

    void LateUpdate()
    {
        Camera.transform.position = transform.position + offset;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))   //pick up event
        {
            collectaudio.GetComponent<AudioSource>().Play();
            other.gameObject.SetActive(false);
            count++;
            SetCountText();

        }

        if(other.gameObject.CompareTag("level1_pass"))  //level passing
        {
            fallingaudio.GetComponent<AudioSource>().Play();
            current_level++;
        }

        if(other.gameObject.CompareTag("GameFailedPass"))   //marble runs out of the boundry
        {
            winPanel.SetActive(true);
            Text failedText = winPanel.GetComponentInChildren<Text>();
            failedText.text = "You Failed. Left the surfaces. Do not move too fast while tilting";
            Time.timeScale = 0;
            if (PlayerPrefs.GetInt("score") <= count)
            {
                PlayerPrefs.SetInt("score", count);
                PlayerPrefs.SetFloat("time", timer);
            }
        }
    
    }

    /*
     * **********Event handler Functions**********
    */
    void LevelJumpOnClick()
    {
        if(current_level==1)
        {
            current_level++;
            count = 10;
            countText.GetComponent<TextMeshProUGUI>().text = "Count: " + count.ToString();
            transform.position = new Vector3(0, -37, 0);
            
        }
        else if(current_level==2)
        {
            current_level++;
            count = 20;
            countText.GetComponent<TextMeshProUGUI>().text = "Count: " + count.ToString();
            transform.position = new Vector3(0, -72, 0);
            
        }
    }

    void TiltBtnOnClick()
    {
        if (status == false)
        {
            status = true;
            moveflag = true;
        }
        else
        {
            status = false;
            moveflag = true;
        }
    }




}
