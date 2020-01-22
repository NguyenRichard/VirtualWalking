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
    Vector3 _oldPosition;
    float _distance;

    void Start()
    {
        _oldPosition = this.transform.position;
    }

    // V2, l'autre me semble pas ouf
    void Update()
    {
        //_distance = Vector3.Distance(_oldPosition, this.transform.position);
        //speedIndicator = (speedIndicator + (Mathf.Clamp(_distance/Time.deltaTime, minimalSpeed, maximalSpeed) / (maximalSpeed - minimalSpeed)))/2.0f;
        //_oldPosition = this.transform.position;
        //speedIndicator = (float)((int)(speedIndicator * 10))/10.0f;

        _distance = Vector3.Distance(_oldPosition, transform.position);
        speedIndicator = Mathf.Lerp(speedIndicator, _distance/((maximalSpeed - minimalSpeed) * Time.deltaTime), Time.deltaTime);
        Mathf.Clamp(speedIndicator, 1, 0);
        _oldPosition = transform.position;
    }
}
