using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class WeaponController : MonoBehaviour
{
    [SerializeField] public float mouseSensitivity = 55;
    [SerializeField] public float touchSensitivity = 4f;

    [SerializeField] private GameObject cam;
    [SerializeField] private GameObject Position;
    [SerializeField] private GameObject GlobalHolder;

    private GameObject weapon;

    private bool isToushed;

    public float XInput;
    public float YInput;
    [SerializeField] private float XInputRotation;
    [SerializeField] private float YInputRotation;
    [SerializeField] private float YMainHolderRotation;

    private Quaternion CamPOs;

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }


    void Awake()
    {
        weapon = this.gameObject;

        

        ChangeRotationAnchor();

        SetupNextLevel.SetupCurrentLevel += ChangeRotationAnchor;
        SetupNextLevel.SetupCurrentLevel += CrossLevelRotationFix;

        if (PlayerPrefs.GetFloat("TSens") != 0)
        {
            touchSensitivity = PlayerPrefs.GetFloat("TSens");
        }
        else if (PlayerPrefs.GetFloat("TSens") == 0)
        {
            PlayerPrefs.SetFloat("TSens", touchSensitivity);
        }
    }

    void LateUpdate()
    {
        GetInput();
    }


    public void SetSens(float i)
    {
        touchSensitivity = i;
    }

    public bool GetInput()
    {
        
            if (Input.GetMouseButton(0) || (Input.touchCount == 1))
            {
            if (!EventSystem.current.IsPointerOverGameObject() & !IsPointerOverUIObject())
            {
                isToushed = true;
                WeaponRotation();
            }
            }
            else
            {
                isToushed = false;
            }

            return isToushed; 
    }


    private void WeaponRotation()
    {

#if UNITY_EDITOR
        XInput += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        YInput += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        InputTransformsToActualRotation();
#endif

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            XInput += Input.touches[0].deltaPosition.x * touchSensitivity * Time.deltaTime;
            YInput += Input.touches[0].deltaPosition.y * touchSensitivity * Time.deltaTime;

            InputTransformsToActualRotation();
        }


       
            CamPOs.y += XInputRotation * 0.85f * Time.deltaTime;
            CamPOs.y = Mathf.Clamp(CamPOs.y, - 15f,  + 15);

            CamPOs.x += -YInputRotation * 0.85f * Time.deltaTime;
            CamPOs.x = Mathf.Clamp(CamPOs.x,  - 5f,  5f);
            
            // Camera rotation
            cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(CamPOs.x, CamPOs.y, 0f), 0.7f);
    }

    private void InputTransformsToActualRotation()
    {
        XInputRotation = XInput - YMainHolderRotation;
        YInputRotation = YInput;
        XInput = Mathf.Clamp(XInput, YMainHolderRotation - 60f, YMainHolderRotation + 60f);
        YInput = Mathf.Clamp(YInput, -50f, 40f);

        //weapon rotation
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-YInput, XInput, transform.rotation.z), 0.9f);

    }

    public void CrossLevelRotationFix()
    {
        XInput = 0f + YMainHolderRotation;
        YInput = 0f;
        CamPOs.y = 0f;
        CamPOs.x = 0f;
        XInputRotation = 0f;
        YInputRotation = 0f;
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(-YInput, XInput, transform.rotation.z), 1f);
        cam.transform.localRotation = Quaternion.Lerp(cam.transform.localRotation, Quaternion.Euler(CamPOs.x, CamPOs.y, 0f), 1f);
    }


    public void ChangeRotationAnchor()
    {
        YMainHolderRotation = GlobalHolder.transform.rotation.eulerAngles.y;
    }


   

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("TSens", touchSensitivity);
    }


    private void OnDestroy()
    {
        SetupNextLevel.SetupCurrentLevel -= ChangeRotationAnchor;
        SetupNextLevel.SetupCurrentLevel -= CrossLevelRotationFix;
    }

}
