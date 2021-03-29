using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class playerscontroller : MonoBehaviour
{
    public float isFirstLaunch;
    public float appOpen;

    public static playerscontroller instance;

    public bool onceStarted;

    void Awake()
    {

        if(instance == null)
        {
            instance = this;
            appOpen = 0f;
        }
        else
        {
            onceStarted = true;
            Destroy(this);
            
            
        }

        if (!onceStarted)
        {

            DontDestroyOnLoad(this.gameObject);

            Amplitude amplitude = Amplitude.Instance;
            amplitude.logging = true;
            amplitude.init("5b49884a203199f4833f82d245f9fc17");


            isFirstLaunch = PlayerPrefs.GetFloat("isFirstLaunch");
            if (isFirstLaunch == 0)
            {
                Amplitude.Instance.logEvent("install");
                isFirstLaunch = 1;
                PlayerPrefs.SetFloat("isFirstLaunch", isFirstLaunch);
            }

            if (appOpen == 0f)
            {
                Amplitude.Instance.logEvent("app_open");
                appOpen = 1f;
            }
            onceStarted = true;
        }

    }
}
