using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class LobbyUserController : MonoBehaviourPun
{
    LobbyController lobbyController;
    public GameObject colourButtonHolder;
    public RawImage selectedColour;
    public int id;

    // Name
    public string playerName;
    public InputField inputName;
    public Text outputText;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(GameObject.Find("Players").transform);

        lobbyController = GameObject.FindGameObjectWithTag("LobbyController").GetComponent<LobbyController>();

        // Setup colours
        List<GameObject> buttonList = new List<GameObject>();
        foreach (RectTransform child in colourButtonHolder.transform) {
            buttonList.Add(child.gameObject);
        }
        for (int i = 0; i < lobbyController.colours.Count; i++) {
            buttonList[i].GetComponent<Image>().color = lobbyController.colours[i];
        }

        if (!photonView.IsMine) {
            HideEdit();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerName != inputName.text) {
            playerName = inputName.text;
            //PhotonNetwork.LocalPlayer.CustomProperties["name"] = playerName;
            //photonView.Controller.CustomProperties["name"] = playerName;
            photonView.RPC("SetNameRPC", RpcTarget.All, inputName.text);
        }
    }

    public void SetColour(int index) {
        photonView.RPC("SetColourRPC", RpcTarget.All, index);
    }

    [PunRPC]
    void SetColourRPC(int index, PhotonMessageInfo info) {
        Color c = lobbyController.colours[index];
        selectedColour.color = c;

        info.Sender.CustomProperties["colour"] = index;
    }

    [PunRPC]
    void SetNameRPC(string playerName, PhotonMessageInfo info) {
        outputText.text = playerName;

        //if (!photonView.IsMine) {
        info.Sender.CustomProperties["name"] = playerName;
        //}
    }

    public void HideEdit() {
        inputName.gameObject.SetActive(false);
        colourButtonHolder.SetActive(false);
    }
}
