using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private float horizontal;
    private NewShootController shootController;
    private PlayerStats playerStats;

    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;
    public float decceleration;
    public float velPower;
    [Space(10)]
    public float frictionAmount;

    [Header("Jump")]
    public float jumpForce;
    [Range(0, 1)]
    public float jumpCutMultiplier;
    [Space(10)]
    public float jumpCoyoteTime;
    private float lastGroundedTime;
    [Space(10)]
    public float fallGravityMultiplier;
    private float gravityScale;

    [Header("Attack")]
    public float attackDelay;
    private bool isAttacking;
    [Range(0, 10)]
    public float knockbackForce;
    public float knockbackTime;
    private bool canMove;

    [Header("Damage")]
    [SerializeField] private float invincibilityTime = 1f;
    [SerializeField] Vector2 damageRecoilSpeed = new Vector2(0.05f, 5f);
    [Range(0, 1)]
    [SerializeField] float recoilFreezeTime = 0.3f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        shootController = GetComponent<NewShootController>();
        playerStats = GetComponent<PlayerStats>();
    }

    void Start()
    {
        gravityScale = rb.gravityScale;
        isAttacking = false;
        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        if (IsGrounded())
        {
            lastGroundedTime = jumpCoyoteTime;
        }
        else
        {
            lastGroundedTime -= Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space) && lastGroundedTime > 0 && canMove)
        {
            Jump();
        }
        if(Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0f && canMove)
        {
            OnJumpUp();
            lastGroundedTime = 0;
        }
        if(IsGrounded() && horizontal == 0 && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("isLookingUp", true);
        }
        if(IsGrounded() && Mathf.Abs(horizontal) > 0 && Input.GetKeyDown(KeyCode.W))
        {
            animator.SetBool("isLookingDiag", true);
        }
        if(Input.GetKeyUp(KeyCode.W))
        {
            animator.SetBool("isLookingUp", false);
            animator.SetBool("isLookingDiag", false);
        }
        if(IsGrounded() && Input.GetKeyDown(KeyCode.O) && !isAttacking)
        {
            if(playerStats.CanShootA())
            {
                isAttacking = true;
                animator.SetBool("isShooting", true);
                Invoke("ExecuteShootNoRec", 0.2f);
            }
        }
        if (IsGrounded() && Input.GetKeyDown(KeyCode.P) && !isAttacking)
        {
            if(playerStats.CanShootB())
            {
                isAttacking = true;
                animator.SetBool("isShooting", true);
                Invoke("ExecuteShootRec", 0.2f);
            }
        }
        if (Input.GetKeyUp(KeyCode.O) || Input.GetKeyUp(KeyCode.P))
        {
            animator.SetBool("isShooting", false);
        }
        SetAnimationState();
    }

    private void LateUpdate()
    {
        if(canMove)
            CheckDirectionToFace();
    }

    private void FixedUpdate()
    {
        if(canMove)
        {
            Run();
            Friction();
            #region Jump Gravity
            if (rb.velocity.y < 0)
            {
                rb.gravityScale = gravityScale * fallGravityMultiplier;
            }
            else
            {
                rb.gravityScale = gravityScale;
            }
            #endregion
        }
    }

    private void ExecuteShootRec()
    {
        Knockback();
        shootController.RecShoot();
    }

    private void ExecuteShootNoRec()
    {
        Knockback();
        shootController.NoRecShoot();
    }

    private bool IsGrounded()
    {
        Debug.DrawRay(transform.position, Vector2.down * 1.2f, Color.red);
        return Physics2D.Raycast(transform.position, Vector2.down, 1.2f);
    }

    private void CheckDirectionToFace()
    {
        if (horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if (horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    public void OnJumpUp()
    {
        rb.AddForce(Vector2.down * rb.velocity.y * jumpCutMultiplier, ForceMode2D.Impulse);
    }

    private void Run()
    {
        float targetSpeed = horizontal * moveSpeed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
    }

    private void Friction()
    {
        if (lastGroundedTime > 0 && Mathf.Abs(horizontal) < 0.01f)
        {
            float amount = Mathf.Min(Mathf.Abs(rb.velocity.x), Mathf.Abs(frictionAmount));
            amount *= Mathf.Sign(rb.velocity.x);
            rb.AddForce(Vector2.right * -amount, ForceMode2D.Impulse);
        }
    }

    private void SetAnimationState()
    {
        if(horizontal == 0)
        {
            animator.SetBool("isRunning", false);
            animator.SetBool("isLookingDiag", false);
        }
        if(IsGrounded())
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", false);
        }
        if(Mathf.Abs(horizontal) > 0 && rb.velocity.y <= 0.1f && rb.velocity.y >= -0.1f)
        {
            animator.SetBool("isRunning", true);
            animator.SetBool("isLookingUp", false);
        } else
        {
            animator.SetBool("isRunning", false);
        }
        if(!IsGrounded() && rb.velocity.y > 0.1f)
        {
            animator.SetBool("isJumping", true);
            animator.SetBool("isLookingUp", false);
            animator.SetBool("isLookingDiag", false);
        }
        if(!IsGrounded() && rb.velocity.y < -0.1f)
        {
            animator.SetBool("isJumping", false);
            animator.SetBool("isFalling", true);
        }
    }

    private void AttackComplete()
    {
        isAttacking = false;
    }

    private void Knockback()
    {
        canMove = false;
        //rb.AddForce(new Vector2(-transform.localScale.x * knockbackForce, 0), ForceMode2D.Impulse);
        StartCoroutine(KnockCo());
    }

    private IEnumerator KnockCo()
    {
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(knockbackTime);
        animator.SetBool("isShooting", false);
        canMove = true;
        Invoke("AttackComplete", attackDelay);
    }

    public void TakeDamage(Vector2 shootPoint)
    {
        StartCoroutine(Invencibility());
        StartCoroutine(CantMove(recoilFreezeTime));
        rb.velocity = new Vector2(transform.localScale.x * damageRecoilSpeed.x * shootPoint.x, damageRecoilSpeed.y);
    }

    public IEnumerator CantMove(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    private IEnumerator Invencibility()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        playerStats.canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityTime);
        playerStats.canTakeDamage = true;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }
}
