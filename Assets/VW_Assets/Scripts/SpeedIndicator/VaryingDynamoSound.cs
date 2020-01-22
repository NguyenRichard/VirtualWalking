using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpeedIndicator))]
public class VaryingDynamoSound : MonoBehaviour
{
    SpeedIndicator _speedIndicatorScript;
    AudioSource _audioSource;
    [SerializeField] AudioClip _dynamoAudioClip;
    //float _varyingVolume;
    //float _minVolume;

    void Start()
    {
        _speedIndicatorScript = gameObject.GetComponent<SpeedIndicator>();
        _audioSource = gameObject.AddComponent<AudioSource>();
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
