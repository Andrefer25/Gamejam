using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3 : MonoBehaviour
{
    public float speed;
    public float waitTime = 5f;
    public Transform[] Limits;
    private int currentLimitIndex = 0;
    //private int randomSpot;
    bool waiting = false;
    private float waitCounter = 0f;
    public int hp = 2;
    public int Damage = 1;
    public GameObject BulletPrefab;
    private int Shootcount = 0;
    private Animator Animator;
    public float ShootDelay = 2;
    public float ShootdelayCounter = 0;
    void Start()
    {
        // rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
        Shootcount = 0;
    }

    private void Update()
    {
        if (waiting == true)
        {
            waitCounter += Time.deltaTime;
            if (waitCounter < waitTime)
                return;
            waiting = false;
            Shoot();

        }
        Transform limit = Limits[currentLimitIndex];
        if (Vector3.Distance(transform.position, limit.position) < 0.5f)
        {
            Animator.SetBool("running", false);
            waitCounter = 0f;
            waiting = true;
            currentLimitIndex = (currentLimitIndex + 1) % Limits.Length;
           // Shoot();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(limit.position.x, transform.position.y, transform.position.z), speed * Time.deltaTime);
            Animator.SetBool("running", true);

        }

        Vector3 direction = Limits[currentLimitIndex].position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(-1f, 1f, 1f);
        else transform.localScale = new Vector3(1f, 1f, 1f);
    }
    private void Shoot()
    {
        Vector3 direction = Vector3.down;
       /* if (transform.localScale.x == 1.0f) direction = Vector2.right;
        else direction = Vector2.left;*/

        GameObject bullet = Instantiate(BulletPrefab, transform.position + direction * 2f, Quaternion.identity);
        bullet.GetComponent<BulletController2>().SetDirection(direction);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bulleta"))
        {
            Hit();
            Shootcount ++;
           
            if (hp == 0 && Shootcount >= 2)
            {
                Animator.SetTrigger("DieSad");
                Debug.Log("MuereTroste");
            }
            //Animacion derrota (sufriendo)
        }
        if (other.gameObject.CompareTag("Bulletb"))
        {
            Hit();
            //Shootcount ++;
            if (hp == 0) Animator.SetTrigger("DieHappy");
            //Animacion derrota (feliz porque te caga)
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
