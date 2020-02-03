using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Classe permettant à chaque mur d'update les Walldata
public class WallDistToPlayer : MonoBehaviour
{
    //Stocke les walldata des murs les plus proches des deux mains et de la tête
    public static WallData closestWallLHand;
    public static WallData closestWallRHand;
    public static WallData closestWallHead;

    //Stocke les colliders des deux mains et de la tête
    private Collider leftHandCollider;
    private Collider rightHandCollider;
    private Collider headCollider;

    //Collider du mur
    private Collider colliderWall;
    //Collider de detection de la distance
    private BoxCollider thisCollider;

    //Taille du collider de detection
    private float range = 0.5f;

    //Point du mur le plus proche des deux mains et de la tête
    private Vector3 handLClosestPoint;
    private Vector3 handRClosestPoint;
    private Vector3 headClosestPoint;

    //Walldate du mur correspondant à ce script
    private WallData wallLHand;
    private WallData wallRHand;
    private WallData wallHead;

    //prépare les différentes variales pour la mesure (crée des WallData et va chercher les Colliders du mur)
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

    //Si une des deux mains ou la tête entre dans le collider de mesure, les stocke
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

    //Dans l'éditeur unity, affiche une sphere au niveau des points le plus proches.
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

    //tant qu'une des mains ou la tête reste dans le collider de mesure, update le WallData static si c'est le mur associé à ce script qui est le plus proche.
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

    //Si l'une des mains quitte le collider, la supprime des variables pour arreter de mesurer sa distance au mur
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
