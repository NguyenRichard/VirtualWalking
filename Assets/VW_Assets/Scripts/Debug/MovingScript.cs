﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//script déplaçant un objet à chaque frame
public class MovingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(new Vector3(0.001f, 0, 0));
    }
}
