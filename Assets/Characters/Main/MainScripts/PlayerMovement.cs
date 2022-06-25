using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject Player;
    public Animator MainAnimator;
    public Rigidbody2D MainRB;
    public Transform MainTransfrom;
    [HideInInspector]public bool IsCrouch;

    public Transform WallSlideBox;
    public LayerMask WallLayer;
    public Vector2 WallJumpVec;
    public float WallSlideRadius;
    private bool WallSliding = false;

    public Vector2 JumpVec;
    private bool Jump;
    [HideInInspector]public bool OnTheGround = true;

    public Vector2 RunningSpeed; 
    private bool MoveLeft;
    private bool MoveRight;
    public bool CanMove = true;
    //private bool isright = true;

    [HideInInspector] public float CurrentStamina; //comes from PlayerStamina


    private void Awake()
    {
        Cursor.visible = false;
    }
    void Update()
    {
        CurrentStamina = Player.GetComponent<PlayerStamina>().CurrentStamina;
        if (Input.GetKey(KeyCode.D) && CanMove)
        {
            MoveRight = true;
            if (OnTheGround)
            {
                MainAnimator.SetBool("Run", true);
            }
        }
        else if (Input.GetKey(KeyCode.A) && CanMove)
        {
            MoveLeft = true;
            if (OnTheGround)
            {
                MainAnimator.SetBool("Run", true);
            }
        }
        else
        {
            MoveRight = false;
            MoveLeft = false;
            MainAnimator.SetBool("Run", false);
        }
        if (Input.GetKeyDown(KeyCode.W) && (OnTheGround || WallSliding) && CurrentStamina >= 7)
        {
            Jump = true;
            MainAnimator.SetBool("Jump", true);
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            IsCrouch = !IsCrouch;
            MainAnimator.SetBool("IsCrouch", IsCrouch);
        }
    }
    private void FixedUpdate()
    {
        JumpFunc();
        MoveLeftFunc();
        MoveRightFunc();
        slidecontrol();
    }

    void JumpFunc()
    {
        if (Jump)
        {
            if (!WallSliding)
            {
                MainRB.AddForce(JumpVec * Time.deltaTime);
            }
            else
            {
                MainRB.AddForce(WallJumpVec * Time.deltaTime);
                MainTransfrom.eulerAngles += new Vector3(0, 180, 0);
                WallJumpVec.x = -WallJumpVec.x;
            }

            Jump = !Jump;
        }
    }
    void MoveRightFunc()
    {
        if (MoveRight)
        {
            MainTransfrom.eulerAngles = new Vector3(0, 0, 0);
            WallJumpVec.x = -10000;
            MainTransfrom.Translate(RunningSpeed * Time.deltaTime);
        }
    }
    void MoveLeftFunc()
    {
        if (MoveLeft)
        {
            MainTransfrom.eulerAngles = new Vector3(0, 180, 0);
            MainTransfrom.Translate(RunningSpeed * Time.deltaTime);
            WallJumpVec.x = 10000;
        }
    }
    void slidecontrol()
    {
        Collider2D WallDetector = Physics2D.OverlapCircle(WallSlideBox.position, WallSlideRadius, WallLayer);
        if (WallDetector != null && !OnTheGround)
        {
            MainAnimator.SetBool("WallSlide", true);
            WallSliding = true;
        }
        else
        {
            MainAnimator.SetBool("WallSlide", false);
            WallSliding = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "ETop")
        {
            OnTheGround = true;
            MainAnimator.SetBool("Falling", false);
            MainAnimator.SetBool("Jump", false);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground" || collision.tag == "Etop")
        {
            OnTheGround = false;
            MainAnimator.SetBool("Run", false);
            MainAnimator.SetBool("Falling", true);
            MainAnimator.SetBool("SuperAttack", false);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(WallSlideBox.position, WallSlideRadius);
    }

    void StopMoving() //stops moving before attacks (from the animations)
    {
        CanMove = false;
    }
    void StartMoving() //starts moving after attacks (from the animations)
    {
        CanMove = true;
        MainRB.gravityScale = 1;
    }
    void ReadyForNextAttack() //for better input reading
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
        if (!OnTheGround)
        {
            MainRB.gravityScale = 3;
        }
        if (OnTheGround)
        {
            MainRB.gravityScale = 1;
        }
    }

}
