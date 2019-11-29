using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepDestruction : MonoBehaviour
{
    [SerializeField]
    float destructionTimer = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        destructionTimer -= Time.deltaTime;
        if (destructionTimer < 0)
            Destroy(gameObject);
    }
}
