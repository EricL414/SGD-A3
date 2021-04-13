using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class IgnoreOtherActivity : MonoBehaviour
{
    public MonoBehaviour[] scriptsToIgnore;

    private PhotonView PhotonView;
    // Start is called before the first frame update
    void Start()
    {
        PhotonView = GetComponent<PhotonView>();
        if(!PhotonView.IsMine)
        {
            foreach (var script in scriptsToIgnore)
            {
                script.enabled = false;
            }
        }
    }
}
