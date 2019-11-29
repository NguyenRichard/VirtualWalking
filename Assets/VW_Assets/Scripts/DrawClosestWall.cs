using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawClosestWall : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (WallDistToSphereCollider.closestWallLHand != null)
        {
            Debug.DrawRay(WallDistToSphereCollider.closestWallLHand.WallClosestPoint, WallDistToSphereCollider.closestWallLHand.Direction, Color.red);
        }
        if (WallDistToSphereCollider.closestWallRHand != null)
        {
            Debug.DrawRay(WallDistToSphereCollider.closestWallRHand.WallClosestPoint, WallDistToSphereCollider.closestWallRHand.Direction, Color.blue);
        }
        if (WallDistToSphereCollider.closestWallHead != null)
        {
            Debug.DrawRay(WallDistToSphereCollider.closestWallHead.WallClosestPoint, WallDistToSphereCollider.closestWallHead.Direction, Color.green);
        }
    }
}
