using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SensSlider : MonoBehaviour
{
    private float Value;


    void Start()
    {
        Value = PlayerPrefs.GetFloat("TSens");
        GetComponent<Slider>().value = Value;
    }

    private void OnDisable()
    {
        Value = GetComponent<Slider>().value;
        PlayerPrefs.SetFloat("TSens", Value);
    }
}
