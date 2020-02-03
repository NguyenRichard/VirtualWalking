using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VaryingDynamoSound : MonoBehaviour
{
    SpeedIndicator _speedIndicatorScript;
    AudioSource _audioSource;
    [SerializeField] AudioClip _dynamoAudioClip;


    void Start()
    {
        GameObject playerController = GameObject.Find("OVRPlayerController");
        Debug.Assert(playerController, "You must add gameManager because its eventList is needed.");
        _speedIndicatorScript = playerController.GetComponent<SpeedIndicator>();
        Debug.Assert(_speedIndicatorScript, "Could'nt get the EventList from the GameManager");
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.clip = _dynamoAudioClip;
        _audioSource.loop = true;
        _audioSource.Play();
    }

    void Update()
    {
        _audioSource.volume = _speedIndicatorScript.speedIndicator;
        // Envisager l'utilisation de pitch au lieu de volume
    }
}
