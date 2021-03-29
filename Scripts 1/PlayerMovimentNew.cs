using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMovimentNew : MonoBehaviour
{
    [Space(1)]
    [Header("Objects")]
    [SerializeField] private HingeJoint[] allRigidbodies;
    [SerializeField] private bool useSpring = true;
    [SerializeField] private GameObject animationHolder;
    [SerializeField] private GameObject kinematicCarrier;
    [SerializeField] private GameObject EnemySetup;
    [SerializeField] private GameObject colorChanger;


    [Space(2)]
    [Header("Limits")]
    [SerializeField] private HingeJoint Spine;
    [SerializeField] private HingeJoint Chest;
    [SerializeField] private HingeJoint UpperChest;

    private GameObject YKinematicPos;

    private bool isRaised = true;
    private bool onceCallCourotine;

    [Space(3)]
    [Header("Raise and Death Values")]

    [SerializeField]private float damage = 0f;

    [SerializeField]  private float timerForUseSpring = 0f;

    public delegate void DeathDelegate();
    public static event DeathDelegate Death;

    public static event DeathDelegate DestroyMainLink;



    private void Start()
    {
        YKinematicPos = kinematicCarrier;
 
    }



    private void Update()
    {


        if(damage > 6)
        {
            ThisOneIsDead();
        }

        if (!useSpring)
        {
            if (!onceCallCourotine)
            {
                StartCoroutine(HitByArrow());
                onceCallCourotine = !onceCallCourotine;
            }

            kinematicCarrier.GetComponent<Rigidbody>().isKinematic = false;
            foreach (HingeJoint HJ in allRigidbodies)
            {
                if(HJ != null)
                HJ.useSpring = false;
                
            }

            SpineChestUpperChestLimitsHit();
            animationHolder.GetComponent<Animator>().enabled = false;
            EnemySetup.GetComponent<NavMeshAgent>().enabled = false;
            EnemySetup.GetComponent<LookAtPlayerForWalkingEnemies>().enabled = false;
            EnemySetup.GetComponent<Rigidbody>().isKinematic = false;
            isRaised = false;
        }
        else if (useSpring)
        {
            if (!isRaised)
            {
                onceCallCourotine = !onceCallCourotine;
                StopAllCoroutines();

                kinematicCarrier.transform.position = Vector3.Lerp(transform.position,
                    new Vector3(kinematicCarrier.transform.position.x, 
                                YKinematicPos.transform.position.y, 
                                kinematicCarrier.transform.position.z), 1f);
                kinematicCarrier.GetComponent<Rigidbody>().isKinematic = true;
                foreach (HingeJoint HJ in allRigidbodies)
                {
                    HJ.useSpring = true;
                    
                }
                SpineChestUpperChestLimitsRaise();
                EnemySetup.GetComponent<EnemyMotor>().enabled = true;
                animationHolder.GetComponent<Animator>().enabled = true;
                EnemySetup.GetComponent<NavMeshAgent>().enabled = true;
                EnemySetup.GetComponent<LookAtPlayerForWalkingEnemies>().enabled = true;
                EnemySetup.GetComponent<Rigidbody>().isKinematic = true;

                isRaised = true;
            }
        }
    }


    private void SpineChestUpperChestLimitsHit()
    {
        var tempSpine = Spine.limits;
        var tempChest = Chest.limits;
        var tempUpperchest = UpperChest.limits;

        tempSpine.max = 35f;
        tempChest.max = 35f;
        tempUpperchest.max = 35f;
        tempSpine.min = -90f;
        tempChest.min = -90f;
        tempUpperchest.min = -90f;


        Spine.limits = tempSpine;
        Chest.limits = tempChest;
        UpperChest.limits = tempUpperchest;

    }

    private void SpineChestUpperChestLimitsRaise()
    {
        var tempSpine = Spine.limits;
        var tempChest = Chest.limits;
        var tempUpperchest = UpperChest.limits;

        tempSpine.max = 5f;
        tempChest.max = 5f;
        tempUpperchest.max = 5f;
        tempSpine.min = 0f;
        tempChest.min = 0f;
        tempUpperchest.min = 0f;


        Spine.limits = tempSpine;
        Chest.limits = tempChest;
        UpperChest.limits = tempUpperchest;

    }


    public void SignToPiercing()
    {
        Piercing.Pierce += TurnAnimatorOff;
    }

    private void TurnAnimatorOff()
    {
        if (this != null)
        {
            if (GetComponentInParent<NavMeshAgent>() != null)
                GetComponentInChildren<Animator>().enabled = false;
            if (GetComponentInParent<NavMeshAgent>() != null)
                GetComponentInParent<NavMeshAgent>().enabled = false;
        }
     }

    public void ArmsWasHit()
    {
            damage++;
    }



    public void StartCourotineUseSpring()
    {
        damage = damage + 2;
        if (useSpring)
        {
            useSpring = !useSpring;
        }
        else
        {
            timerForUseSpring = 100f;
        }
    }

    public void ThisOneIsDead()
    {
        
        StopAllCoroutines();
        useSpring = false;
        colorChanger.GetComponent<Renderer>().material.color = Color.gray;
        EnemySetup.GetComponent<CapsuleCollider>().enabled = false;
        kinematicCarrier.GetComponent<Rigidbody>().isKinematic = false;
        foreach (HingeJoint HJ in allRigidbodies)
        {
            HJ.useSpring = false;

        }
        animationHolder.GetComponent<Animator>().enabled = false;
        EnemySetup.GetComponent<NavMeshAgent>().enabled = false;
        EnemySetup.GetComponent<LookAtPlayerForWalkingEnemies>().enabled = false;
        EnemySetup.GetComponent<Rigidbody>().isKinematic = false;
        Piercing.Pierce -= TurnAnimatorOff;
        Death();
        Destroy(this);
    }



    private void restartCourotine()
    {
        StopAllCoroutines();
        StartCoroutine(HitByArrow());
    }

    public IEnumerator HitByArrow()
    {
        bool once = true;
        while (once)
        {

            float waitTime = 4f;
            timerForUseSpring = waitTime;
            float timer = timerForUseSpring;
            yield return new WaitForSeconds(waitTime);
            
            if (timer == timerForUseSpring) 
            {
                useSpring = true;
                once = false;
            }
            else
            {
                Debug.Log("else");
                Invoke("restartCourotine", 0f);
            }
            
              
        }


    }

}