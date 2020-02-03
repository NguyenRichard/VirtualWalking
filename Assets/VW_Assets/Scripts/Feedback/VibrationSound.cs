using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VibrationSound : MonoBehaviour
{

    //Script a appliquer sur un gameObject contenant un Sphere Collider
    //Le booleen isInWall prend la valeur true si cette sphere entre dans un objet possédant le tag wall.

    private float distMax = 0.20f;
    //Distance max de déclenchement du son de vibrations
    public float DistMax
    {
        get { return distMax; }

        set
        {
            distMax = value;
        }
    }

    [SerializeField]
    private bool isInWall = false;
    private float newIntensity = 0;

    //nombre de murs avec lesquels la tête est en collisions
    int numberOfInWall = 0;

    AudioSource _audioSource;
    [SerializeField] AudioClip _vibrationAudioClip;

    private void Start()
    {
        _audioSource = gameObject.GetComponent<AudioSource>();
        _audioSource.clip = _vibrationAudioClip;
        _audioSource.loop = true;
    }
    void Update()
    {
        if (!isInWall)
        {
            //Debug.Log("IN THE WALL");
          //  _audioSource.volume = 1;
            _audioSource.Play();
        }
        else
        {
            //_audioSource.Stop();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wall")
        {
            numberOfInWall++;
            if (numberOfInWall == 1)
            {
                isInWall = true;
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
            }
        }
    }
}
