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
    [SerializeField] float lerpEffect = 1.0f; // Plus cette valeur est haute, plus l'effet de lissage est rapide
    float realSpeedIndicator = 0.0f; // Varie entre 0 et 1
    public float speedIndicator = 0.0f; // Varie entre 0.1 et 1
    Vector3 _oldPosition;
    float _distance;

    void Start()
    {
        _oldPosition = this.transform.position;
    }

    // V2, l'autre me semble pas ouf
    void Update()
    {

        _distance = Vector3.Distance(_oldPosition, transform.position);
        
        realSpeedIndicator = Mathf.Lerp(realSpeedIndicator, _distance/((maximalSpeed - minimalSpeed) * Time.deltaTime), lerpEffect * Time.deltaTime);

        if(realSpeedIndicator < 0.1)
        {
            speedIndicator = 0;
        }
        else
        {
            speedIndicator = realSpeedIndicator;
        }

        Mathf.Clamp(realSpeedIndicator, 1, 0);
        _oldPosition = transform.position;
    }
}
