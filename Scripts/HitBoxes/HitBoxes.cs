using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxes : MonoBehaviour
{
    [SerializeField] private GameObject Armature;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Stakes"))
        {
            HitBoxesFunc();
        }
    }

    public void HitBoxesFunc()
    {
        if (Armature.GetComponent<PlayerMovimentNew>())
        {
            Armature.GetComponent<PlayerMovimentNew>().StartCourotineUseSpring();
        }
        else
        {
            Destroy(this);
        }
    }
}
