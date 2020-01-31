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
    private int intensityInWall = 255;
    public int IntensityInWall
    {
        get { return intensityInWall; }

        set
        {
            intensityInWall = value;
        }

    }

    [SerializeField]
    private AnimationCurve intensityCurb;
    public AnimationCurve IntensityCurb
    {
        get { return intensityCurb; }
        set { intensityCurb = value; }
    }

    private float distToWall = 0;

    private float distMax = 0.10f;

    public float DistMax
    {
        get { return distMax; }

        set
        {
            distMax = value;
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
                eventList.TriggerVibration(40, 1, intensityInWall, OVRInput.Controller.LTouch);
            }
            else
            {
                eventList.TriggerVibration(40, 1, intensityInWall, OVRInput.Controller.RTouch);
            }


        }
        else if (left)
        {
            if (WallDistToPlayer.closestWallLHand != null)
            {
                //newIntensity = Vector3.Distance(WallDistToPlayer.closestWallLHand.WallClosestPoint, gameObject.transform.position);
                distToWall = Vector3.Magnitude(WallDistToPlayer.closestWallLHand.Direction);

                // distToWall = Mathf.Lerp(intensityInWall, 0, Mathf.InverseLerp(0, distMax, distToWall));
                //Debug.Log("--------------------------- new intensity hand = " + newIntensity + "---------------------------------------");
                // eventList.TriggerVibration(40, 1, (int)distToWall, OVRInput.Controller.LTouch);

                eventList.TriggerVibration(40, 1, CalculateIntensity(distToWall), OVRInput.Controller.LTouch);
            }
        }
        else
        {
            //Debug.Log("--------------------------- right hand -----------------------------");
            //Debug.Log(WallDistToPlayer.closestWallRHand != null);
            if (WallDistToPlayer.closestWallRHand != null)
            {
                //Debug.Log("---------------------------- wall distance ---------------------");
                // newIntensity = Vector3.Distance(WallDistToPlayer.closestWallRHand.WallClosestPoint, gameObject.transform.position);

                distToWall = Vector3.Magnitude(WallDistToPlayer.closestWallRHand.Direction);


                // distToWall = Mathf.Lerp(intensityInWall, 0, Mathf.InverseLerp(0, distMax, distToWall));
                //Debug.Log("--------------------------- new intensity hand = " + newIntensity + "---------------------------------------");
                // eventList.TriggerVibration(40, 1, (int)distToWall, OVRInput.Controller.RTouch);

                eventList.TriggerVibration(40, 1, CalculateIntensity(distToWall), OVRInput.Controller.RTouch);
            }
        }

    }

    private int CalculateIntensity(float distance)
    {
        //float value = Mathf.Lerp(intensityInWall, 0, Mathf.InverseLerp(0, distMax, distToWall));
        float value = 1 - Mathf.InverseLerp(0, distMax, distToWall);

        value = intensityCurb.Evaluate(value);

        if (value > 1)
        {
            value = 1;
        }
        else if (value < 0)
        {
            value = 0;
        }

        Debug.Log("Intensity=" + Mathf.Lerp(0, intensityInWall, value));

        return (int)Mathf.Lerp(0,intensityInWall,value);
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
