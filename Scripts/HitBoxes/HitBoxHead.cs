using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxHead : MonoBehaviour
{
    [SerializeField] private GameObject Armature;
    [SerializeField] private GameObject EnemySetup;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Stakes"))
        {
            HitBoxHeadFunc();
            
        }
    }

    public void HitBoxHeadFunc()
    {
        if (Armature.GetComponent<PlayerMovimentNew>())
        {
            Armature.GetComponent<PlayerMovimentNew>().ThisOneIsDead();
        }
        else
        {
            Destroy(this);
        }
    }

    

}
