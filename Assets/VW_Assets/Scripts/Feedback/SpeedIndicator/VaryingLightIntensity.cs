using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaryingLightIntensity : MonoBehaviour
{
    // Ce script a pour but de modifier l'intensité lumineuse d'une lampe (script placé sur la lampe)
    // movingObject est l'objet sur lequel le script SpeedIndicator.cs est appliqué
    SpeedIndicator _speedIndicatorScript;
    Light _varyingLight;
    float _lightIntensity;

    private AnimationCurve _lightIntensityCurb;
    public AnimationCurve LightIntensityCurb
    {
        set { _lightIntensityCurb = value; }
    }

    void Start() 
    {
        _varyingLight = GetComponent<Light>();
        GameObject playerController = GameObject.Find("OVRPlayerController");
        Debug.Assert(playerController, "You must add gameManager because its eventList is needed.");
        _speedIndicatorScript = playerController.GetComponent<SpeedIndicator>();
        Debug.Assert(_speedIndicatorScript, "Could'nt get the EventList from the GameManager");
    }

    void Update()
    {
        _lightIntensity =  _lightIntensityCurb.Evaluate(_speedIndicatorScript.speedIndicator);
        _varyingLight.intensity = _lightIntensity;
            //_speedIndicatorScript.speedIndicator;
    }
}