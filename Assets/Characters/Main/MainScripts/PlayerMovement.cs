using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Inspector Variables
    [SerializeField]ProgressManager ProgressManagerScript;

    [Header("Movement Inputs")]
    [SerializeField] private KeyCode MoveRightButton;
    [SerializeField] private KeyCode MoveLeftButton;
    [SerializeField] private KeyCode JumpButton;
    [SerializeField] private KeyCode CrouchButton;
    [SerializeField] private KeyCode ReleaseTheLedgeBTN;

    [Space(5)]
    [Header("Wall Jump Settings")]
    [SerializeField] private Transform WallJumpCircleCenter;
    [SerializeField] private float WallJumpCircleRadius;
    [SerializeField] private LayerMask WallLayer;
    [SerializeField] private Vector2 WallJumpForce;

    [Space(5)]
    [Header("Running and Jump settings")]
    [SerializeField] private Vector2 JumpForce;
    [SerializeField] private Vector2 RunningSpeed;


    [SerializeField] private GameObject TopClimbObject;
    [SerializeField] private GameObject BottomClimbObject;
    [SerializeField] private float ClimbRayDistance;
    [SerializeField] private LayerMask GroundLayer;


    #endregion



    #region Other Variables
    private Animator MainAnimator;
    private Rigidbody2D MainRB;



    private bool Jump;
    private bool MoveLeft;
    private bool MoveRight;

    private bool Right = true, Left = false; //Direction Controls

    private bool CornerGrb=false;
    private bool CanGrab=true;

    private bool ClimbUp;
    private bool ReleaseTheLedge;
    private Vector2 CornerPos;
    #endregion

    private void Start()
    {
        MainAnimator=GetComponent<Animator>();
        MainRB=GetComponent<Rigidbody2D>();
        ProgressManagerScript.CanMove = true;
        ProgressManagerScript.IsOnTheGround = true;
    }

    #region Input Control
    void Update()
    {
        InputControl();
    }


    private void InputControl()
    {
        if (ProgressManagerScript.CanMove) //RUN
        {
            MoveRight=Input.GetKey(MoveRightButton);
            MoveLeft=Input.GetKey(MoveLeftButton);
            if (ProgressManagerScript.IsOnTheGround)
            {
                MainAnimator.SetBool("Run", (MoveRight || MoveLeft));
            }
        }
        else
        {
            MoveRight = false;
            MoveLeft = false;
            MainAnimator.SetBool("Run", false);
        }

        if (Input.GetKeyDown(JumpButton)&&(ProgressManagerScript.IsOnTheGround || WallSlideControl()) && ProgressManagerScript.CurrentStamina >= 7)//JUMP
        {
            Jump = true; //it will set to false after executing jump function
            MainAnimator.SetBool("Jump", true);
        }


        if (Input.GetKeyDown(CrouchButton))//Crouch
        {
            ProgressManagerScript.IsCrouch = !ProgressManagerScript.IsCrouch;
            MainAnimator.SetBool("IsCrouch", ProgressManagerScript.IsCrouch);
        }

        if(CornerGrb)
        {
            ClimbUp=Input.GetKeyDown(JumpButton);
            ReleaseTheLedge = Input.GetKeyDown(ReleaseTheLedgeBTN);
        }

    }
    #endregion

    #region Physical Movement Functions
    private void FixedUpdate()
    {
        JumpFunc();
        MoveFunc();
        WallSlideControl();
        ClimbFunc();
    }

    void JumpFunc()
    {
        if (Jump)
        {
            if (!WallSlideControl())
            {
                MainRB.AddForce(JumpForce * Time.deltaTime);
            }
            else
            {
                MainRB.AddForce(WallJumpForce * Time.deltaTime);
                FlipTheCharacter(!ProgressManagerScript.CharacterDirection);
            }
            Jump = false;
        }
    }
    void MoveFunc()
    {
        if (MoveRight)
        {
            FlipTheCharacter(Right);
            transform.Translate(RunningSpeed * Time.deltaTime);
        }
        else if (MoveLeft)
        {
            FlipTheCharacter(Left);
            transform.Translate(RunningSpeed * Time.deltaTime);
        }
    }
    void FlipTheCharacter(bool Direction )
    {
        if (Direction==Right)
        {
             transform.eulerAngles = new Vector3(0, 0, 0);
             WallJumpForce.x = -10000;
             ProgressManagerScript.CharacterDirection = Right;
        }
        else if (Direction==Left)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            WallJumpForce.x = 10000;
            ProgressManagerScript.CharacterDirection = Left;
        }
    }
    #endregion



    void ClimbFunc()
    {

        RaycastHit2D TopRay = Physics2D.Raycast(TopClimbObject.transform.position, transform.right, ClimbRayDistance, GroundLayer);
        RaycastHit2D BottomRay = Physics2D.Raycast(BottomClimbObject.transform.position, transform.right, ClimbRayDistance, GroundLayer);

        

        if (!TopRay && BottomRay && CanGrab)
        {
          //  CapsuleCollider2D capsuleCollider2D = new CapsuleCollider2D();
          //  capsuleCollider2D.enabled = false;
            CornerGrb = true;
            CornerPos = BottomRay.point;
            //transform.position = new Vector3(CornerPos.x, Mathf.Floor(CornerPos.y), 0);
            transform.position =  Vector3.Lerp(transform.position,new Vector3(CornerPos.x, Mathf.Floor(CornerPos.y), 0),Mathf.SmoothStep(0.2f,1,0.05f));

            MainRB.constraints = RigidbodyConstraints2D.FreezeAll;
            MainAnimator.SetBool("GrabCorner", true);
            ProgressManagerScript.CanMove = false;
            ProgressManagerScript.CanAim=false;
            ///boxcol

        }

        if (CornerGrb)
        {
            if (ClimbUp)
            {
                MainRB.constraints = RigidbodyConstraints2D.None;
                MainRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                CornerGrb = false;
                MainAnimator.SetTrigger("Climb");
                MainAnimator.SetBool("GrabCorner", false);
                ProgressManagerScript.CanMove = true;
                CanGrab = false;
                MainRB.AddForce(JumpForce * Time.deltaTime);
                ClimbUp = false;
            }

            if (ReleaseTheLedge)
            {
                MainRB.constraints = RigidbodyConstraints2D.None;
                MainRB.constraints = RigidbodyConstraints2D.FreezeRotation;
                CornerGrb = false;
                MainAnimator.SetBool("GrabCorner", false);
                ProgressManagerScript.CanMove = true;
                CanGrab = false;
                ProgressManagerScript.CanAim = true;
                ReleaseTheLedge=false;
               // CapsuleCollider2D capsuleCollider2D = new CapsuleCollider2D();
               // capsuleCollider2D.enabled = true;
            }
        }
    }

    void EndOfClimbing()
    {
        CanGrab = true;
        ProgressManagerScript.CanAim = true;
        //CapsuleCollider2D capsuleCollider2D = new CapsuleCollider2D();
        //capsuleCollider2D.enabled = true;

    }

    #region Wall And Ground Detection
    private bool WallSlideControl()
    {
        Collider2D WallDetector = Physics2D.OverlapCircle(WallJumpCircleCenter.position, WallJumpCircleRadius, WallLayer);
        bool DoSlide = (WallDetector != null && !ProgressManagerScript.IsOnTheGround);
        MainAnimator.SetBool("WallSlide", DoSlide);
        return (DoSlide);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(WallJumpCircleCenter.position, WallJumpCircleRadius);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "ETop")
        {
            ProgressManagerScript.IsOnTheGround = true;
            MainAnimator.SetBool("Falling", false);
            MainAnimator.SetBool("Jump", false);
            CanGrab = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Etop")
        {
            ProgressManagerScript.IsOnTheGround = false;
            MainAnimator.SetBool("Run", false);
            MainAnimator.SetBool("Falling", true);
            MainAnimator.SetBool("SuperAttack", false);
        }
    }
    #endregion




    #region Movement Functions that are called from inside of the animations
    void StopMoving() //stops moving before attacks (from the animations)
    {
        ProgressManagerScript.CanMove = false;
    }
    void StartMoving() //starts moving after attacks (from the animations)
    {
        ProgressManagerScript.CanMove = true;
        MainRB.gravityScale = 1;
    }
    void ReadyForNextAttack() //for better input reading (from the animations)
    {
        MainAnimator.SetBool("Attack", false);
        MainAnimator.SetBool("SuperAttack", false);
    }

    void AirStop() //stops moving before air attacks (from the animations)
    {
        MainRB.constraints = RigidbodyConstraints2D.FreezePosition;
        ProgressManagerScript.CanMove = false;

    }
    void AirStart() //starts moving after air attacks (from the animations)
    {
        MainRB.constraints = RigidbodyConstraints2D.None;
        ProgressManagerScript.CanMove = true;
        MainRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void AirAttackGravityControl() 
    {
        if (!ProgressManagerScript.IsOnTheGround)
        {
            MainRB.gravityScale = 3;
        }
        if (ProgressManagerScript.IsOnTheGround)
        {
            MainRB.gravityScale = 1;
        }
    }
    #endregion
}
