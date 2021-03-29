using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public float XInput;
    public float YInput;
    [SerializeField] float multiplier;


    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            XInput = transform.parent.rotation.x;
            YInput = transform.parent.rotation.y;
            //transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x + YInput * multiplier, -0.7f, 1.5f), Mathf.Clamp(transform.localPosition.y + XInput * multiplier, -0.7f, 1.5f), transform.localPosition.z);
            transform.rotation = new Quaternion(XInput, YInput, transform.rotation.z, 1f);
        }
    }
}
