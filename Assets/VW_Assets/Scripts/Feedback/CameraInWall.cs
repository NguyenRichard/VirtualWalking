using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraInWall : MonoBehaviour
{

    //Script a appliquer sur un gameObject contenant un Sphere Collider
    //Le booleen isInWall prend la valeur true si cette sphere entre dans un objet possédant le tag wall.

    [SerializeField]
    private bool isInWall = false;

    private float timeInWall = 0;
    public float TimeInWall
    {
        get { return timeInWall; }
    }

    private float startTimeInWall = 0;

    int numberOfInWall = 0;

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
        if (other.gameObject.tag == "wall")
        {
            numberOfInWall++;
            if (numberOfInWall == 1)
            {
                startTimeInWall = Time.time;
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
                timeInWall += (Time.time - startTimeInWall);
                startTimeInWall = 0;
            }
        }
    }


    public void resetTimeInWall()
    {
        timeInWall = 0;
    }
}
