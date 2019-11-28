using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInObstacle : Feedback
{
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
    }
}
