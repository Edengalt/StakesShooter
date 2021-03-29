using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunPowerSlider : MonoBehaviour
{
    private float Value;


    void Start()
    {
        Value = PlayerPrefs.GetFloat("GunPower");
        GetComponent<Slider>().value = Value;
    }

    private void OnDisable()
    {
        Value = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("GunPower", Value);
    }
}
