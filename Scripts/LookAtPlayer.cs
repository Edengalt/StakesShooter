using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [SerializeField] private ParticleSystem VFX;

    void Update()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y- (Player.transform.position.y - transform.position.y), Player.transform.position.z));

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MainChar"))
        {
            Destroy(this.gameObject);

            Instantiate(VFX, transform.position, transform.rotation);
        }

        if (collision.gameObject.CompareTag("Stakes"))
        {
            Destroy(this.gameObject);
            Instantiate(VFX, transform.position, transform.rotation);
        }
    }
}
