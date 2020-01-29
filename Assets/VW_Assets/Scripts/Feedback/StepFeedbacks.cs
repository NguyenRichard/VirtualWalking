using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepFeedbacks : Feedback
{
    protected override void InitScene()
    {
        var prefabDataManager = Resources.Load<GameObject>("Prefabs/DataManager");
        Debug.Assert(prefabDataManager, "Couldn't find the VaryingLight prefabs in Assets.");
        GameObject dataManager = Instantiate(prefabDataManager, Vector3.zero, Quaternion.identity);
        Debug.Assert(dataManager, "Couldn't instantiate prefabDataManager");

        GameObject forwardDirection = GameObject.Find("OVRPlayerController/ForwardDirection");
        Debug.Assert(forwardDirection, "You must add OVRPlayerController because it is needed.");

        dataManager.GetComponent<StepDetector>().headGameObject = forwardDirection;

        components.Add(dataManager);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        SwitchActiveState();
    }
}
