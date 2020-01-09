using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInWall : MonoBehaviour
{

    //Script a appliquer sur un gameObject contenant un Sphere Collider
    //Le booleen isInWall prend la valeur true si cette sphere entre dans un objet possédant le tag wall.

    [SerializeField]
    private bool isInWall = false;
    private float newIntensity = 0;

    public GameObject blackScreen;

    void Update()
    {
        if (isInWall)
        {
            blackScreen.SetActive(true);
            AudioManager.PlaySFX("InObstaclesWarningSound");
            
        }
        else if(WallDistToPlayer.closestWallHead != null)
        {
            newIntensity = Vector3.Distance(WallDistToPlayer.closestWallLHand.WallClosestPoint, gameObject.transform.position);
            newIntensity = Mathf.Lerp(0, 1, Mathf.InverseLerp(0, 1.5f, newIntensity));
            Color baseColor = blackScreen.GetComponent<Renderer>().material.color;
            blackScreen.GetComponent<Renderer>().material.color = new Color(baseColor.r,baseColor.g,baseColor.b,newIntensity);
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
            other.gameObject.GetComponent<Renderer>().enabled = false;
            isInWall = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            other.gameObject.GetComponent<Renderer>().enabled = true;
            isInWall = false;
        }
    }
}
