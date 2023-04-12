using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class player : MonoBehaviour
{

    [Header("movement")]
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float boostSpeed = 8f;

    [Header("Jump")]
    [SerializeField] private float jumpForce = 13f;
    [SerializeField] private float jumpTime = 0.25f;

    [Header("Turn Check")]
    [SerializeField] private GameObject lLeg;
    [SerializeField] private GameObject rLeg;

    [Header("Ground Check")]
    [SerializeField] private float extraHeight = 0.25f; //altura do gorund check para pular
    [SerializeField] private LayerMask whatIsGround;

    [HideInInspector]public bool isFacingRight;

    private Rigidbody2D rb;
    private Collider2D coll;
    private Animator anim;
    private float moveInput;

    private bool isJumping;
    private bool isFalling;
    private float jumpTimeCounter;
    private bool correndo = false;

    private RaycastHit2D groundHit;

    private Coroutine resetTriggerCoroutine;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        coll = GetComponent<Collider2D>();
    }

    private void Update()
    {
        Move();
        EstaCorrendo();
        Jump();
    }


    #region Jump
    private void Jump()
    {
        //no exato momento que aperta o botão do Jump && está no chão
        if (UserInput.instance.controls.Jumping.Jump.WasPressedThisFrame() && IsGrounded()) 
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            anim.SetTrigger("jump");
        }
        
        //no momento que o botão do Jump está apertado
        if (UserInput.instance.controls.Jumping.Jump.IsPressed()) 
        {
            if(jumpTimeCounter > 0 && isJumping)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);
                jumpTimeCounter -= Time.deltaTime;
            }
            else if (jumpTimeCounter == 0)
            {
                isFalling = true;
                isJumping = false;

            }
            else
            {
                isJumping = false;
            }
           
        }

        //no momento que solta o botão do Jump
        if (UserInput.instance.controls.Jumping.Jump.WasReleasedThisFrame()) 
        {
            isJumping = false;
            isFalling = true;
        }

        if(!isJumping && CheckForLand())
        {
            anim.SetTrigger("land");
            resetTriggerCoroutine = StartCoroutine(Reset()); // faz resets da corotina de validar se está no chão evitando pular no ar
        }

        

    }

    #endregion

    #region Ground/landed Check
    
    private bool IsGrounded()
    {
        groundHit = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, extraHeight, whatIsGround);

        if (groundHit.collider != null)
        {
            return true;
            
        }
        else
        {
            return false;
        }

    }
    

    private bool CheckForLand()
    {
        if(isFalling)
        {
            if (IsGrounded())
            {
                //player has landed
                isFalling = false;

                return true;
            }
            else
            {
                return false;
            }
        }
        else { return false; }  
    }

    private IEnumerator Reset()
    {
        yield return null;

        anim.ResetTrigger("land");
    }


    #endregion

    #region Debug Functions
    private void DrawGroundCheck()
    {
        Color rayColor;

        if(IsGrounded())
        {
            rayColor  = Color.green;
        }

        else
        {
            rayColor  = Color.red;
        }
        Debug.DrawRay(coll.bounds.center + new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, 0), Vector2.down * (coll.bounds.extents.y + extraHeight), rayColor);
        Debug.DrawRay(coll.bounds.center - new Vector3(coll.bounds.extents.x, coll.bounds.extents.y + extraHeight), Vector2.right * (coll.bounds.extents.x + 2), rayColor);

    }
    #endregion

    #region Funções de Movimento/Corrida
    private void Move()
    {
        moveInput = UserInput.instance.moveInput.x;

        

        if(moveInput > 0 || moveInput < 0 ) // animação de movimento
        {
            anim.SetBool("isWalking", true);
            TurnCheck();
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        if (correndo == true)
        {
            rb.velocity = new Vector2(moveInput * boostSpeed, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        }
        




    }

    private void EstaCorrendo()
    {
        //no exato momento em que preciona o botão
        if (UserInput.instance.controls.Running.Run.WasPressedThisFrame())
        {
            correndo = true;
            Debug.Log("---- Está Correndo 1 !");
        }
        else
        { correndo = false; }
        //no momento que o botão está pressionado
        if (UserInput.instance.controls.Running.Run.IsPressed())
        {
            correndo = true;
            Debug.Log("---- Está Correndo 2 !");
        }
        else { correndo = false; }
        //no momento que solta o botão
        if (UserInput.instance.controls.Running.Run.WasReleasedThisFrame())
        {
            correndo = false;
            Debug.Log("Parou de correr!");
        }

    }

    
    #endregion

    #region Turn Checks

    private void StartDirectionCheck()
    {
        if (rLeg.transform.position.x > lLeg.transform.position.x)
        {
            isFacingRight = true;
        }
        else
        {
            isFacingRight = false;
        }
    }

    private void TurnCheck()
    {
        if (UserInput.instance.moveInput.x > 0 && !isFacingRight)
        {
            Turn();
        }
        else if (UserInput.instance.moveInput.x < 0 && isFacingRight)
        {
            Turn();
        }
    }

    private void Turn()
    {
        if(isFacingRight)
        {
            Vector3 rotator = new Vector3(transform.position.x, 180f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
        else
        {
            Vector3 rotator = new Vector3(transform.position.x, 0f, transform.rotation.z);
            transform.rotation = Quaternion.Euler(rotator);
            isFacingRight = !isFacingRight;
        }
    }

    #endregion

}