using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyDeath : MonoBehaviour
{
    [SerializeField] GameObject gameController;


    public void OneFlyingEnemyIsDead() 
    {
        gameController.GetComponent<GameController>().OneMoreDead();
    }
}
