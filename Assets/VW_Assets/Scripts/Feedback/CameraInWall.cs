using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInWall : MonoBehaviour
{

    //Script a appliquer sur un gameObject contenant un Sphere Collider
    //Le booleen isInWall prend la valeur true si cette sphere entre dans un objet possédant le tag wall.

    [SerializeField]
    private bool isInWall = false;

    public GameObject blackScreen;

    void Update()
    {
        if (isInWall)
        {
            blackScreen.SetActive(true);
            AudioManager.PlaySFX("InObstaclesWarningSound");
            
        }
        else
        {
            blackScreen.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("----------------------- Collision tête -----------------------------");
        // other.gameObject.GetComponent<Renderer>().enabled = false;
        if (other.gameObject.tag == "wall")
        {
            isInWall = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Renderer>().enabled = true;
        if (other.gameObject.tag == "wall")
        {
            isInWall = false;
        }
    }
}
