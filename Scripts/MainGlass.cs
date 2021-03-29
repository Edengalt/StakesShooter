using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGlass : MonoBehaviour
{
     private Transform[] transforms = new Transform[23];
     public GameObject AllLevelThings;

    private int i = 0;

    void Start()
    {
        foreach (Transform child in transform)
        {
            transforms[i] = child;
            i++;
        }
        AllLevelThings = GameController.FirstLevelThings;
    }


    public void SetAllLevelGOForParenting()
    {

        AllLevelThings = SetupNextLevel.AllThingsForNextLevelStatic;
        Debug.Log(AllLevelThings);
    }

    public void GlassWasHit()
    {
        
        if (AllLevelThings == null)
        {
            SetAllLevelGOForParenting();
            Invoke("GlassWasHit", 0f);
        }
        else
        {

            for (int i = 0; i < transforms.Length; i++)
            {
                GameObject Piece = transforms[i].gameObject;
                Destroy(Piece.GetComponent<GlassBreaker>());
                Piece.transform.SetParent(AllLevelThings.transform);
                Piece.transform.tag = "BrokenGlass";
                Piece.layer = LayerMask.NameToLayer("Default");
                Piece.GetComponent<MeshRenderer>().enabled = true;
                Piece.GetComponent<Rigidbody>().isKinematic = false;


            }
            gameObject.SetActive(false);
            
        }
    }
}
