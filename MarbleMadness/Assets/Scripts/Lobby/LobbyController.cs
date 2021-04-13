using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public int playerCount = 0;
    public Text hostIP;

    public List<Color> colours;

    public GameObject startGameButton;
    public static Player[] playerDetails;

    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.JoinOrCreateRoom("room", new RoomOptions { MaxPlayers = 4 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        //GameObject item = PhotonNetwork.Instantiate("LobbyPlayer", Vector2.zero, Quaternion.identity);
        //item.transform.parent = GameObject.Find("Players").transform;
        
        //PhotonNetwork.Instantiate("TestCube", new Vector3(Random.Range(-1f,1f), 0, -8.5f), Quaternion.identity);
        //photonView.RPC("CreatePlayer", RpcTarget.All);
        GameObject test = PhotonNetwork.Instantiate("LobbyPlayer", Vector2.zero, Quaternion.identity);
        //test.GetComponent<PhotonView>().Controller.UserId;

        // Update player details
        //Debug.Log(playerList.transform.childCount);

        if (PhotonNetwork.IsMasterClient) {
            startGameButton.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (Player player in PhotonNetwork.PlayerList) {
            //Debug.Log("Name: " + player.CustomProperties["name"]);
            //Debug.Log("Colour: " + player.CustomProperties["colour"]);
        }
    }

    public void StartGame() {
        playerDetails = PhotonNetwork.PlayerList;
        if (PhotonNetwork.IsMasterClient) {
            PhotonNetwork.LoadLevel("MiniGame");
        }
    }
}
