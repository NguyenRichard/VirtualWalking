using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This script allow the player to recalibrate the scene to the center of the PlayArea.
/// </summary>
public class CalibrateScene : MonoBehaviour
{
    //Play zone area from the Oculus Quest called "Guardian".
    private OVRBoundary boundary;

    //Origin of the scene that need to be calibrated.
    [SerializeField]
    private Transform _sceneOrigin;


    // Update is called once per frame
    void Update()
    {
        //Left controller thumbstick click.
        if (OVRInput.GetDown(OVRInput.Button.PrimaryThumbstick))
        {
            _sceneOrigin.position = calculateOrigin();
        }
    }

    /// <summary>
    /// Calculate the new origin using the play area which is the biggest rectangle that can fit in the Guardian.
    /// </summary>
    /// <returns>Vector3 corresponding to the center of the PlayArea.</returns>
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
