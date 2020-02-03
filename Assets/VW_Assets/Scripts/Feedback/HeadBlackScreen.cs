using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBlackScreen : Feedback
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
        cameraInWall.DistMax = DistanceStart;
    }

    [SerializeField]
    private AnimationCurve blackScreenOpacityCurb;
    //Courbe de variation de la transparence de l'écran noir
    public AnimationCurve BlackScreenOpacityCurb
    {
        get { return blackScreenOpacityCurb; }
        set
        {
            blackScreenOpacityCurb = value;
            UpdateBlackScreenOpacityCurb();
        }
    }

    private void UpdateBlackScreenOpacityCurb()
    {
        cameraInWall.OpacityCurb = blackScreenOpacityCurb;
    }

    private CameraInWall cameraInWall;
    private AudioFile sound;

    protected override void InitScene()
    {

        var prefabHeadCollider = Resources.Load<GameObject>("Prefabs/HeadCollider");
        Debug.Assert(prefabHeadCollider, "Couldn't find the HeadCollider prefabs in Assets.");
        GameObject headCollider = Instantiate(prefabHeadCollider, Vector3.zero, Quaternion.identity);
        Debug.Assert(headCollider, "Couldn't instantiate headCollider");
        headCollider.tag = "Head";
        cameraInWall = headCollider.GetComponent<CameraInWall>();
        

        GameObject centerEyeAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        Debug.Assert(centerEyeAnchor, "You must add OVRPlayerController because its child CenterEyeAnchor is needed.");
        headCollider.transform.SetParent(centerEyeAnchor.transform);

        components.Add(headCollider);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        UpdateBlackScreenOpacityCurb();
        UpdateDist();
        SwitchActiveState();
    }

}
