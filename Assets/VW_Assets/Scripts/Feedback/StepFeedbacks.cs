using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFeedbacks : Feedback
{
    //Horizontal variation treshold between the generation of two steps.
    [SerializeField]
    private float horizontalTreshold;
    public float HorizontalTreshold
    {
        get { return horizontalTreshold; }
        set {
            horizontalTreshold = value;
            UpdateDistanceTreshold();
        }
    }

    public void UpdateDistanceTreshold()
    {
        stepDetector.distThreshold = horizontalTreshold;
    }

    //Vertical variation treshold between the generation of two steps.
    [SerializeField]
    private float verticalTreshold;
    public float VerticalTreshold
    {
        get { return verticalTreshold; }
        set
        {
            verticalTreshold = value;
            UpdateVerticalTreshdold();
        }
    }

    private void UpdateVerticalTreshdold()
    {
        stepDetector.VerticalThreshold = verticalTreshold;
    }

    //Time treshold between the generation of two steps.
    [SerializeField]
    private ulong timeTreshold;
    public ulong TimeTreshold
    {
        get { return timeTreshold; }
        set
        {
            timeTreshold = value;
            UpdateTimeTreshold();
        }
    }

    private void UpdateTimeTreshold()
    {
        stepDetector.timeThreshold = timeTreshold;
    }

    //Offset on Y coordinate for the position of the step sprite.
    [SerializeField]
    private float offsetY;
    public float OffsetY
    {
        get { return offsetY; }
        set
        {
            offsetY = value;
            UpdateOffsetY();
        }
    }

    private void UpdateOffsetY()
    {
        stepDetector.offsetY = offsetY;
    }

    ///Volume of the steps.
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float soundVolume = 0;
    public float SoundVolume
    {
        get { return soundVolume; }

        set
        {
            soundVolume = value;
            UpdateVolume();
        }

    }

    public void UpdateVolume()
    {
        if (soundVolume < 0)
        {
            soundVolume = 0f;
        }
        else if (soundVolume > 1)
        {
            soundVolume = 1f;
        }
        sound.volume = soundVolume;
    }

    private StepDetector stepDetector;
    private AudioSource sound;

    protected override void InitScene()
    {
        var prefabDataManager = Resources.Load<GameObject>("Prefabs/DataManager");
        Debug.Assert(prefabDataManager, "Couldn't find the VaryingLight prefabs in Assets.");
        GameObject dataManager = Instantiate(prefabDataManager, Vector3.zero, Quaternion.identity);
        Debug.Assert(dataManager, "Couldn't instantiate prefabDataManager");


        GameObject forwardDirection = GameObject.Find("OVRPlayerController/ForwardDirection");
        Debug.Assert(forwardDirection, "You must add OVRPlayerController because it is needed.");

        stepDetector = dataManager.GetComponent<StepDetector>();
        Debug.Assert(stepDetector, "You must add a StepDetector to DataManager.");
        stepDetector.headGameObject = forwardDirection;
        sound = dataManager.GetComponent<AudioSource>();
        Debug.Assert(sound, "You must add an AudioSource to DataManager.");
        components.Add(dataManager);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        UpdateVolume();
        UpdateOffsetY();
        UpdateTimeTreshold();
        UpdateVerticalTreshdold();
        UpdateDistanceTreshold();
        SwitchActiveState();
    }
}
