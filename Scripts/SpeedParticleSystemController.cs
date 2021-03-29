using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedParticleSystemController : MonoBehaviour
{

    private ParticleSystem speedVFX;
    void Awake()
    {
        GameController.GoToNextLevel += TurnParticlesOn;
        GameController.StartLevel += TurnParticlesOff;
        speedVFX = gameObject.GetComponent<ParticleSystem>();
    }

    private void TurnParticlesOn()
    {
        speedVFX.Play();
    }

    private void TurnParticlesOff()
    {
        speedVFX.Stop();
    }

    private void OnDestroy()
    {
        GameController.GoToNextLevel -= TurnParticlesOn;
        GameController.StartLevel -= TurnParticlesOff;
    }

}
