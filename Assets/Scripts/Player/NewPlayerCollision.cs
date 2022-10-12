using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerCollision : MonoBehaviour
{
    private NewPlayerMovement playerMovement;
    private PlayerStats playerStats;
    private PlayerRespawn playerRespawn;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = GetComponent<NewPlayerMovement>();
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
        if (collision.gameObject.tag == "Enemy1")
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
        animator.SetBool("isDamage", true);
        if (playerStats.hasHealth)
        {
            playerMovement.TakeDamage(collision.gameObject.transform.position);
        }
        animator.SetBool("isDamage", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "FallDetector")
        {
            playerStats.TakeDamage(1);
            playerRespawn.Respawn();
        }
        if (collision.tag == "Checkpoint")
        {
            Transform spawnPoint = collision.transform.Find("SpawnPoint");
            print("Collision with checkpoint");
            playerRespawn.Checkpoint(spawnPoint.position);
            print(spawnPoint.position);
            Destroy(collision.gameObject);
        }
    }
}
