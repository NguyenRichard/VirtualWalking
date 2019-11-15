using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadInObstacles  : Feedback
{
    ///Distance where the black screen start to appear.
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
        throw new NotImplementedException();
    }

    ///Volume of the sound when you are in the obstacles.
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float volume = 0;
    public float Volume
    {
        get { return volume; }

        set
        {
            volume = value;
            UpdateVolume();
        }

    }

    public void UpdateVolume()
    {
        if (volume < 0)
        {
            volume = 0f;
        }
        else if (volume > 1)
        {
            volume = 1f;
        }

        sound.source.volume = volume;

    }

    private AudioFile sound;

    protected override void InitScene()
    {
        sound = AudioManager.Find("InObstaclesWarningSound");
        Debug.Assert(sound!=null, "Couldn't find the sound InObstaclesWarningSound");

        var prefabHeadCollider = Resources.Load<GameObject>("Prefabs/HeadCollider");
        Debug.Assert(prefabHeadCollider, "Couldn't find the HeadCollider prefabs in Assets.");
        GameObject headCollider = Instantiate(prefabHeadCollider, Vector3.zero, Quaternion.identity);
        Debug.Assert(headCollider, "Couldn't instantiate headCollider");

        GameObject centerEyeAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        Debug.Assert(centerEyeAnchor, "You must add OVRPlayerController because its child CenterEyeAnchor is needed.");
        headCollider.transform.SetParent(centerEyeAnchor.transform);

        components.Add(headCollider);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        // UpdateDist();
        UpdateVolume();
        SwitchActiveState();
    }

}
