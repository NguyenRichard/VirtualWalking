using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightVariation : Feedback
{
    protected override void InitScene()
    {
        var prefabVaryingLight = Resources.Load<GameObject>("Prefabs/VaryingLight");
        Debug.Assert(prefabVaryingLight, "Couldn't find the VaryingLight prefabs in Assets.");
        GameObject varyingLight = Instantiate(prefabVaryingLight, Vector3.zero, Quaternion.identity);
        Debug.Assert(varyingLight, "Couldn't instantiate prefabVaryingLight");

        GameObject playerController = GameObject.Find("OVRPlayerController");
        Debug.Assert(playerController, "You must add OVRPlayerController because it is needed.");
        varyingLight.transform.SetParent(playerController.transform);

        components.Add(varyingLight);

        isInit = true;
        UpdateParameters();

    }

    protected override void UpdateParameters()
    {
        SwitchActiveState();
    }
}
