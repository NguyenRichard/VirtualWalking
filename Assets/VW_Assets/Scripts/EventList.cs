using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus;

public class EventList : MonoBehaviour
{

    public AudioClip sound;
    AudioSource audioSource;
    OVRInput.Controller LeftController;
    OVRInput.Controller RightController;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        /*LeftController = OVRInput.Controller.LTouch;
        RightController = OVRInput.Controller.RTouch;*/
    }

    /*private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            MakeSound();
            TriggerVibration(40, 2, 255, LeftController);
            TriggerVibration(40, 2, 255, RightController);
        }
    }*/

    public void MakeSound()
    {
        audioSource.PlayOneShot(sound, 0.25f);
    }

    public void TriggerVibration(int iteration, int frequency, int strength, OVRInput.Controller controller)
    {
        OVRHapticsClip clip = new OVRHapticsClip();

        for(int i = 0; i < iteration; i++)
        {
            clip.WriteSample(i % frequency == 0 ? (byte)strength : (byte)0);
        }

        if(controller == OVRInput.Controller.LTouch)
        {
            OVRHaptics.LeftChannel.Preempt(clip);
        }
        else if (controller == OVRInput.Controller.RTouch)
        {
            OVRHaptics.RightChannel.Preempt(clip);
        }

    }

}
