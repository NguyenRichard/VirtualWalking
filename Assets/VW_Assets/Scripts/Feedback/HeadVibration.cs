using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadVibration  : Feedback
{
    ///Distance where the black screen start to appear.
    [SerializeField]
    private float distanceStart = 0;
    public float DistanceStart
    {
        get { return distanceStart; }

        set
        {
            distanceStart = value;
            UpdateDist();
        }

    }

    public void UpdateDist()
    {
        if (distanceStart < 0)
        {
            distanceStart = 0f;
        }
        _vibrationSound.DistMax = DistanceStart;
    }



    private VibrationSound _vibrationSound;


    protected override void InitScene()
    {
        var prefabVibrationCollider = Resources.Load<GameObject>("Prefabs/VibrationCollider");
        Debug.Assert(prefabVibrationCollider, "Couldn't find the VibrationCollider prefabs in Assets.");
        GameObject vibrationCollider = Instantiate(prefabVibrationCollider, Vector3.zero, Quaternion.identity);
        Debug.Assert(vibrationCollider, "Couldn't instantiate VibrationCollider");
        vibrationCollider.tag = "Head";
        _vibrationSound = vibrationCollider.GetComponent<VibrationSound>();
        

        GameObject centerEyeAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        Debug.Assert(centerEyeAnchor, "You must add OVRPlayerController because its child CenterEyeAnchor is needed.");
        vibrationCollider.transform.SetParent(centerEyeAnchor.transform);

        components.Add(vibrationCollider);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        UpdateDist();
        SwitchActiveState();
    }

}
