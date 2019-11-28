using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInObstacle : Feedback
{
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

    protected override void InitScene()
    {
        var prefabHandCollider = Resources.Load<GameObject>("Prefabs/HandCollider");
        Debug.Assert(prefabHandCollider, "Couldn't find the HandCollider prefabs in Assets.");
        GameObject handColliderLeft = Instantiate(prefabHandCollider, Vector3.zero, Quaternion.identity);
        Debug.Assert(handColliderLeft, "Couldn't instantiate handColliderLeft");
        GameObject handColliderRight = Instantiate(prefabHandCollider, Vector3.zero, Quaternion.identity);
        Debug.Assert(handColliderRight, "Couldn't instantiate handColliderRight");
       
        GameObject rightHandAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/RightHandAnchor");
        Debug.Assert(rightHandAnchor, "You must add OVRPlayerController because its child RightHandAnchor is needed.");
        handColliderRight.transform.SetParent(rightHandAnchor.transform);
        HandInWall handInWallRight = handColliderRight.GetComponent<HandInWall>();
        Debug.Assert(handInWallRight, "Could'nt get the HandInWall from the HandColliderRight");
        handInWallRight.Left = false;

        GameObject leftHandAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/LeftHandAnchor");
        Debug.Assert(leftHandAnchor, "You must add OVRPlayerController because its child LeftHandAnchor is needed.");
        handColliderLeft.transform.SetParent(leftHandAnchor.transform);
        HandInWall handInWallLeft = handColliderLeft.GetComponent<HandInWall>();
        Debug.Assert(handInWallLeft, "Could'nt get the HandInWall from the HandColliderLeft");
        handInWallLeft.Left = true;

        components.Add(handColliderLeft);
        components.Add(handColliderRight);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        UpdateIntensity();
    }
}
