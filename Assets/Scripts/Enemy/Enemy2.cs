using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    public Transform Player;
    public float speed = 1f;
    public float retreatDistance = 1f;
    private bool nearWall;
    public int hp = 1;
    public int Damage = 2;
    private Animator Animator;
    private PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Animator = GetComponent<Animator>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.right * 10f,Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.right, 0.2f))
        {
            nearWall = true;
            //Debug.Log("funca");
        }
        else nearWall = false;
        Debug.DrawRay(transform.position, Vector3.left * 10f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.left, 0.2f))
        {
            nearWall = true;
            //Debug.Log("funca");
        }
        else nearWall = false;

        if (Vector2.Distance(transform.position, Player.position) > retreatDistance | nearWall == true)
        {

           /// transform.position = Vector2.MoveTowards(transform.position, Player.position, -speed * Time.deltaTime);
            Animator.SetBool("running", false);
        }

        if (Vector2.Distance(transform.position, Player.position)<retreatDistance && nearWall == false)
        {
            
            transform.position = Vector2.MoveTowards(transform.position, Player.position, -speed * Time.deltaTime);
            Animator.SetBool("running", true);
        }
        // transform.position = Vector3.pr(Player.position + transform.position);
        Vector3 direction = Player.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else transform.localScale = new Vector3(1f, 1f, 1f);
    }
    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Bulleta"))
    //    {
    //        Hit();
    //        //Animator.SetTrigger("DieHappy");
    //        //foreach (Behaviour component in components)
    //        // component.enabled = false;
    //        //Animacion derrota (feliz porque te caga)
    //        if (hp == 0) Animator.SetTrigger("DieHappy");
    //    }
    //    if (other.gameObject.CompareTag("Bulletb"))
    //    {
    //        Hit();
    //        if (hp == 0) Animator.SetTrigger("DieSad");
    //        //Animacion derrota (sufriendo)
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bulleta"))
        {
            Hit();
            //Animator.SetTrigger("DieHappy");
            //foreach (Behaviour component in components)
            // component.enabled = false;
            //Animacion derrota (feliz porque te caga)
            if (hp == 0)
            {
                Animator.SetTrigger("DieHappy");
                playerStats.AddPoints(1);
            }
        }
        if (other.gameObject.CompareTag("Bulletb"))
        {
            Hit();
            if (hp == 0)
            {
                Animator.SetTrigger("DieSad");
                playerStats.AddPoints(-1);
            }
            //Animacion derrota (sufriendo)
        }
    }

    private void Hit()
    {
        hp = hp - 1;
        
    }
    private void DestroyObject()
    {
        
        Destroy(gameObject);
    }
    private void Stop()
    {
        speed = 0;
    }
}
