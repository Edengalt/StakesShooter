using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfMassFix : MonoBehaviour
{
    [SerializeField] private Vector3 moveCenterOfMass;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(GetComponent<Rigidbody>().worldCenterOfMass, 0.04f);
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().centerOfMass = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
