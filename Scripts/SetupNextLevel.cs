using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupNextLevel : MonoBehaviour
{
    public delegate void StartNextLevelDelegate();
    public static event StartNextLevelDelegate SetupCurrentLevel;

    [SerializeField] private GameObject GameController;
    [SerializeField] private GameObject AllEnemiesForNextLevel;
    [SerializeField] public  GameObject AllThingsForNextLevel;


    [SerializeField] String AnimationName;

    private GameController startGame;

    private float t = 5f;

    public static GameObject AllThingsForNextLevelStatic;



    private void Start()
    {
        
        startGame = GameController.GetComponent<GameController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("MainChar"))
        {
            SetupCurrentLevel();
            AllThingsForNextLevelStatic = AllThingsForNextLevel;
            startGame.SetUpEnemiesAndAllObjects(AllEnemiesForNextLevel, AllThingsForNextLevel, AnimationName);
            Destroy(transform.gameObject);
        }
    }

    
        
    
}
