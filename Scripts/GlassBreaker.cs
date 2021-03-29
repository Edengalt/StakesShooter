using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlassBreaker : MonoBehaviour
{
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Stakes"))
        {
            if(transform.CompareTag("Glass"))
            GetComponentInParent<MainGlass>().GlassWasHit();
        }
    }
}
