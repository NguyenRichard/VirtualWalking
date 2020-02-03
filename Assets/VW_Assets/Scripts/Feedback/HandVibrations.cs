using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandVibrations : Feedback
{
    [SerializeField]
    private float distanceStart = 0;
    /// <summary>
    /// Distance max jusqu'à laquelle les vibrations sont déclenchées
    /// </summary>
    public float DistanceStart
    {
        get { return distanceStart; }

        set
        {
            if (distanceStart < 0)
            {
                distanceStart = 0f;
            }
            distanceStart = value;
            UpdateDist();
        }

    }

    //Update la distance dans le script des deux mains
    public void UpdateDist()
    {
        leftHandInWall.DistMax = distanceStart;
        rightHandInWall.DistMax = distanceStart;
    }

    [SerializeField]
    [Range(0, 255)]
    private int intensityInWall = 255;
    //Intensité des vibrations dans les murs
    public int IntensityInWall
    {
        get { return intensityInWall; }

        set
        {
            if (intensityInWall < 0)
            {
                intensityInWall = 0;
            }
            else if (intensityInWall > 255)
            {
                intensityInWall = 255;
            }
            intensityInWall = value;
            UpdateIntensity();
        }

    }

    //Update l'intensité de vibration dans les murs des deux mains
    public void UpdateIntensity()
    {

        rightHandInWall.IntensityInWall = intensityInWall;
        leftHandInWall.IntensityInWall = intensityInWall;

    }

    [SerializeField]
    private AnimationCurve vibrationIntensityCurb;
    //Courbe de variation de l'intensité des vibrations en fonction de la distance
    public AnimationCurve VibrationIntensityCurb
    {
        get { return vibrationIntensityCurb; }
        set {
            vibrationIntensityCurb = value;
            UpdateVibrationIntensityCurb();
        }
    }

    //Update les courbes de variation des vibrations des deux mains
    private void UpdateVibrationIntensityCurb()
    {
        rightHandInWall.IntensityCurb = vibrationIntensityCurb;
        leftHandInWall.IntensityCurb = vibrationIntensityCurb;
    }

    private HandInWall rightHandInWall;
    private HandInWall leftHandInWall;

    protected override void InitScene()
    {
        var prefabHandCollider = Resources.Load<GameObject>("Prefabs/HandCollider");
        Debug.Assert(prefabHandCollider, "Couldn't find the HandCollider prefabs in Assets.");
        GameObject handColliderLeft = Instantiate(prefabHandCollider, Vector3.zero, Quaternion.identity);
        Debug.Assert(handColliderLeft, "Couldn't instantiate handColliderLeft");
        handColliderLeft.tag = "HandL";
        GameObject handColliderRight = Instantiate(prefabHandCollider, Vector3.zero, Quaternion.identity);
        Debug.Assert(handColliderRight, "Couldn't instantiate handColliderRight");
        handColliderRight.tag = "HandR";

        GameObject rightHandAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor");
        Debug.Assert(rightHandAnchor, "You must add OVRPlayerController because its child RightHandAnchor is needed.");
        handColliderRight.transform.SetParent(rightHandAnchor.transform);
        HandInWall handInWallRight = handColliderRight.GetComponent<HandInWall>();
        Debug.Assert(handInWallRight, "Could'nt get the HandInWall from the HandColliderRight");
        handInWallRight.Left = false;
        rightHandInWall = handInWallRight;

        GameObject leftHandAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/LeftHandAnchor");
        Debug.Assert(leftHandAnchor, "You must add OVRPlayerController because its child LeftHandAnchor is needed.");
        handColliderLeft.transform.SetParent(leftHandAnchor.transform);
        HandInWall handInWallLeft = handColliderLeft.GetComponent<HandInWall>();
        Debug.Assert(handInWallLeft, "Could'nt get the HandInWall from the HandColliderLeft");
        handInWallLeft.Left = true;
        leftHandInWall = handInWallLeft;

        components.Add(handColliderLeft);
        components.Add(handColliderRight);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        UpdateVibrationIntensityCurb();
        UpdateDist();
        UpdateIntensity();
    }
}
