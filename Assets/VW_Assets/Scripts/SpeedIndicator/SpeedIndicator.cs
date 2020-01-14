using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedIndicator : MonoBehaviour
{
    // Ce script a pour but de mesurer la vitesse d'un objet et de la contraindre entre 0 et 1
    // L'objet sur lequel ce script est appliqué doit contenir un composant rigid body
    // 0 correspond à une vitesse v <= minimalSpeed
    // 1 correspond à une vitesse v >= maximalSpeed

    [SerializeField] float minimalSpeed = 0.0f;
    [SerializeField] float maximalSpeed = (4.0f / 3.6f); // 4 kilomètres par heures exprimé en mètres par seconde
    public float speedIndicator = 0.0f; // Varie entre 0 et 1
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        speedIndicator = Mathf.Clamp(rb.velocity.magnitude, minimalSpeed, maximalSpeed) / (maximalSpeed - minimalSpeed);
    }
}
