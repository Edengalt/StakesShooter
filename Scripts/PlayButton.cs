using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayButton : MonoBehaviour
{
public void ClosePlayWindow()
    {
        transform.parent.gameObject.GetComponent<Image>().DOFade(0f, .3f);
        transform.parent.gameObject.SetActive(false);
    }
}
