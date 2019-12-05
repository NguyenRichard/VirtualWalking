using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawClosestWall : MonoBehaviour
{

    [SerializeField]
    LineRenderer lineLeftHand;


    // Update is called once per frame
    void Update()
    {
        if (WallDistToPlayer.closestWallLHand != null)
        {
            //Debug.DrawRay(WallDistToPlayer.closestWallLHand.WallClosestPoint, WallDistToPlayer.closestWallLHand.Direction, Color.red);

        }
     /*   if (WallDistToPlayer.closestWallRHand != null)
        {
            Debug.DrawRay(WallDistToPlayer.closestWallRHand.WallClosestPoint, WallDistToPlayer.closestWallRHand.Direction, Color.blue);
        }
        if (WallDistToPlayer.closestWallHead != null)
        {
            Debug.DrawRay(WallDistToPlayer.closestWallHead.WallClosestPoint, WallDistToPlayer.closestWallHead.Direction, Color.green);
        }*/
    }

}
