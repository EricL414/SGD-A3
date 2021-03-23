using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TiltBtn_script : MonoBehaviour
{

    public GameObject level1;
    public GameObject level2;
    public GameObject level3;
    public Button TiltBtn;
    public Text TiltText;
    private bool status=false;

    // Start is called before the first frame update
    void Start()
    {
        Button tiltbtn = TiltBtn.GetComponent<Button>();
        tiltbtn.onClick.AddListener(TiltBtnOnClick);
    }

    void TiltBtnOnClick()
    {
        if (status == false)
        {
            level1.transform.Rotate(new Vector3(-25, 0, 0));
            level2.transform.Rotate(new Vector3(-25, 0, 0));
            level3.transform.Rotate(new Vector3(-25, 0, 0));

            TiltText.text = "Click me again to restore!";
            status = true;
        }
        else
        {
            level1.transform.rotation = new Quaternion(0, 0, 0, 0);
            level2.transform.rotation = new Quaternion(0, 0, 0, 0);
            level3.transform.rotation = new Quaternion(0, 0, 0, 0);

            TiltText.text = "Try me!";
            status = false;
        }
    }
}
