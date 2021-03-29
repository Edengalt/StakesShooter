using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InnerMover : MonoBehaviour
{
    [SerializeField] GameObject mainbody;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.localPosition != Vector3.zero)
        {
            mainbody.transform.position = transform.position;
            
        }
    }
}
