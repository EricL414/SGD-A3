using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;

public class LobbyController : MonoBehaviour
{
    public GameObject playerList;
    public int playerCount = 0;
    public Text hostIP;

    public List<Color> colours;

    // Start is called before the first frame update
    void Start()
    {
        // Host Lobby
        if (LobbyCreationController.ip == null) {
            HostGame();
            DisplayIP();
        }
        // Join the ip of the given lobby
        else {
            JoinGame(LobbyCreationController.ip);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        
    }

    void HostGame() {

    }

    void JoinGame(string ip) {

    }

    private void DisplayIP() {
        var host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (var ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                hostIP.text = ip.ToString();
            }
        }
    }
}
