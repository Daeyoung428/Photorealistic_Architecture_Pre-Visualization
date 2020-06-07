using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject UI;

    public OVRInput.Controller controller;
    //public OVRInput.Button menuButton;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyUp("space"))
        {
            ToggleUI();
        }
#endif
        if(OVRInput.GetDown(OVRInput.RawButton.Back, controller))
        {
            ToggleUI();
        }
    }


    void ToggleUI()
    {
        if (UI.activeInHierarchy == false)
        {
            UI.SetActive(true);
        }
        else
        {
            UI.SetActive(false);
        }
    }
}
