using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using EZCameraShake;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject stake;
    [SerializeField] Transform AllFirstLevelThings;

    [SerializeField] public float timer = 0.7f;
    [SerializeField] public float shootingStrength = 110f;
    [SerializeField] private AudioSource Shot;
    [SerializeField] private AudioSource Reload;
    [SerializeField] Transform placeForArrow;

    [SerializeField] GameObject gameController;
    
    private GameObject croshair;

    private WeaponController weaponController;

    private Rigidbody stakeRigidbody;

    private Animator recoil;

    private GameController controller;







    void Start()
    {
        weaponController = this.gameObject.GetComponent<WeaponController>();
        recoil = GetComponentInChildren<Animator>();
        controller = gameController.GetComponent<GameController>();

        if (PlayerPrefs.GetFloat("GunPower") != 0)
        {
            shootingStrength = PlayerPrefs.GetFloat("GunPower");
        }
        else if(PlayerPrefs.GetFloat("GunPower") == 0)
        {
            PlayerPrefs.SetFloat("GunPower", shootingStrength);
        }

        

        
    }

    public void SetShootingStrength(float i)
    {
        shootingStrength = i;
    }

    void Update()
    {


        if (timer == 0 & weaponController.GetInput())
        {
                Shot.Play();
                Reload.Play();
                GameObject link = Instantiate(stake, placeForArrow.position, transform.rotation);
                link.transform.parent = AllFirstLevelThings;

                stakeRigidbody = link.GetComponent<Rigidbody>();
                stakeRigidbody.AddRelativeForce(Vector3.forward * shootingStrength, ForceMode.Impulse);
            MooveArrow(stakeRigidbody);
            recoil.Play("recoilWithModel");



            CameraShaker.Instance.ShakeOnce(3f, 1f, .1f, 0.1f);

                timer = 0.7f;

       }

        if (controller.LevelHasStarted)
        {
            timer -= Time.deltaTime;
            timer = Mathf.Clamp(timer, 0f, 5f);
        }
        
    }


    private void MooveArrow(Rigidbody go)
    {
        go.AddRelativeForce(Vector3.forward);
    }

/*    private void OnDestroy()
    {
        PlayerPrefs.SetFloat("GunPower", shootingStrength);
    }*/
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("GunPower", shootingStrength);
    }
}
