﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInWall : MonoBehaviour
{

    //Script a appliquer sur un gameObject contenant un Sphere Collider
    //Le booleen isInWall prend la valeur true si cette sphere entre dans un objet possédant le tag wall.

    private float distMax = 0.20f;

    public float DistMax
    {
        get { return distMax; }

        set
        {
            distMax = value;
        }
    }

    [SerializeField]
    private bool isInWall = false;
    private float newIntensity = 0;

    int numberOfInWall = 0;

    public GameObject blackScreen;

    void Update()
    {
        if (isInWall)
        {
            //Debug.Log("IN THE WALL");
            blackScreen.SetActive(true);
            AudioManager.PlaySFX("InObstaclesWarningSound");
            Color baseColor = blackScreen.GetComponent<Renderer>().material.color;
            baseColor.a = 1;
            blackScreen.GetComponent<Renderer>().material.color = baseColor;
        }
        
        else if(WallDistToPlayer.closestWallHead != null)
        {
            blackScreen.SetActive(true);
            newIntensity = Vector3.Distance(WallDistToPlayer.closestWallHead.WallClosestPoint, gameObject.transform.position) - 0.10f;
            //Debug.Log("-------------------- Intensity head = " + newIntensity + " ----------------------------");
            //Debug.Log("******************** Inverse Lerp = " + Mathf.InverseLerp(0, distMax, newIntensity) + "***********************************");
            newIntensity = Mathf.Lerp(1, 0, Mathf.InverseLerp(0, distMax, newIntensity));
            //Debug.Log("-------------------- Intensity head = " + newIntensity + " ----------------------------");
            Color baseColor = blackScreen.GetComponent<Renderer>().material.color;
            baseColor.a = newIntensity;
            blackScreen.GetComponent<Renderer>().material.color = baseColor;
            
        }
        else
        {
            blackScreen.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            numberOfInWall++;
            if (numberOfInWall == 1)
            {
                other.gameObject.GetComponent<Renderer>().enabled = false;
                isInWall = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            numberOfInWall--;
            other.gameObject.GetComponent<Renderer>().enabled = true;
            if (numberOfInWall == 0)
            {
                isInWall = false;
            }
        }
    }
}
