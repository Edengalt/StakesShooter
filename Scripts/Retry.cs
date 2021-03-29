using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Retry : MonoBehaviour
{
    [SerializeField] GameObject retry;
    [SerializeField] Image imageFail;
    [SerializeField] GameObject MainRetry;

    private Image img;
    private Image retryImage;

    private void Start()
    {
        EnemyReachPLayer.End += RetryScreen;
        img = MainRetry.transform.GetComponent<Image>();
        retryImage = transform.GetComponent<Image>();
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void RetryScreen()
    {
        retryImage.raycastTarget = true;
        retryImage.DOFade(0.85f, 0.6f);
        img.raycastTarget = true;
        //Time.timeScale = 0;
        retry.SetActive(true);
        img.DOFade(0.85f, 0.6f);
        imageFail.DOFade(0.85f, 0.6f);
        imageFail.raycastTarget = true;
    }

    private void OnDestroy()
    {
        EnemyReachPLayer.End -= RetryScreen;
    }
}
