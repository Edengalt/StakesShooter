using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class EnemyMotor : MonoBehaviour
{
    
    [SerializeField] private Transform RootWithAnimation;
    [SerializeField] private GameObject Player;

    private NavMeshAgent agent;
    private Animator animator;
    public bool Dead;

    public bool start;

    public delegate void LowerGroundDelegate();
    public static event LowerGroundDelegate LowerGround;

    // Start is called before the first frame update
    void Start()
    {
       agent = GetComponent<NavMeshAgent>();
       animator = RootWithAnimation.GetComponent<Animator>();
        

    }

    // Update is called once per frame
    void LateUpdate()
    {
        

        if (start & agent.enabled)
       {
            agent.SetDestination(Player.transform.position);
        }

        float SpeedController = agent.velocity.magnitude / agent.speed;
        animator.SetFloat("ActiveRagdollSpeed", SpeedController, .1f, Time.deltaTime);

    }

    public void seton()
    {
        PlayerMovimentNew.Death += setoff;
        EnemyReachPLayer.End += NavMeshAgentDisable;
    }

    public void setoff()
    {
        PlayerMovimentNew.Death -= setoff;
        EnemyReachPLayer.End -= NavMeshAgentDisable;
        Dead = true;
        this.enabled = false;
    }

    public void SignToDelegate()
    {
        GameController.StartLevel += StartPers;
    }

    public void StartPers()
    {
        start = true;
        NavMeshAgentEnable();
    }

    public void NavMeshAgentDisable()
    {
        if (agent.enabled)
            agent.speed = 0f;
    }

    public void NavMeshAgentEnable()
    {
        if (agent != null)
            agent.enabled = true;
    }


    private void LowerGroundOn()
    {
        if(!Dead)
            LowerGround();
    }

    private void OnDestroy()
    {
        LowerGroundOn();
        PlayerMovimentNew.Death -= setoff;
        GameController.StartLevel -= StartPers;
        EnemyReachPLayer.End -= NavMeshAgentDisable;
    }
}
