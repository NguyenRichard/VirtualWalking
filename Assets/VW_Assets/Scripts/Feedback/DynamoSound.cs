using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamoSound : Feedback
{
    protected override void InitScene()
    {
        var prefabVaryingDynamo = Resources.Load<GameObject>("Prefabs/VaryingDynamo");
        Debug.Assert(prefabVaryingDynamo, "Couldn't find the VaryingLight prefabs in Assets.");
        GameObject varyingDynamo = Instantiate(prefabVaryingDynamo, Vector3.zero, Quaternion.identity);
        Debug.Assert(varyingDynamo, "Couldn't instantiate prefabVaryingDynamo");

        GameObject playerController = GameObject.Find("OVRPlayerController");
        Debug.Assert(playerController, "You must add OVRPlayerController because it is needed.");
        varyingDynamo.transform.SetParent(playerController.transform);

        components.Add(varyingDynamo);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        SwitchActiveState();
    }
}
