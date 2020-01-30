using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDistToPlayer : MonoBehaviour
{

    public static WallData closestWallLHand;
    public static WallData closestWallRHand;
    public static WallData closestWallHead;

    private Collider leftHandCollider;
    private Collider rightHandCollider;
    private Collider headCollider;


    private Collider colliderWall;
    private BoxCollider thisCollider;

    private float range = 0.5f;

    private Vector3 handLClosestPoint;
    private Vector3 handRClosestPoint;
    private Vector3 headClosestPoint;

    private WallData wallLHand;
    private WallData wallRHand;
    private WallData wallHead;


    private void Start()
    {
        colliderWall = transform.parent.GetComponent<Collider>();
        Vector3 wallScale = colliderWall.bounds.size;
        thisCollider = gameObject.GetComponent<BoxCollider>();
        thisCollider.size = new Vector3(1 + range/wallScale.x,1+range/ wallScale.y, 1+range/ wallScale.z);   
        wallLHand = new WallData();
        wallRHand = new WallData();
        wallHead = new WallData();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "HandL" && leftHandCollider == null)
        {
            leftHandCollider = other.gameObject.GetComponent<Collider>();
        }
        if (other.tag == "HandR" && rightHandCollider == null)
        {
            rightHandCollider = other.gameObject.GetComponent<Collider>();
        }
        if (other.tag == "Head" && headCollider == null)
        {
            headCollider = other.gameObject.GetComponent<Collider>();
        }
    }

    private void OnDrawGizmos()
    {
        if(closestWallLHand == wallLHand && wallLHand != null)
        {
            Gizmos.DrawSphere(wallLHand.WallClosestPoint, 0.005f);
            Gizmos.DrawSphere(handLClosestPoint, 0.005f);
        }
        if (closestWallRHand == wallRHand && wallRHand != null)
        {
            Gizmos.DrawSphere(wallRHand.WallClosestPoint, 0.1f);
            Gizmos.DrawSphere(handRClosestPoint, 0.1f);
        }
        if (closestWallHead == wallHead && wallHead != null)
        {
            Gizmos.DrawSphere(wallHead.WallClosestPoint, 0.1f);
            Gizmos.DrawSphere(headClosestPoint, 0.1f);
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "HandL")
        {
            wallLHand.WallClosestPoint = colliderWall.ClosestPoint(other.gameObject.transform.position);

            handLClosestPoint = leftHandCollider.ClosestPoint(wallLHand.WallClosestPoint);

            wallLHand.Direction = (handLClosestPoint - wallLHand.WallClosestPoint);

            if(closestWallLHand == null || wallLHand.Direction.magnitude < closestWallLHand.Direction.magnitude)
            {
                closestWallLHand = wallLHand;
            }
        }
        if (other.tag == "HandR")
        {
            wallRHand.WallClosestPoint = colliderWall.ClosestPoint(other.gameObject.transform.position);

            handRClosestPoint = rightHandCollider.ClosestPoint(wallRHand.WallClosestPoint);

            wallRHand.Direction = (handRClosestPoint - wallRHand.WallClosestPoint);

            if (closestWallRHand == null || wallRHand.Direction.magnitude < closestWallRHand.Direction.magnitude)
            {
                closestWallRHand = wallRHand;
            }
        }
        if (other.tag == "Head")
        {
            wallHead.WallClosestPoint = colliderWall.ClosestPoint(other.gameObject.transform.position);

            headClosestPoint = headCollider.ClosestPoint(wallHead.WallClosestPoint);

            wallHead.Direction = (headClosestPoint - wallHead.WallClosestPoint);

            if (closestWallHead == null || wallHead.Direction.magnitude < closestWallHead.Direction.magnitude)
            {
                closestWallHead = wallHead;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "HandL")
        {
            if(closestWallLHand == wallLHand)
            {
                closestWallLHand = null;
            }
        }
        if (other.tag == "HandR")
        {
            if (closestWallRHand == wallRHand)
            {
                closestWallRHand = null;
            }
        }
        if (other.tag == "Head")
        {
            if (closestWallHead == wallHead)
            {
                closestWallHead = null;
            }
        }
    }

}
