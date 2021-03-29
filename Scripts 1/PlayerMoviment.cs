using UnityEngine;

public class PlayerMoviment : MonoBehaviour
{// Pé = foot //perna = leg // braço = arm // FatorSubida = height Fator
    public Transform Rotate;
    public Animator AnimPerna,AnimBraço;
    public float Translação = 8;
    [Space(10)]
    [Header("SpringSettings")]
    public bool UseSpring = true;
    public float TempoLevantar;
    public float ForçaInicial = 500;
    public HingeJoint[] AllHingeJoints;
    public BoxCollider[] ColisoresIK;
    [Space(10)]
    [Header("FootSettings")]
    public HingeJoint PeEsq;
    public HingeJoint PeDir;
    public float PForward, PBackward;
    [Space(10)]
    [Header("RunSettings")]
    public Transform RotateBraço;
    public float FatorSubida;
    [Space(20)]
    public Transform CameraRot;

    private float Dir;
    private float ZHeight;
    private float TimeReborn;
    private Quaternion RotAnt;
    private float[] PreviousString;
    private void Start()
    {
        if(RotateBraço)
        ZHeight = RotateBraço.transform.localPosition.z;
        TimeReborn = 500;
        PreviousString = new float[AllHingeJoints.Length];
        for (int x = 0; x < AllHingeJoints.Length; x++)
        {
                PreviousString[x] = AllHingeJoints[x].spring.spring;
        }
    }
    void Update()
    {
        SetAllSprings();
        if (Input.GetKeyDown(KeyCode.P))
        {
            UseSpring = !UseSpring;
        }
        if (TimeReborn == 500)
        {
            if (UseSpring)
            {
                if (CameraRot != null)
                    if (!Input.GetKey(KeyCode.LeftAlt))
                    {
                        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                        {
                            var AnguloY = Mathf.LerpAngle(transform.eulerAngles.y, CameraRot.eulerAngles.y, 2.5f * Time.deltaTime);
                            transform.eulerAngles = new Vector3(0, AnguloY, 0);
                        }
                    }
            }


            Dir = 0;
            if (Rotate != null)
                Dir = Rotate.eulerAngles.y;
            float Frente = transform.eulerAngles.y;
            var Direita = Frente + 90;
            var Esquerda = Frente - 90;
            //Run mode ===============================
            if (Input.GetKey(KeyCode.LeftShift))
            {
                AnimPerna.speed = 1.5f;
                AnimBraço.speed = 1.5f;
                RotateBraço.transform.localPosition = Vector3.Lerp
                    (RotateBraço.transform.localPosition, new Vector3(0, 0, ZHeight + FatorSubida), 2 * Time.deltaTime);
            }
            else
            {
                AnimPerna.speed = 1f;
                AnimBraço.speed = 1f;
                RotateBraço.transform.localPosition = Vector3.Lerp
                    (RotateBraço.transform.localPosition, new Vector3(0, 0, ZHeight), 2 * Time.deltaTime);
            }



            if (Input.GetKey(KeyCode.W))
            {
                SetFootRot(PForward);
                SetMoviment(Dir, Frente, true, 1, 1);

                if (Input.GetKey(KeyCode.D)) SetMoviment(Dir, Direita, false, 0, 0);
                if (Input.GetKey(KeyCode.A)) SetMoviment(Dir, Esquerda, false, 0, 0);
            }
            else
            {
                if (Input.GetKey(KeyCode.S))
                {
                    SetFootRot(PBackward);//esse valor é relativo!!
                    SetMoviment(Dir, Frente, true, -1, -1);
                    if (Input.GetKey(KeyCode.D)) SetMoviment(Dir, Esquerda, false, 0, 0);
                    if (Input.GetKey(KeyCode.A)) SetMoviment(Dir, Direita, false, 0, 0);
                }
                else
                {
                    if (Input.GetKey(KeyCode.D))
                    {
                        SetMoviment(Dir, Direita, true, 1, 0);
                    }
                    else
                    if (Input.GetKey(KeyCode.A))
                    {
                        SetMoviment(Dir, Esquerda, true, 1, 0);
                    }
                    else
                    {
                        SetMoviment(Dir, Frente, true, 0, 0);
                    }
                }
            }

            if (Rotate != null)
                Rotate.eulerAngles = new Vector3(Rotate.eulerAngles.x, Dir, Rotate.eulerAngles.z);
        }
    }
    
    void SetAllSprings()
    {
        if (AllHingeJoints.Length > 0)
        {
            if (AllHingeJoints[0].useSpring != UseSpring) // com isto, estas coisas só aconteceram em 1 frame, e não a todo momento
            {
                for (int x = 0; x < AllHingeJoints.Length; x++)
                {
                    AllHingeJoints[x].useSpring = UseSpring;
                    if (UseSpring == true)
                    {
                        JointSpring Sp = AllHingeJoints[x].spring;
                        Sp.spring = ForçaInicial;
                        AllHingeJoints[x].spring = Sp;
                    }
                }
                
                
                TimeReborn = 0;
                AnimBraço.SetInteger("BraçoAndar", -10);
                AnimPerna.SetInteger("Andar", -10);
                AnimBraço.StopPlayback();
                AnimPerna.StopPlayback();
            }
            if (TimeReborn <= TempoLevantar)
            {
                TimeReborn += 1 * Time.deltaTime;
                if (UseSpring == true)
                {
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
                    transform.rotation = Quaternion.Lerp(transform.rotation, RotAnt, 2f * Time.deltaTime);
                    AnimBraço.SetInteger("BraçoAndar", -10);
                    AnimPerna.SetInteger("Andar", -10);
                    AnimBraço.Play(1);
                    AnimBraço.Play(1);
                }
                else
                {
                    RotAnt = transform.rotation;
                    TimeReborn = 500;
                    GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    for (int x = 0; x < ColisoresIK.Length; x++)
                    {
                        ColisoresIK[x].enabled = UseSpring;
                    }
                }
            }
            else
            {
                if (TimeReborn != 500)
                {
                    var AnguloY = Mathf.LerpAngle(transform.eulerAngles.y, CameraRot.eulerAngles.y, 2.5f * Time.deltaTime);
                    transform.eulerAngles = new Vector3(0, AnguloY, 0);
                    for (int x = 0; x < ColisoresIK.Length; x++)
                    {
                        ColisoresIK[x].enabled = UseSpring;
                    }
                    for (int x = 0; x < AllHingeJoints.Length; x++)
                    {
                            JointSpring Sp = AllHingeJoints[x].spring;
                            Sp.spring = PreviousString[x]; // o spring original dos ossos
                            AllHingeJoints[x].spring = Sp;
                        
                    }
                    TimeReborn = 500;
                }

            }
        }
    }
    
    

    void SetMoviment(float Direction, float Sentido, bool SetAnim, int LegState, int ArmSate) 
    {
        Dir = Mathf.LerpAngle(Direction, Sentido, Translação * Time.deltaTime);
        if (SetAnim)
        {
            AnimPerna.SetInteger("Andar", LegState);
            AnimBraço.SetInteger("BraçoAndar", ArmSate);
        }
    }


    void SetFootRot(float Rotation)
    {
        if(PeDir != null)
        {
            var SjD = PeDir.spring;
            var SjE = PeEsq.spring;
            SjD.targetPosition = Rotation;
            SjE.targetPosition = Rotation;
            PeDir.spring = SjD;
            PeEsq.spring = SjE;
        }
    }
}
