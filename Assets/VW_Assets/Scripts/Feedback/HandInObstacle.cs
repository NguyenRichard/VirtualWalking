using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInObstacle : Feedback
{
    public AnimationCurve curveX;

    ///Volume of the sound when you are in the obstacles.
    [SerializeField]
    [Range(0, 255)]
    private int intensity = 255;
    public int Intensity
    {
        get { return intensity; }

        set
        {
            intensity = value;
            UpdateIntensity();
        }

    }

    public void UpdateIntensity()
    {
        if (intensity < 0)
        {
            intensity = 0;
        }
        else if (intensity > 255)
        {
            intensity = 255;
        }

        components[0].GetComponent<HandInWall>().Intensity = intensity;
        components[1].GetComponent<HandInWall>().Intensity = intensity;

    }

    [SerializeField]
    private float dist = 0;
    public float Dist
    {
        get { return dist; }

        set
        {
            dist = value;
            UpdateDist();
        }

    }

    public void UpdateDist()
    {
        if (dist < 0)
        {
            dist = 0f;
        }
        leftHandInWall.DistMax = dist;
        rightHandInWall.DistMax = dist;
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
        UpdateDist();
        UpdateIntensity();
    }
}
