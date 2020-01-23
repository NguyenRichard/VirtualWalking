using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaryingLightIntensity : MonoBehaviour
{
    // Ce script a pour but de modifier l'intensité lumineuse d'une lampe (script placé sur la lampe)
    // movingObject est l'objet sur lequel le script SpeedIndicator.cs est appliqué
    SpeedIndicator speedIndicatorScript;
    Light varyingLight;

    void Start() 
    {
        varyingLight = GetComponent<Light>();
        GameObject playerController = GameObject.Find("OVRPlayerController");
        Debug.Assert(playerController, "You must add gameManager because its eventList is needed.");
        speedIndicatorScript = playerController.GetComponent<SpeedIndicator>();
        Debug.Assert(speedIndicatorScript, "Could'nt get the EventList from the GameManager");
    }

    void Update()
    {
        varyingLight.intensity = speedIndicatorScript.speedIndicator;
    }
}