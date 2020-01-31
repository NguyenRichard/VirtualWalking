using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFeedbacks : Feedback
{
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

    private StepDetector stepDetector;

    protected override void InitScene()
    {
        var prefabDataManager = Resources.Load<GameObject>("Prefabs/DataManager");
        Debug.Assert(prefabDataManager, "Couldn't find the VaryingLight prefabs in Assets.");
        GameObject dataManager = Instantiate(prefabDataManager, Vector3.zero, Quaternion.identity);
        Debug.Assert(dataManager, "Couldn't instantiate prefabDataManager");

        GameObject forwardDirection = GameObject.Find("OVRPlayerController/ForwardDirection");
        Debug.Assert(forwardDirection, "You must add OVRPlayerController because it is needed.");

        stepDetector = dataManager.GetComponent<StepDetector>();
        stepDetector.headGameObject = forwardDirection;

        components.Add(dataManager);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        UpdateOffsetY();
        UpdateTimeTreshold();
        UpdateVerticalTreshdold();
        UpdateDistanceTreshold();
        SwitchActiveState();
    }
}
