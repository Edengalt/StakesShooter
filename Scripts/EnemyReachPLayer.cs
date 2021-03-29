using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReachPLayer : MonoBehaviour
{
    public delegate void EndGame();
    public static event EndGame End;

    private void OnTriggerEnter(Collider other)
    {

            Debug.Log("End");
            End();

    }
}
