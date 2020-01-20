using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateCollisions : MonoBehaviour
{

    [SerializeField]
    private bool isInWall = false;

    private float timeInWall = 0;
    public float TimeInWall
    {
        get { return timeInWall; }
    }

    private int numberOfCollision = 0;
    public int NumberOfCollision
    {
        get { return numberOfCollision; }
    }

    private float startTimeInWall = 0;

    int numberOfInWall = 0;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            numberOfInWall++;
            if (numberOfInWall == 1)
            {
                startTimeInWall = Time.time;
                isInWall = true;
                numberOfCollision++;
            }
        }
    }


    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            numberOfInWall--;
            if (numberOfInWall == 0)
            {
                isInWall = false;
                timeInWall += (Time.time - startTimeInWall);
                startTimeInWall = 0;
            }
        }
    }

    public void reset()
    {
        timeInWall = 0;
        numberOfCollision = 0;
    }
}
