using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class BarrelExplosion : MonoBehaviour
{
    [SerializeField] private bool radiusGizmo;

    [SerializeField] private float radius;
    [SerializeField] private float power;

    [SerializeField] private ParticleSystem VFX;

    [SerializeField] private Collider[] colliders;



    private void OnDrawGizmos()
    {
        if (radiusGizmo)
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stakes"))
        {
           Vector3 explosPos = transform.position;
           colliders = Physics.OverlapSphere(explosPos, radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(power, explosPos, radius, 10.0f);
                    if (hit.gameObject.GetComponent<HitBoxes>() != null) {
                        hit.gameObject.GetComponent<HitBoxes>().HitBoxesFunc();
                    }
                    else if (hit.gameObject.GetComponent<HitBoxHead>() != null)
                    {
                        hit.gameObject.GetComponent<HitBoxHead>().HitBoxHeadFunc();
                    }
                    else if(hit.gameObject.GetComponent<HitBoxArms>() != null)
                    {
                        hit.gameObject.GetComponent<HitBoxArms>().HitBoxArmsFunc();
                    }
                    else if (hit.CompareTag("Glass"))
                        hit.GetComponentInParent<MainGlass>().GlassWasHit();

                    
                }


            }

            Instantiate(VFX, transform.position, transform.rotation);
            CameraShaker.Instance.ShakeOnce(8f, 4f, .1f, 1f);
            Destroy(this.gameObject);

        }
    }
}
