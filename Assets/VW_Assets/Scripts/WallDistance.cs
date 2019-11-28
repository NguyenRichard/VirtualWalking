using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDistance : MonoBehaviour
{

    public static WallDistance closestWallLHand;
    public static WallDistance closestWallRHand;
    public static WallDistance closestWallHead;

    Collider leftHandCollider;
    Collider rightHandCollider;
    Collider headCollider;

    Collider colliderWall;

    Vector3 myClosestPoint;
    public Vector3 MyClosestPoint
    {
        get { return myClosestPoint; }
    }
    Vector3 handClosestPoint;

    private Vector3 direction;
    public Vector3 Direction
    {
        get { return direction; }
    }


    private void Start()
    {
        colliderWall = transform.parent.GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" && leftHandCollider == null)
        {
            leftHandCollider = other.gameObject.GetComponent<Collider>();
        }
    }

    private void OnTriggerStay(Collider other)
    {

        if (other.tag == "Hand")
        {
            myClosestPoint = leftHandCollider.ClosestPoint(other.gameObject.transform.position);

            handClosestPoint = colliderWall.ClosestPoint(myClosestPoint);

            direction = (handClosestPoint - myClosestPoint);

            if(closestWallLHand == null || direction.magnitude < closestWallLHand.Direction.magnitude)
            {
                closestWallLHand = this;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            if(closestWallLHand == this)
            {
                closestWallLHand = null;
            }
        }
    }

}
