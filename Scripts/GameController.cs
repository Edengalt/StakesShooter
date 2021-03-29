using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GameController : MonoBehaviour
{
    [Space]
    [Header("First Level")]
    [SerializeField] private GameObject firstLevelEnemies;
    [SerializeField] private GameObject BoxForCamAndChar;
    [SerializeField] private GameObject FirstLevelAllThings;

    [Space]
    [Header("Enemies Counting")]
    [SerializeField] private int OnLevelEnemyCount;
    [SerializeField] private int DeadBodies;

    [Space]
    [Header("Very First Anmation")]
    [SerializeField] String FirstToSecoundLvlAnimName;
    
    private GameObject AllThingsOnCurrentLevel;

    private BoxCollider AllThingsBoxCollider;

    private Animator AllThingsAnimator;

    public bool LevelHasStarted;
    private bool isParticlesTurnedOn;

    private String AnimationName;

    public static GameObject FirstLevelThings;

    public delegate void StartDelegate();
    public static event StartDelegate StartLevel;
    public static event StartDelegate GoToNextLevel;

    private void Awake()
    {
        
        FirstLevelThings = FirstLevelAllThings;
        PlayerMovimentNew.Death += OneMoreDead;
        EnemyMotor.LowerGround += OneMoreDead;
        SetUpEnemiesAndAllObjects(firstLevelEnemies, FirstLevelAllThings, FirstToSecoundLvlAnimName);
        AnimationName = FirstToSecoundLvlAnimName;

    }

    private void Update()
    {
        if(DeadBodies >= OnLevelEnemyCount)
        {
            TurnSpeedParticles();
            AllThingsOnCurrentLevel.GetComponent<BoxCollider>().enabled = true;
            BoxForCamAndChar.GetComponent<Animator>().enabled = true;
            BoxForCamAndChar.GetComponent<Animator>().Play(AnimationName);
        }
    }


    private void TurnSpeedParticles()
    {
        if (!isParticlesTurnedOn)
        {
            GoToNextLevel?.Invoke();
            isParticlesTurnedOn = true;
        }
    }

    public void SetUpEnemiesAndAllObjects(GameObject EnemyCount, GameObject AllObjects, string AnimName)
    {
        AllThingsOnCurrentLevel = AllObjects;

        OnLevelEnemyCount = EnemyCount.transform.childCount;
        foreach (Transform CurrentLevelEnemy in EnemyCount.transform)
        {
            if (CurrentLevelEnemy.CompareTag("Enemies"))
                if (CurrentLevelEnemy.GetComponentInChildren<Transform>().GetComponentInChildren<EnemyMotor>() != null)
                {
                    CurrentLevelEnemy.GetComponentInChildren<Transform>().GetComponentInChildren<EnemyMotor>().SignToDelegate();
                    CurrentLevelEnemy.GetComponentInChildren<Transform>().GetComponentInChildren<EnemyMotor>().seton();
                }

                if (CurrentLevelEnemy.CompareTag("FlyingEnemies"))
                {
                Debug.Log("Fly");
                    CurrentLevelEnemy.GetComponentInChildren<FlyingMonsterController>().StartMoving();
                }
        }
        StartCurrentLevel();
        AnimationName = AnimName;
        DeadBodies = 0;
        
    }

    public void OneMoreDead()
    {
        DeadBodies++;
    }

    public void StartLevelFromButtonPLay()
    {
        LevelHasStarted = true;
    }

    public void StartCurrentLevel()
    {
        if (LevelHasStarted)
        {
            StartLevel();
            isParticlesTurnedOn = false;
        }

    }

    private void OnDestroy()
    {
        PlayerMovimentNew.Death -= OneMoreDead;



    }

    

 

}
