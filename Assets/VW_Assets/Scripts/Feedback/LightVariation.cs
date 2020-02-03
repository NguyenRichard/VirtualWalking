using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVariation : Feedback
{

    [SerializeField]
    private AnimationCurve _lightIntensityCurb;
    public AnimationCurve LightIntensityCurb
    {
        get { return _lightIntensityCurb; }
        set
        {
            _lightIntensityCurb = value;
        }
    }

    public void UpdateLightIntensityCurb()
    {
        varyingLightIntensity.LightIntensityCurb = _lightIntensityCurb;
    }


    private VaryingLightIntensity varyingLightIntensity;

    protected override void InitScene()
    {
        var prefabVaryingLight = Resources.Load<GameObject>("Prefabs/VaryingLight");
        Debug.Assert(prefabVaryingLight, "Couldn't find the VaryingLight prefabs in Assets.");
        GameObject varyingLight = Instantiate(prefabVaryingLight, Vector3.zero, Quaternion.identity);
        varyingLight.transform.SetParent(GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor").transform);
        Debug.Assert(varyingLight, "Couldn't instantiate prefabVaryingLight");

        GameObject centerEyeAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        Debug.Assert(centerEyeAnchor, "You must add OVRPlayerController because its child CenterEyeAnchor is needed.");
        varyingLight.transform.SetParent(centerEyeAnchor.transform);

        varyingLightIntensity = varyingLight.GetComponent<VaryingLightIntensity>();

        components.Add(varyingLight);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        UpdateLightIntensityCurb();
        SwitchActiveState();
    }
}
