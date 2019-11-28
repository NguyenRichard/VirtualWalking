using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawClosestWall : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {

        if(WallDistance.closestWallRHand != null)
        {
            Debug.DrawRay(WallDistance.closestWallRHand.MyClosestPoint, WallDistance.closestWallRHand.Direction, Color.red);
        }
        if (WallDistance.closestWallLHand != null)
        {
            Debug.DrawRay(WallDistance.closestWallLHand.MyClosestPoint, WallDistance.closestWallLHand.Direction, Color.red);
        }
        if (WallDistance.closestWallHead != null)
        {
            Debug.DrawRay(WallDistance.closestWallHead.MyClosestPoint, WallDistance.closestWallHead.Direction, Color.red);
        }
    }
}
