using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemiesMoving : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody[] allRigidbodies;

    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        for(int i = 0; i <allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = true;
        }
    }

    private void Update()
    {
        
    }

    public void MakeItRigiddoll()
    {
        animator.enabled = false;

        for (int i = 0; i < allRigidbodies.Length; i++)
        {
            allRigidbodies[i].isKinematic = false;
            GetComponent<CapsuleCollider>().enabled = false;
            //allRigidbodies[i].gameObject.tag = "EnemiesDead";
            //Destroy(allRigidbodies[i].transform.gameObject.GetComponent<MainEnemyBodyLink>());
        }
    }
}
