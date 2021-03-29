using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMotor1 : MonoBehaviour
{
    private NavMeshAgent agent;
    public bool start;

    [SerializeField] private GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //StartLevelTwo.StartPlatformTwo += StartPers;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (start & agent.enabled)
        {
            agent.SetDestination(Player.transform.position);
        }
        
    }

    public void StartPers()
    {
        start = true;
    }

    public void NavMeshAgentDisable()
    {
        if(agent.enabled)
        agent.enabled = false;
    }
}
