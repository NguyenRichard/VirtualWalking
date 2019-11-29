using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDetector : MonoBehaviour {

    public GameObject footstepPrefab;

    private List<GameObject> footstepList;
    [SerializeField]
    private int stepNumber = 10;

    public DataManager dm;
    
    public float freq;

    private ulong pos;
    public List<Step> Steps { get; private set; }
    private Step currentStep;

    private Vector2 WalkingDirection;

    public AudioClip impact;
    AudioSource audioSource;

    public float Length { get; private set; }
    public float Distance { get; private set; }

    public float distThreshold = 0.05f; //rayon par frame
    private float sqrDistThreshold;

    public ulong timeThreshold = 7; //puis 14 //nombre de frames a attendre avant de detecter un nouveau pas

    public ulong PosSinceLastStep { get; private set; }
    public List<Step.Side> PastSides { get; private set; }

    public float LastMaxY { get; private set; }
    public float VerticalThreshold = 0; //defaut : 0.003

    public LastLogSteps lls;

    private void Awake()
    {
        freq = 1f / Time.fixedDeltaTime;

        audioSource = GetComponent<AudioSource>();

        sqrDistThreshold = distThreshold * distThreshold;

        PosSinceLastStep = timeThreshold;

        footstepPrefab = Resources.Load<GameObject>("Prefabs/Footstep");

        print("aouak");
    }

    private void OnEnable()
    {
        Debug.Log("pouet");
        pos = dm.NbPos;
        Steps = new List<Step>
        {
            new Step(0, dm.Timer, new Vector2(dm.FiltPositions[pos].x, dm.FiltPositions[pos].z), 0, 0)
        };
        Steps[0].walkingDirection = Vector2.zero;

        WalkingDirection = Vector2.zero;

        Distance = 0;

        LastMaxY = int.MaxValue;

        PastSides = new List<Step.Side>();

        print("enable");
    }

    private void OnDisable()
    {
        lls.Write();
        print("disable");
    }

    public void Go()
    {
        //print("coucou");

        pos++;
        PosSinceLastStep++;

        if (pos >= 2)
        {
            LocalMinXDetection();

            LocalMaxYDetection();

            //Seuil spatial
            if ((new Vector2 (dm.FiltPosition.x, dm.FiltPosition.z) - Steps[Steps.Count - 1].localMinPosition).sqrMagnitude > sqrDistThreshold) {
                //Seuil temporel
                if (PosSinceLastStep >= timeThreshold)
                {

                    //Detection du heel strike
                    if ((dm.FiltPositions[pos - 1].y <= dm.FiltPositions[pos - 2].y && dm.FiltPositions[pos].y > dm.FiltPositions[pos - 1].y) && (LastMaxY - dm.FiltPositions[pos - 1].y >= VerticalThreshold))
                    {
                        //Nouveau pas detecte
                        audioSource.PlayOneShot(impact, 1f);

                        GameObject footstep = Instantiate(footstepPrefab, new Vector3(dm.FiltPositions[pos - 1].x, 0.01f, dm.FiltPositions[pos - 1].z), Quaternion.identity);
                        footstep.transform.rotation = new Quaternion(footstep.transform.rotation.x, Camera.main.transform.rotation.y, footstep.transform.rotation.z, footstep.transform.rotation.w);
                        footstep.transform.Rotate(90, 0, 0);
                        
                        /*if(footstepList.Count > stepNumber)
                        {
                            GameObject toDestroy = footstepList[0];
                            foots tepList.Remove(footstepList[0]);
                            Destroy(toDestroy);

                        }*/

                        currentStep = new Step(pos - 1, dm.Timer - Time.fixedDeltaTime, new Vector2(dm.FiltPositions[pos - 1].x, dm.FiltPositions[pos - 1].z), 0, LastMaxY - dm.FiltPositions[pos - 1].y);

                        LastMaxY = int.MinValue;

                        //Mise a jour de la direction de marche
                        UpdateWalkingDirection();
                        currentStep.walkingDirection = WalkingDirection;

                        //Determination du cote du pas
                        currentStep.side = Side();

                        //Suppression des pas superflus
                        //CleanUp();

                        //Calcul de la longueur du pas
                        Length = StepLength();
                        currentStep.length = Length;
                        
                        Distance += currentStep.length;

                        Steps.Add(currentStep);

                        PosSinceLastStep = 0;
                        PastSides.Clear();
                    }

                }
            }

        }
    }

    /// <summary>
    /// Detection des maximums locaux "verticaux" (y).
    /// </summary>
    private void LocalMaxYDetection()
    {
        if (dm.FiltPositions[pos - 1].y >= dm.FiltPositions[pos - 2].y && dm.FiltPositions[pos].y < dm.FiltPositions[pos - 1].y)
        {
            LastMaxY = (dm.FiltPositions[pos - 1].y > LastMaxY) ? dm.FiltPositions[pos - 1].y : LastMaxY;
        }
    }

    /// <summary>
    /// Detection des minimums locaux "lateraux" (xz).
    /// </summary>
    private void LocalMinXDetection()
    {
        if (WalkingDirection != Vector2.zero)
        {
                if (Side(1) != Step.Side.Left)
                {
                    if (Side() == Step.Side.Left) PastSides.Add(Step.Side.Right);
                }
            if (Side(1) != Step.Side.Right)
            {
                if (Side() == Step.Side.Right) PastSides.Add(Step.Side.Left);
            }
        }
    }

    /// <summary>
    /// Suppression des pas superflus (les faux pasitifs).
    /// </summary>
    private bool CleanUp()
    {
        if (Steps.Count > 2)
        {
            if (currentStep.side == Steps[Steps.Count - 1].side && PastSides.Count == 0)
            {
                Distance -= Steps[Steps.Count - 1].length;
                Steps.RemoveAt(Steps.Count - 1);

                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Returns the "side" of the currentStep's xz speed vector.
    /// </summary>
    private Step.Side Side(ulong i = 0)
    {
        Vector2 n = new Vector2(WalkingDirection.y, -WalkingDirection.x);
        Vector2 speed = new Vector2(dm.FiltPositions[pos - i].x - dm.FiltPositions[pos - i - 1].x, dm.FiltPositions[pos - i].z - dm.FiltPositions[pos - i - 1].z); //ou prendre SpeedBF

        if (Vector2.Dot(n, speed) > 0) return Step.Side.Right;
        else if (Vector2.Dot(n, speed) < 0) return Step.Side.Left;
        else return Step.Side.Unknown;
    }
    
    /// <summary>
    /// Returns the length of the last step
    /// </summary>
    private float StepLength()
    {
        //Vector2 n = new Vector2(WalkingDirection.y, -WalkingDirection.x);
        //Ray2D r = new Ray2D(Steps[Steps.Count - 1].localMinPosition, n);

        ////distance entre le Ray partant de la position du dernier pas, de direction la normale a la direction de marche, et
        ////la position du pas courant (donne la projection sur le vecteur direction, et donc la longueur du pas)
        //return Vector3.Cross(r.direction, currentStep.localMinPosition - r.origin).magnitude;

        //devrait suffire
        float l = Vector2.Dot(WalkingDirection, currentStep.localMinPosition - Steps[Steps.Count - 1].localMinPosition);
        return (l > 0) ? l : 0;
    }

    /// <summary>
    /// Updates the WalkingDirection vector.
    /// </summary>
    private void UpdateWalkingDirection()
    {
        if (Steps.Count <= 2)
        {
            WalkingDirection = (currentStep.localMinPosition - Steps[0].localMinPosition).normalized;
        }
        else
        {
            WalkingDirection = (currentStep.localMinPosition - Steps[Steps.Count - 2].localMinPosition).normalized;

            //
            if(currentStep.side != Steps[Steps.Count - 2].side) print("erreur côté pas " + (Steps.Count - 2) + " pos " + pos);
            //
        }
    }
}
