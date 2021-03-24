using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyUserController : MonoBehaviour
{
    LobbyController lobbyController;
    public GameObject colourButtonHolder;

    // Start is called before the first frame update
    void Start()
    {
        lobbyController = GameObject.FindGameObjectWithTag("LobbyController").GetComponent<LobbyController>();

        // Setup colours
        List<GameObject> buttonList = new List<GameObject>();
        foreach (RectTransform child in colourButtonHolder.transform) {
            buttonList.Add(child.gameObject);
        }
        for (int i = 0; i < lobbyController.colours.Count; i++) {
            buttonList[i].GetComponent<Image>().color = lobbyController.colours[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetColour(int index) {
        Debug.Log(index);
    }
}
