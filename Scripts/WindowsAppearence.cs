using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WindowsAppearence : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        transform.DOScale(Vector3.one, 0.9f);
    }

    private void OnDisable()
    {
        transform.DOScale(Vector3.zero, 0.9f);
    }
}
