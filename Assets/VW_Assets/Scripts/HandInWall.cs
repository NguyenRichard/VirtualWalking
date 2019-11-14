using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInWall : MonoBehaviour
{
    [SerializeField]
    private bool left;
    //Script a appliquer sur un gameObject contenant un Sphere Collider
    //Le booleen isInWall prend la valeur true si cette sphere entre dans un objet possédant le tag wall.

    [SerializeField]
    private bool isInWall = false;
    public EventList eventList;


    void Update()
    {
        if (isInWall)
        {
            if (left)
            {
                eventList.TriggerVibration(40, 1, 255, OVRInput.Controller.LTouch);
            }
            else
            {
                eventList.TriggerVibration(40, 1, 255, OVRInput.Controller.RTouch);
            }


        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            isInWall = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            isInWall = false;
        }
    }
}
