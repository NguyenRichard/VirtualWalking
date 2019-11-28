using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// Classe créant un fichier de log.
/// </summary>
public class LogSteps : MonoBehaviour
{
    public DataManager dm;

    private string path;

    private StepDetector sd;

    void Start()
    {
        sd = GetComponent<StepDetector>();
        
        DateTime now = DateTime.Now;
        path = Path.Combine(Application.persistentDataPath, "Log_" +
            now.Day.ToString("00") + "_" + now.Month.ToString("00") + "_" + now.Year + "_" +
            now.Hour.ToString("00") + "_" + now.Minute.ToString("00") + "_" + now.Second.ToString("00") + "_" +
            sd.freq + "_Hz_STEPS.txt");

        File.WriteAllText(path, "Timer (s);" +
            "X (m);Y (m);Z (m);" +
            "Xf (m);Yf (m);Zf (m);" +
            "Xv (m/s);Yv (m/s);Zv (m/s);" +
            "Pas (bool);DeltaD (m);Distance (m)" +
            Environment.NewLine);

        File.AppendAllText(path, "0;" +
            "0;0;0;" +
            "0;0;0;" +
            "0;0;0;" +
            "0;0;0" +
            Environment.NewLine);
    }

    void Update() //utiliser un buffer par la suite
    {
        // Sauvegarde fichier
        //File.AppendAllText(path, dm.Timer.ToString() + ";" + dm.Position.x + ";" + dm.Position.y + ";" + dm.Position.z + ";" + dm.FiltPosition.x + ";" + dm.FiltPosition.y + ";" + dm.FiltPosition.z + ";" + dm.SpeedFF.x + ";" + dm.SpeedFF.y + ";" + dm.SpeedFF.z + ";" + dm.SpeedBF.x + ";" + dm.SpeedBF.y + ";" + dm.SpeedBF.z + ";" + dm.SpeedFB.x + ";" + dm.SpeedFB.y + ";" + dm.SpeedFB.z + Environment.NewLine);
    }

    public void Write()
    {
        File.AppendAllText(path, dm.Timer.ToString() + ";" +
            dm.Position.x + ";" + dm.Position.y + ";" + dm.Position.z + ";" +
            dm.FiltPosition.x + ";" + dm.FiltPosition.y + ";" + dm.FiltPosition.z + ";" +
            dm.SpeedFB.x + ";" + dm.SpeedFB.y + ";" + dm.SpeedFB.z + ";" +
            (sd.PosSinceLastStep == 0 ? "1" : "0") + ";" + sd.Length + ";" + sd.Distance +
            Environment.NewLine);
    }
}