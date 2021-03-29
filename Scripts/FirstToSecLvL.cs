using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstToSecLvL : MonoBehaviour
{
    [SerializeField] private bool FirsWin;

    [SerializeField] private GameObject CamPOsControl;


    public void GoToSecLVL()
    {
        transform.GetComponent<Animator>().enabled = true;
    }


    private void Update()
    {
        if (FirsWin)
        {
            GoToSecLVL();
        }
    }
}
