using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayerForWalkingEnemies : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    void Update()
    {
        transform.LookAt(new Vector3(Player.transform.position.x, Player.transform.position.y- (Player.transform.position.y - transform.position.y), Player.transform.position.z));

    }

}
