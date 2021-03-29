using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piercing : MonoBehaviour
{

    [SerializeField]private float timer = 1f;

    public bool check;

    public delegate void PiercingDelegate();
    public static event PiercingDelegate Pierce;


    private void Update()
    {
        if (check)
        {

            if (timer >= 0)
            {
                gameObject.layer = LayerMask.NameToLayer("StrakeTop");
                timer -= Time.deltaTime;

            }
            else
            {
                GetComponent<BoxCollider>().enabled = false;
                Destroy(this.gameObject);
            }

        }
    }

    public void DestroyTop()
    {
        GetComponent<BoxCollider>().enabled = false;
        transform.parent.GetComponent<Rigidbody>().isKinematic = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls") & check == true)
        {
            DestroyTop();
            
            Pierce();
            Destroy(this);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Walls") & check == true)
        {
            GetComponent<BoxCollider>().enabled = false;
            transform.parent.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(this);
        }
    }
}
