using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowerGround : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        //if (collision.gameObject.CompareTag("EnemiesDead"))
       //  Destroy(collision.transform.GetComponent<MainEnemyBodyLink>().mainEnemy);
       // else
            Destroy(collision.gameObject);
    }
}
