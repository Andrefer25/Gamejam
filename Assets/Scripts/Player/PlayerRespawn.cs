using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRespawn : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;
    // Start is called before the first frame update
    private Vector2 respawnPoint;
    void Start()
    {
        respawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Checkpoint(Vector2 checkpoint)
    {
        respawnPoint = checkpoint;
    }

    public void Respawn()
    {
        StartCoroutine(playerMovement.CantMove(0.5f));
        rb.velocity = new Vector2(0, 0);
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        transform.position = respawnPoint;
    }

    public void RespawnD()
    {
        SceneManager.LoadScene("Integracion");
    }
}
