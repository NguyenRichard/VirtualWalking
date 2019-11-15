using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadInWall  : Feedback
{
    [SerializeField]
    [OnChangedCall("UpdateDist")]
    ///Distance where the black screen start to appear.
    private float dist;
    public float Dist
    {
        get { return dist; }

        set
        {
            dist = value;
            UpdateDist();
        }

    }

    private void UpdateDist()
    {
        throw new NotImplementedException();
    }

    [SerializeField]
    [OnChangedCall("UpdateVolume")]
    ///Distance where the black screen start to appear.
    private float volume;
    public float Volume
    {
        get { return volume; }

        set
        {
            volume = value;
            UpdateVolume();
        }

    }

    private void UpdateVolume()
    {
        if (volume < 0)
        {
            volume = 0;
        }
        else if (volume > 1)
        {
            volume = 1;
        }
        sound.volume = volume;
    }

    AudioFile sound;

    protected override void InitScene()
    {
        sound = AudioManager.Find("InObstaclesWarningSound");
        Debug.Assert(sound != null, "Couldn't find the sound InObstaclesWarningSound");

        UnityEngine.Object prefabHeadCollider = Resources.Load("Assets/VW_Assets/Prefabs/HeadCollider");
        Debug.Assert(prefabHeadCollider, "Couldn't find the HeadCollider prefabs in Assets.");
        GameObject headCollider = (GameObject)GameObject.Instantiate(prefabHeadCollider, Vector3.zero, Quaternion.identity);

        GameObject centerEyeAnchor = GameObject.Find("OVRPlayerController/OVRCameraRig/TrackingSpace/CenterEyeAnchor");
        Debug.Assert(centerEyeAnchor, "You must add OVRPlayerController because its child CenterEyeAnchor is needed.");
        headCollider.transform.SetParent(centerEyeAnchor.transform);

        components.Add(headCollider);

    }


}
