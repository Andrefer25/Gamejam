using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour

{
    
    public float speed;
    public float waitTime = 5f;
    public Transform[] Limits;
    private int currentLimitIndex = 0;
    bool waiting = false;
    private float waitCounter = 0f;
    public int hp = 1;
    public int Damage = 1;
    private Animator Animator;
    private PlayerStats playerStats;

    void Start()
    {
        Animator = GetComponent<Animator>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    private void Update()
    {
        if (waiting == true)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < waitTime)
                return;
            waiting = false;
        }
        Transform limit = Limits[currentLimitIndex];
        if (Vector3.Distance(transform.position, limit.position) < 1f) 
        {
            Animator.SetBool("running", false);
            //transform.position = limit.position;
            waitCounter = 0f;
            waiting = true;
            if(currentLimitIndex == 0)
            {
                currentLimitIndex = 1;
            }
            else
            {
                currentLimitIndex = 0;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(limit.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            Animator.SetBool("running",true);
        }

        Vector3 direction = Limits[currentLimitIndex].position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bulleta"))
        {
            Hit();
            if (hp == 0)
            {
                Animator.SetTrigger("DieSad");
                playerStats.AddPoints(-1);
            }

        }
        if (other.gameObject.CompareTag("Bulletb"))
        {
            Hit();
            if (hp == 0)
            {
                Animator.SetTrigger("DieHappy");
                playerStats.AddPoints(1);
            }
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

