using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private ShootController shootController;
    private PlayerStats playerStats;
    private float horizontal;
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float invincibilityTime = 1f;
    private bool isGrounded;
    private float LastShoot;
    private float coyoteUsable;
    [Range(0f, 1f)]
    [SerializeField] private float coyoteTimeThreshold = 0.15f;
    [HideInInspector]
    public bool canMove;
    private bool shootFreeze;
    [SerializeField] private Vector2 recoilSpeed;
    [SerializeField] private float shootRecoilTime = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        shootController = GetComponent<ShootController>();
        playerStats = GetComponent<PlayerStats>();
        canMove = true;
        shootFreeze = false;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal < 0.0f)
        {
            transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        }
        else if(horizontal > 0.0f)
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }

        Debug.DrawRay(transform.position, Vector2.down * 1f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector2.down, 1f))
        {
            isGrounded = true;
        }
        else isGrounded = false;

        if(isGrounded)
        {
            coyoteUsable = coyoteTimeThreshold;
            animator.SetBool("isJumping", false);
            animator.SetBool("isRunning", horizontal != 0.0f);
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (horizontal == 0.0f)
                {
                    animator.SetBool("isLookingUp", true);
                    animator.SetBool("isLookingDiag", false);
                }
                else
                {
                    animator.SetBool("isLookingDiag", true);
                    animator.SetBool("isLookingUp", false);
                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                animator.SetBool("isLookingDiag", false);
                animator.SetBool("isLookingUp", false);
            }
        } else
        {
            coyoteUsable -= Time.deltaTime;
            animator.SetBool("isJumping", true);
            animator.SetBool("isLookingDiag", false);
            animator.SetBool("isLookingUp", false);
        }

        if (animator.GetBool("isLookingUp") && animator.GetBool("isRunning"))
        {
            animator.SetBool("isLookingDiag", true);
            animator.SetBool("isLookingUp", false);
        }

        if (animator.GetBool("isLookingDiag") && !animator.GetBool("isRunning"))
        {
            animator.SetBool("isLookingDiag", false);
            animator.SetBool("isLookingUp", true);
        }

        if (Input.GetKeyDown(KeyCode.Space) && coyoteUsable > 0f)
        {
            Jump();
        }

        if (Input.GetKeyDown(KeyCode.O) && Time.time > LastShoot + 0.5f)
        {
            animator.SetBool("isShooting", true);
            if (playerStats.CanShootA())
            {
                LastShoot = Time.time;
                StartCoroutine(ExecuteShootA(0.5f));
            }
            else animator.SetBool("isShooting", false);
        }
        if (Input.GetKeyDown(KeyCode.P) && Time.time > LastShoot + 0.5f)
        {
            animator.SetBool("isShooting", true);
            if (playerStats.CanShootB())
            {
                LastShoot = Time.time;
                StartCoroutine(ExecuteShootB(0.5f));
            }
            else animator.SetBool("isShooting", false);
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce);
        animator.SetBool("isRunning", false);
        animator.SetBool("isJumping", true);
    }

    private void AfterShoot()
    {
        if(!animator.GetBool("isLookingUp"))
            TakeRecoil(transform.position, recoilSpeed, shootRecoilTime);
        //animator.SetBool("isShooting", false);
    }



    public void TakeRecoil(Vector2 shootPoint, Vector2 recoilSpeed, float recoilTime, bool isDamage = false)
    {
        if (isDamage) StartCoroutine(Invencibility());
        StartCoroutine(CantMove(recoilTime));
        rb.velocity = new Vector2(transform.localScale.x * recoilSpeed.x * shootPoint.x, recoilSpeed.y);
    }

    public IEnumerator ExecuteShootA(float time)
    {
        animator.SetBool("isShooting", true);
        yield return new WaitForSeconds(time);
        StartCoroutine(ShootFreeze(shootRecoilTime));
        shootController.Shoot();
        animator.SetBool("isShooting", false);
        //AfterShoot();
    }

    public IEnumerator ExecuteShootB(float time)
    {
        animator.SetBool("isShooting", true);
        yield return new WaitForSeconds(time);
        StartCoroutine(ShootFreeze(shootRecoilTime));
        shootController.OtherShoot();
        animator.SetBool("isShooting", false);
        //AfterShoot();
    }

    public IEnumerator CantMove(float time)
    {
        canMove = false;
        yield return new WaitForSeconds(time);
        canMove = true;
    }

    public IEnumerator ShootFreeze(float time)
    {
        shootFreeze = true;
        yield return new WaitForSeconds(time);
        shootFreeze = false;
    }

    private IEnumerator Invencibility()
    {
        Physics2D.IgnoreLayerCollision(6, 7, true);
        playerStats.canTakeDamage = false;
        yield return new WaitForSeconds(invincibilityTime);
        playerStats.canTakeDamage = true;
        Physics2D.IgnoreLayerCollision(6, 7, false);
    }

    private void FixedUpdate()
    {
        if(canMove && !shootFreeze)
        {
            rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        } else if(shootFreeze)
        {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
