using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaryingLightIntensity : MonoBehaviour
{
    // Ce script a pour but de modifier l'intensité lumineuse d'une lampe (script placé sur la lampe)
    // movingObject est l'objet sur lequel le script SpeedIndicator.cs est appliqué
    [SerializeField] GameObject movingObject;
    SpeedIndicator speedIndicatorScript;
    Light varyingLight;

    void Start() 
    {
        varyingLight = GetComponent<Light>();
        speedIndicatorScript = movingObject.GetComponent<SpeedIndicator>();
    }

    void Update()
    {
        varyingLight.intensity = 1.0f * speedIndicatorScript.speedIndicator;
    }
}