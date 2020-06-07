using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnvironmentSwap : MonoBehaviour
{
    [Header("Reference Toggle")]
    public Toggle day_NightToggle;

    [Header("List of Day Objects")]
    public List<GameObject> dayObjects = new List<GameObject>();

    [Header("Day Skybox")]
    public Material daySkybox;

    [Header("Night Objects")]
    public List<GameObject> nightObjects = new List<GameObject>();

    [Header("Night Skybox")]
    public Material nightSkybox;


    void Update()
    {
     if (day_NightToggle.GetComponent<Toggle>().isOn == true)
        {
            foreach(GameObject dayObjects in dayObjects)
            {
                dayObjects.SetActive(true);
            }

            foreach (GameObject nightObjects in nightObjects)
            {
                nightObjects.SetActive(false);
            }
            RenderSettings.skybox = daySkybox;
            //skybox.material = daySkybox;
        }
        else
        {
            foreach (GameObject nightObjects in nightObjects)
            {
                nightObjects.SetActive(true);
            }
            foreach (GameObject dayObjects in dayObjects)
            {
                dayObjects.SetActive(false);
            }
            RenderSettings.skybox = nightSkybox;
        }
    }
}