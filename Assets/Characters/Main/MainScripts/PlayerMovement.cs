using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Inspector Variables
    [Header("Movement Inputs")]
    [SerializeField] private KeyCode MoveRightButton;
    [SerializeField] private KeyCode MoveLeftButton;
    [SerializeField] private KeyCode JumpButton;
    [SerializeField] private KeyCode CrouchButton;

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
    #endregion

    #region Other Variables
    private Animator MainAnimator;
    private Rigidbody2D MainRB;


    [HideInInspector]public bool CanMove;
    [HideInInspector]public bool IsOnTheGround;

    private float CurrentStamina; //comes from PlayerStamina

    private bool Jump;
    private bool MoveLeft;
    private bool MoveRight;
    [HideInInspector]public bool IsCrouch;//for Crouch Script

    bool CharacterDirection, Right = true, Left = false; //Direction Controls
    #endregion

    private void Start()
    {
        MainAnimator=GetComponent<Animator>();
        MainRB=GetComponent<Rigidbody2D>();
        CanMove = true;
        IsOnTheGround = true;
    }

    #region Input Control
    void Update()
    {
        CurrentStamina = GetComponent<PlayerStamina>().CurrentStamina;
        InputControl();
    }

    private void InputControl()
    {
        if (CanMove) //RUN
        {
            MoveRight=Input.GetKey(MoveRightButton);
            MoveLeft=Input.GetKey(MoveLeftButton);
            if (IsOnTheGround)
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

        if (Input.GetKeyDown(JumpButton)&&(IsOnTheGround || WallSlideControl()) && CurrentStamina >= 7)//JUMP
        {
            Jump = true; //it will set to false after executing jump function
            MainAnimator.SetBool("Jump", true);
        }

        if (Input.GetKeyDown(CrouchButton))//Crouch
        {
            IsCrouch = !IsCrouch;
            MainAnimator.SetBool("IsCrouch", IsCrouch);
        }       
    }
    #endregion

    #region Physical Movement Functions
    private void FixedUpdate()
    {
        JumpFunc();
        MoveFunc();
        WallSlideControl();
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
                FlipTheCharacter(!CharacterDirection);
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
             CharacterDirection = Right;
        }
        else if (Direction==Left)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            WallJumpForce.x = 10000;
            CharacterDirection = Left;
        }
    }
    #endregion

    #region Wall And Ground Detection
    private bool WallSlideControl()
    {
        Collider2D WallDetector = Physics2D.OverlapCircle(WallJumpCircleCenter.position, WallJumpCircleRadius, WallLayer);
        bool DoSlide = (WallDetector != null && !IsOnTheGround);
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
            IsOnTheGround = true;
            MainAnimator.SetBool("Falling", false);
            MainAnimator.SetBool("Jump", false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Etop")
        {
            IsOnTheGround = false;
            MainAnimator.SetBool("Run", false);
            MainAnimator.SetBool("Falling", true);
            MainAnimator.SetBool("SuperAttack", false);
        }
    }
    #endregion

    #region Movement Functions that are called from inside of the animations
    void StopMoving() //stops moving before attacks (from the animations)
    {
        CanMove = false;
    }
    void StartMoving() //starts moving after attacks (from the animations)
    {
        CanMove = true;
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
        CanMove = false;

    }
    void AirStart() //starts moving after air attacks (from the animations)
    {
        MainRB.constraints = RigidbodyConstraints2D.None;
        CanMove = true;
        MainRB.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    void AirAttackGravityControl() 
    {
        if (!IsOnTheGround)
        {
            MainRB.gravityScale = 3;
        }
        if (IsOnTheGround)
        {
            MainRB.gravityScale = 1;
        }
    }
    #endregion
}
