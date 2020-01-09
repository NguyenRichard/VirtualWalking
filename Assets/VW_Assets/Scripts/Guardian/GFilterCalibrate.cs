using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GFilterCalibrate : GFilter
{

    private OVRBoundary boundary;
    private Transform _sceneOrigin;
    private float _refreshTime;
    private float nextUpdate;

    public GFilterCalibrate(GameObject guardian, float refreshTime, Transform sceneOrigin) : base(guardian)
    {
        _refreshTime = refreshTime;
        nextUpdate = Time.time + _refreshTime;
        _sceneOrigin = sceneOrigin;

    }

    private Vector3 calculateOrigin()
    {
        Vector3 origin = Vector3.zero;

        foreach(var position in boundary.GetGeometry(OVRBoundary.BoundaryType.PlayArea))
        {
            origin += position;
        }

        origin /= 4;

        return origin;
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

            _sceneOrigin.position = calculateOrigin();

            nextUpdate = Time.time + _refreshTime;
           
        }
    }
}
