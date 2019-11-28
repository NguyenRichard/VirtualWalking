using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInWall : MonoBehaviour
{


    [SerializeField]
    private bool left = false;
    public bool Left
    {
        get { return left; }

        set
        {
            left = value;
        }

    }

    [SerializeField]
    private int intensity = 255;
    public int Intensity
    {
        get { return intensity; }

        set
        {
            intensity = value;
        }

    }

    //Script a appliquer sur un gameObject contenant un Sphere Collider
    //Le booleen isInWall prend la valeur true si cette sphere entre dans un objet possédant le tag wall.

    [SerializeField]
    private bool isInWall = false;
    public EventList eventList;

    private void Start()
    {
        GameObject gameManager = GameObject.Find("GameManager");
        Debug.Assert(gameManager, "You must add gameManager because its eventList is needed.");
        eventList = gameManager.GetComponent<EventList>();
        Debug.Assert(eventList, "Could'nt get the EventList from the GameManager");
    }

    void Update()
    {
        if (isInWall)
        {
            if (left)
            {
                eventList.TriggerVibration(40, 1, intensity, OVRInput.Controller.LTouch);
            }
            else
            {
                eventList.TriggerVibration(40, 1, intensity, OVRInput.Controller.RTouch);
            }


        }
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("------------------------- Collision ---------------------------------");
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
