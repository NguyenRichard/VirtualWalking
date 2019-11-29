using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallData
{

    Vector3 wallClosestPoint;
    public Vector3 WallClosestPoint
    {
        get { return wallClosestPoint; }
        set { wallClosestPoint = value; }
    }

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public WallData()
    {
        wallClosestPoint = Vector3.zero;
        direction = Vector3.zero;
    }
}
