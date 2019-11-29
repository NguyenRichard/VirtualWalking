using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepStart : MonoBehaviour
{
    public StepDetector sd;

    // Start is called before the first frame update
    void Start()
    {
        sd.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
