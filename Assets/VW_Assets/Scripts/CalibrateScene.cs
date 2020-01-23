using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrateScene : MonoBehaviour
{

    private OVRBoundary boundary;

    [SerializeField]
    private Transform _sceneOrigin;


    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
        {
            _sceneOrigin.position = calculateOrigin();
        }
    }

    private Vector3 calculateOrigin()
    {
        boundary = OVRManager.boundary;
        Vector3 origin = Vector3.zero;

        foreach (var position in boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea))
        {
            origin += position;
        }

        origin /= 4;

        return origin;
    }
}
