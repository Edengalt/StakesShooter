﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveRagdollMivong : MonoBehaviour
{

    [SerializeField] GameObject mainbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.forward * 0.3f;
    }
}
