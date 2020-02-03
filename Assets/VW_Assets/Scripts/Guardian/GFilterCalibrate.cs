using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Guardian modifier that calibrate the custom guardian on the official one on refresh time.
/// </summary>
public class GFilterCalibrate : GFilter
{

    private OVRBoundary boundary;
    private float _refreshTime;
    private float nextUpdate;

    public GFilterCalibrate(GameObject guardian, float refreshTime) : base(guardian)
    {
        _refreshTime = refreshTime;
        nextUpdate = Time.time + _refreshTime;

    }

    public override void Apply()
    {

        if(Time.time > nextUpdate)
        {
            boundary = OVRManager.boundary;

            for (int i = 0; i < boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary).Length; i++)
            {
                Vector3 vec = boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary)[i];
                vec.y = 0;
                _guardianData.Vertices[i] = vec;
                _guardianData.Vertices[i+boundary.GetGeometry(OVRBoundary.BoundaryType.OuterBoundary).Length] = vec;

            }

            nextUpdate = Time.time + _refreshTime;
           
        }
    }
}
