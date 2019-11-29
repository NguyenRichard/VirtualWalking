using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Classe fournissant les donnees de position et de vitesse filtrées à une fréquence fixe.
/// </summary>
public class DataManager : MonoBehaviour {
    //temps d'échantillonnage à définir dans l'inspecteur

    public Camera Cam { get; private set; }

    public float Timer { get; private set; }

    public Vector3 Position { get; private set; }
    public Vector3 FiltPosition { get; private set; }
    public Vector3 EulerAngles { get; private set; }
    public Quaternion QuaternionAngles { get; private set; }

    // Position
    private const int NBPOSMAX = 1000000;

    public Vector3[] Positions { get; private set; }
    public Vector3[] FiltPositions { get; private set; }

    public ulong NbPos { get; private set; }

    // Speed
    public uint nbSamples = 10;
    private float factor;
    
    private const uint variation = 5;

    public Vector3 SpeedFF { get; private set; }
    public Vector3 SpeedBF { get; private set; }
    public Vector3 SpeedFB { get; private set; }

    public int SpeedFrequency { get; private set; }
    public int SpeedSamples { get; private set; }

    public bool Overflow { get; private set; }
    
    public StepDetector sd;

    public LogSteps ls;

    void Start()
    {
        factor = 1 / (nbSamples * Time.fixedDeltaTime);

        //Position
        FiltPositions = new Vector3[NBPOSMAX];
        Positions = new Vector3[NBPOSMAX];

        Position = new Vector3(0, 0, 0);
        FiltPosition = new Vector3(0, 0, 0);
        Positions[0] = new Vector3(0, 0, 0);
        FiltPositions[0] = new Vector3(0, 0, 0);

        NbPos = 0;

        Cam = Camera.main;
        Timer = 0f;
        Overflow = false;
    }

    void FixedUpdate()
    {
        NbPos++;

        Timer = Timer + Time.deltaTime;

        Position = Cam.transform.position;
        EulerAngles = Cam.transform.eulerAngles;
        QuaternionAngles = Cam.transform.rotation;


        // Enregistrement temporaire de la position

        Positions[NbPos] = Position;
        FiltPosition = Vector3.Lerp(FiltPositions[NbPos - 1], Position, 0.5f);
        FiltPositions[NbPos] = FiltPosition;


        // Calcul de la vitesse

        if (NbPos >= nbSamples + variation)
        {
            SpeedFB = (FiltPositions[NbPos] - FiltPositions[NbPos - nbSamples]) * factor;
        }
        

        if (NbPos >= NBPOSMAX - 1)
        {
            Overflow = true;
        }

        if (sd.isActiveAndEnabled)
        {
            sd.Go();
        }
        ls.Write();
    }

    /*private void OnEnable()
    {
        factor = 1 / (nbSamples * Time.fixedDeltaTime);

        //Position
        FiltPositions = new Vector3[NBPOSMAX];
        Positions = new Vector3[NBPOSMAX];

        Position = new Vector3(0, 0, 0);
        FiltPosition = new Vector3(0, 0, 0);
        Positions[0] = new Vector3(0, 0, 0);
        FiltPositions[0] = new Vector3(0, 0, 0);

        NbPos = 0;

        Cam = Camera.main;
        Timer = 0f;
        Overflow = false;
    }*/
}
