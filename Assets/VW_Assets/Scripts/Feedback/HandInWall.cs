using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInWall : MonoBehaviour
{


    [SerializeField]
    private bool left = false;
    //Booléen indiquant si il s'agit de la main gauche ou droite
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
    //Intensité des vibrations dans le mur
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
    //Courbe de variation de l'intensité des vibrations
    public AnimationCurve IntensityCurb
    {
        get { return intensityCurb; }
        set { intensityCurb = value; }
    }

    private float distToWall = 0;

    private float distMax = 0.10f;
    //Distance max de déclenchement des vibrations
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

    //Récupère les variables necessaire au fonctionnement du feedback
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
                distToWall = Vector3.Magnitude(WallDistToPlayer.closestWallLHand.Direction);
                eventList.TriggerVibration(40, 1, CalculateIntensity(distToWall), OVRInput.Controller.LTouch);
            }
        }
        else
        {
            if (WallDistToPlayer.closestWallRHand != null)
            {
                distToWall = Vector3.Magnitude(WallDistToPlayer.closestWallRHand.Direction);
                eventList.TriggerVibration(40, 1, CalculateIntensity(distToWall), OVRInput.Controller.RTouch);
            }
        }

    }

    //calcule l'intensité des vibrations en fonction de la distance.
    private int CalculateIntensity(float distance)
    {
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
