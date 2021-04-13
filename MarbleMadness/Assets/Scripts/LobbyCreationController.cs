using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyCreationController : MonoBehaviour
{
    public InputField inputIP;
    public static string ip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CreateLobby() {
        ip = null;
        SceneManager.LoadScene("Lobby");
    }

    public void JoinLobby() {
        ip = inputIP.text;
        SceneManager.LoadScene("Lobby");
    }
}
