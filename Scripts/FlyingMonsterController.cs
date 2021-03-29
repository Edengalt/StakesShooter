using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingMonsterController : MonoBehaviour
{
    [SerializeField] private GameObject Player;

    [SerializeField] private float frequency = 1f;
    [SerializeField] private float magnitude = 2f;
    [SerializeField] private float speed = 0f;


    private Vector3 pos;
    private Vector3 PlPos;

    [SerializeField] private bool startedMoving;





    private void Start()
    {
        pos = transform.position;
        
    }

    void Update()
    {
        transform.LookAt(PlPos);
        pos += (transform.forward * speed * Time.deltaTime);
        Vector3 newPos = pos + transform.up  * Mathf.Sin(Time.time * frequency) * magnitude;
        transform.position = Vector3.Lerp(transform.position, newPos, 0.6f);

        
        PlPos = Player.transform.localPosition;
        
    }

    public void StartMoving()
    {
        startedMoving = true;
        speed = 4f;
    }
}
