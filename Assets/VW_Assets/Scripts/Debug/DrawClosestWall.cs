using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawClosestWall : MonoBehaviour
{

    [SerializeField]
    LineRenderer lineLeftHand;
    private float newIntensity = 0;

    // Update is called once per frame
    void Update()
    {
        if (WallDistToPlayer.closestWallLHand != null)
        {
            Debug.DrawRay(WallDistToPlayer.closestWallLHand.WallClosestPoint, WallDistToPlayer.closestWallLHand.Direction, Color.red);
            //   newIntensity = Vector3.Distance(WallDistToPlayer.closestWallLHand.WallClosestPoint, gameObject.transform.position);
            newIntensity = Vector3.Magnitude(WallDistToPlayer.closestWallLHand.Direction);
            //Debug.Log("intensity : " + newIntensity + " int intensity : " + (int)newIntensity);
          //  Mathf.Clamp(newIntensity, 0, 0.2f);
            newIntensity = Mathf.Lerp(1, 0, Mathf.InverseLerp(0, 0.2f, newIntensity));
            Debug.Log("intensity : " + newIntensity + " int intensity : " + (int)newIntensity);
            //this.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 1.0f, newIntensity);
        }
        if (WallDistToPlayer.closestWallRHand != null)
        {
            Debug.DrawRay(WallDistToPlayer.closestWallRHand.WallClosestPoint, WallDistToPlayer.closestWallRHand.Direction, Color.blue);
        }
        if (WallDistToPlayer.closestWallHead != null)
        {
            Debug.DrawRay(WallDistToPlayer.closestWallHead.WallClosestPoint, WallDistToPlayer.closestWallHead.Direction, Color.green);
        }
    }


}
