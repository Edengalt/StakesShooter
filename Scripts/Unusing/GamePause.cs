using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    [SerializeField] public bool Pause;
    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
            
    }

    public void Paused()
    {
        Time.timeScale = 1;
    }


    public void Unpaused()
    {
        Time.timeScale = 0;
    }
}
