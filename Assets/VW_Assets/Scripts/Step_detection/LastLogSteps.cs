using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class LastLogSteps : MonoBehaviour
{
    private string path;

    private StepDetector sd;

    void Start()
    {
        sd = GetComponent<StepDetector>();
        
        DateTime now = DateTime.Now;
        path = Path.Combine(Application.persistentDataPath, "LastLog_" +
            now.Day.ToString("00") + "_" + now.Month.ToString("00") + "_" + now.Year + "_" +
            now.Hour.ToString("00") + "_" + now.Minute.ToString("00") + "_" + now.Second.ToString("00") + "_" +
            "DEF_STEPS.txt");

        File.WriteAllText(path, "Timer (s);" +
            "X (m);Z (m);" +
            "L (m);V (m/s);Side (L/R/U);" +
            "MeanStepLength (m);Distance (m);" +
            "XwalkDir (m);ZwalkDir (m)" +
            Environment.NewLine);
    }

    public void Write()
    {
        
        //Log des pas définitifs
        float d = 0;
        for (int i = 0; i < sd.Steps.Count; i++)
        {
            d += sd.Steps[i].length;
            File.AppendAllText(path, sd.Steps[i].t.ToString() + ";" +
                sd.Steps[i].localMinPosition.x + ";" + sd.Steps[i].localMinPosition.y + ";" +
                sd.Steps[i].length + ";" + ((i > 0) ? (sd.Steps[i].length / (sd.Steps[i].t - sd.Steps[i - 1].t)) : 0).ToString() + ";" + (int)sd.Steps[i].side + ";" +
                ((i > 0) ? (d / i) : 0) + ";" + d + ";" +
                sd.Steps[i].walkingDirection.x + ";" + sd.Steps[i].walkingDirection.y +
                Environment.NewLine);
        }
    }
}