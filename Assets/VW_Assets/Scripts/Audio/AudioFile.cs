using UnityEditor;
using UnityEngine;


[System.Serializable]

public class AudioFile

{
    public string audioName;
    public AudioClip audioClip;

    [HideInInspector]
    public float volume;

    [HideInInspector]
    public AudioSource source;

    public bool isLooping;
    public bool playOnAwake;
}
