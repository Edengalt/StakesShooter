using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxArms : MonoBehaviour
{
    public GameObject Armature;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Stakes"))
        {
            HitBoxArmsFunc();
        }
    }

    public void HitBoxArmsFunc()
    {
        if (Armature.GetComponent<PlayerMovimentNew>())
        {
            Armature.GetComponent<PlayerMovimentNew>().ArmsWasHit();
        }
        else
        {
            Destroy(this);
        }
    }

}
