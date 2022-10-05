using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerStats playerStats;
    private PlayerRespawn playerRespawn;
    private Animator animator;
    [SerializeField] Vector2 damageRecoilSpeed = new Vector2(0.05f, 5f);
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerStats = GetComponent<PlayerStats>();
        playerRespawn = GetComponent<PlayerRespawn>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy1")
        {
            playerStats.TakeDamage(1);
            RecoilCollision(collision);
        }
        if (collision.gameObject.tag == "Enemy2")
        {
            playerStats.TakeDamage(1);
            RecoilCollision(collision);
        }
        if (collision.gameObject.tag == "Enemy3")
        {
            playerStats.TakeDamage(1);
            RecoilCollision(collision);
        }
        if (collision.gameObject.tag == "Enemy4")
        {
            playerStats.TakeDamage(1);
            RecoilCollision(collision);
        }
    }

    private void RecoilCollision(Collision2D collision)
    {
        if (playerStats.hasHealth)
        {
            var recoilSpeed = damageRecoilSpeed;
            if (animator.GetBool("isJumping"))
                recoilSpeed = new Vector2(damageRecoilSpeed.x, 0f);
            playerMovement.TakeRecoil(collision.gameObject.transform.position, recoilSpeed, 0.3f, true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "FallDetector")
        {
            playerStats.TakeDamage(1);
            playerRespawn.Respawn();
        }
        if(collision.tag == "Checkpoint")
        {
            Transform spawnPoint = collision.transform.Find("SpawnPoint");
            print("Collision with checkpoint");
            playerRespawn.Checkpoint(spawnPoint.position);
            print(spawnPoint.position);
            Destroy(collision.gameObject);
        }
    }
}
