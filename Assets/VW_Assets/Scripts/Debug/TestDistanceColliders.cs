using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDistanceColliders : MonoBehaviour
{

    [SerializeField]
    Collider innerCollider;

    Transform transformWall;
    Collider colliderWall;

    Vector3 myClosestPoint;
    Vector3 wallClosestPoint;

    private bool calculate = false;

    //Si on calcule la distance, cherche la distance entre les deux points les plus proches des 2 colliders
    private void Update()
    {

        if (calculate)
        {
            Debug.Assert(innerCollider);

            wallClosestPoint = colliderWall.ClosestPoint(transform.position);

            myClosestPoint = innerCollider.ClosestPoint(wallClosestPoint);

            Vector3 dir = (wallClosestPoint - myClosestPoint);

            Debug.DrawRay(myClosestPoint, dir, Color.red);
        }
    }

    //Si on rentre dans le collider,commence à calculer la distance
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "wall")
        {
            transformWall = other.gameObject.transform;
            colliderWall = other.gameObject.GetComponent<Collider>();
            calculate = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        calculate = false;
    }


}
