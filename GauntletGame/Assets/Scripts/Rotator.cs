﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] int rotationSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
       Destroy(gameObject, 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Rotate();
        
    }

    void Rotate()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); 
    }
    
   

}
