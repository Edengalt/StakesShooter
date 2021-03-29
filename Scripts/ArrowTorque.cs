using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTorque : MonoBehaviour
{
    [SerializeField] float VelocityMult;
    [SerializeField] float AngularVelocityMult;

    private Rigidbody Rigidbody;

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 cross = Vector3.Cross(transform.forward, Rigidbody.velocity.normalized);
        Rigidbody.AddTorque(cross * Rigidbody.velocity.magnitude * VelocityMult);
        Rigidbody.AddTorque((-Rigidbody.angularVelocity + Vector3.Project(Rigidbody.angularVelocity, transform.forward)) * Rigidbody.velocity.magnitude * AngularVelocityMult);
    }
}
