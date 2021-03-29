
using UnityEngine;
using UnityEngine.AI;

public class MainEnemyBodyLink : MonoBehaviour
{
    [SerializeField] GameObject armature;
    [SerializeField] GameObject EnemySetup;

    private void Start()
    {
        
    }

    public void SignThisEnemyToPiercingDelegate()
    {
        if(armature.GetComponent<PlayerMovimentNew>() != null)
        {
            armature.GetComponent<PlayerMovimentNew>().SignToPiercing();
        }
    }


    
    private void OnDestroy()
    {
       
        Destroy(EnemySetup);
    }


}
