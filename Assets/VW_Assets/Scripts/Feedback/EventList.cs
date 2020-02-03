using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

//classe contenant une méthode permettant de déclencher les vibrations des manettes.
public class EventList : MonoBehaviour
{
    /// <summary>
    /// Méthode permettant de déclencher les vibrations des manettes.
    /// </summary>
    /// <param name="iteration">Le nombre d'itérations de vibrations déclenchées lors d'un appel de la méthode</param>
    /// <param name="frequency">Fréquence des vibrations</param>
    /// <param name="strength">Intensité des vibrations</param>
    /// <param name="controller">Controller dont il faut déclencher les vibrations</param>
    public void TriggerVibration(int iteration, int frequency, int strength, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip();
        //pour chaque itération
        for (int i = 0; i < iteration; i++)
        {
            //programme une vibration
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);
        }
        //déclenche les vibrations sur le bon controller
        if (controller == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            OVRHaptics.RightChannel.Preempt(clip);
        }
    }

}
