using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    // Ce script a pour but de mesurer la vitesse d'un objet et de la contraindre entre 0 et 1
    // 0 correspond à une vitesse v <= minimalSpeed
    // 1 correspond à une vitesse v >= maximalSpeed

    [SerializeField] float minimalSpeed = (4.0f / 3.6f);
    [SerializeField] float maximalSpeed = (8.0f / 3.6f); // 4 kilomètres par heures exprimé en mètres par seconde
    public float speedIndicator = 0.0f; // Varie entre 0 et 1
    Vector3 OldPosition;
    float Distance;

    void Start()
    {
        OldPosition = this.transform.position;
    }

    void Update()
    {
        Distance = Vector3.Distance(OldPosition, this.transform.position);
        speedIndicator = (speedIndicator + (Mathf.Clamp(Distance/Time.deltaTime, minimalSpeed, maximalSpeed) / (maximalSpeed - minimalSpeed)))/2.0f;
        OldPosition = this.transform.position;
        speedIndicator = (float)((int)(speedIndicator * 10))/10.0f;
    }
}
