using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy4 : MonoBehaviour
{
    public Transform Player;
    public float speed = 1f;
    public float StalkDistance = 2f;
    private bool nearWall;
    public float StalkCooldown = 2f;
    bool waiting = false;
    private float waitCounter = 0f;
    public float StillCooldown = 1f;
    public int hp = 2;
    public int Damage = 1;
    private Animator Animator;
    private int Shootcount = 0;
    private PlayerStats playerStats;
    //public float movecooldown = 1f;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        Animator = GetComponent<Animator>();
        Shootcount = 0;
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //EnemyData hp = gameObject.GetComponent<EnemyData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waiting == true)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < StillCooldown)
                return;
            waiting = false;
        }
        Debug.DrawRay(transform.position, Vector3.right * 10f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.right, 1f))
        {
            nearWall = true;
            //Debug.Log("funca");
        }
        else nearWall = false;
        Debug.DrawRay(transform.position, Vector3.left * 10f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.left, 1f))
        {
            nearWall = true;
            //Debug.Log("funca");
        }
        else nearWall = false;

        Vector3 direction1 = Player.position;
        if (Vector2.Distance(transform.position, Player.position) < StalkDistance && nearWall == false && waiting == false)
        {

            /*if(direction1.y == 0)
            {
                transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            }*/
            /*transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
            if(waitCounter == movecooldown)
            waitCounter = 0f;
            waiting = true;*/
            
            StartCoroutine("Stalk");
        }
        Vector3 direction = Player.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1f, 1f, 1f);
        else transform.localScale = new Vector3(-1f, 1f, 1f);
    }
    IEnumerator Stalk()
    {
        Animator.SetBool("running", true);
        transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        yield return new WaitForSeconds(StalkCooldown);
        Animator.SetBool("running", false);
        waitCounter = 0f;
        waiting = true;
        /* while (true)
         {
             transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
             //StartCoroutine("Stalk");

             yield return new WaitForSeconds(2f);
         }*/
        /*if(direction1.y == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        }*/
        //transform.position = Vector2.MoveTowards(transform.position, Player.position, speed * Time.deltaTime);
        //StartCoroutine("Stalk");

        // yield return new WaitForSeconds(2f);
    }
    //private void OnCollisionEnter2D(Collision2D other)
    //{
    //    if (other.gameObject.CompareTag("Bulleta"))
    //    {
    //        Hit();
    //        //if (hp == 0) Animator.SetTrigger("DieHappy");
    //        //Animacion derrota (feliz porque te caga)
    //    }
    //    if (other.gameObject.CompareTag("Bulletb"))
    //    {
    //        Hit();
    //        Shootcount++;

    //        if (hp == 0 && Shootcount >= 2)
    //        {
    //            //Animator.SetTrigger("DieSad");
    //            Debug.Log("MuereTroste");
    //        }
    //        //Animacion derrota (sufriendo)
    //    }

    //}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bulleta"))
        {
            Hit();
            playerStats.AddPoints(1);
            //if (hp == 0) Animator.SetTrigger("DieHappy");
            //Animacion derrota (feliz porque te caga)
        }
        if (other.gameObject.CompareTag("Bulletb"))
        {
            Hit();
            Shootcount++;

            if (hp == 0 && Shootcount >= 2)
            {
                //Animator.SetTrigger("DieSad");
                playerStats.AddPoints(-1);
                Debug.Log("MuereTroste");
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
