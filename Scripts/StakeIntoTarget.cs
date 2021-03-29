using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StakeIntoTarget : MonoBehaviour
{
    [SerializeField] private Collider[] colliders;

    private HingeJoint ArrowhingeJoint;
    private ContactPoint contact;

    private Rigidbody arrowRigidbody;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemies"))
        {
            if(!GetComponent<HingeJoint>())
            JointSetup(collision.gameObject);

            collision.rigidbody.AddForce((transform.forward*0.7f + transform.up * 1.5f) * 100f, ForceMode.Impulse);

            if (transform.GetComponentInChildren<Piercing>() != null )
            {
                transform.GetComponentInChildren<Piercing>().check = true;

                
            }

            if(collision.transform.GetComponent<MainEnemyBodyLink>() !=null)
            collision.transform.GetComponent<MainEnemyBodyLink>().SignThisEnemyToPiercingDelegate();

            //GetComponent<Rigidbody>().drag = 500f;
            GetComponent<Rigidbody>().angularDrag = 50f;
            GetComponent<BoxCollider>().enabled = false;
            GetComponent<ArrowTorque>().enabled = false;
            gameObject.layer = LayerMask.NameToLayer("TransparentFX");
            Destroy(this);
        }
        else if (collision.gameObject.CompareTag("Glass") & !collision.gameObject.GetComponent<HingeJoint>())
        {
            contact = collision.contacts[0];
            JointSetup(collision.gameObject);

            Vector3 explosPos = transform.position;
            colliders = Physics.OverlapSphere(explosPos, 10f);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();
                if (rb != null && hit.CompareTag("BrokenGlass"))
                {
                    rb.AddExplosionForce(100f, explosPos, 50f, 5.0f, ForceMode.Impulse);

                }
            }

            Destroy(GetComponent<BoxCollider>());
            //Destroy(this);


        }
        else if (collision.gameObject.CompareTag("Box"))
        {

            if (transform.GetComponentInChildren<Piercing>() != null)
            {
                transform.GetComponentInChildren<Piercing>().DestroyTop();
            }
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
            transform.SetParent(collision.transform);
            Destroy(GetComponent<BoxCollider>());
            Destroy(this);
            collision.rigidbody.AddForce(transform.forward * 100f, ForceMode.Impulse);
        }
        else if (collision.gameObject.CompareTag("Walls"))
        {

            if (transform.GetComponentInChildren<Piercing>() != null)
            {
                transform.GetComponentInChildren<Piercing>().DestroyTop();
            }
            GetComponent<Rigidbody>().isKinematic = true;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
            Destroy(GetComponent<BoxCollider>());
            
            
            Destroy(this);
        }
        else if (collision.gameObject.CompareTag("FlyingEnemies"))
        {
            collision.gameObject.transform.GetComponent<FlyingEnemyDeath>().OneFlyingEnemyIsDead();
            if (transform.GetComponentInChildren<Piercing>() != null)
            {
                transform.GetComponentInChildren<Piercing>().DestroyTop();
            }
                GetComponent<Rigidbody>().isKinematic = true;
            GetComponentInChildren<Rigidbody>().isKinematic = true;
            transform.SetParent(collision.transform);
            Destroy(GetComponent<BoxCollider>());
            Destroy(this);
            collision.rigidbody.AddForce(transform.forward * 100f, ForceMode.Impulse);
        }
         
    }

    private void JointSetup(GameObject go)
    {
        transform.SetParent(go.transform);
        ArrowhingeJoint = gameObject.AddComponent<HingeJoint>();
        ArrowhingeJoint.connectedBody = go.gameObject.GetComponent<Rigidbody>();
        ArrowhingeJoint.axis = new Vector3(-1f, 0f, 0f);
        ArrowhingeJoint.useLimits = true;
        ArrowhingeJoint.useSpring = true;
        var newSpring = ArrowhingeJoint.spring;
        newSpring.spring = 3000f;
        newSpring.damper = 10f;
        ArrowhingeJoint.spring = newSpring;
    }

}
