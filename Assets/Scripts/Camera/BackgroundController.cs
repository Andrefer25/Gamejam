using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Vector2 movementSpeed;
    private Vector2 offset;
    private Material material;
    private Rigidbody2D playerRb;

    // Start is called before the first frame update
    void Awake()
    {
        material = GetComponent<SpriteRenderer>().material;
        playerRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        offset = (playerRb.velocity.x * 0.1f) * movementSpeed * Time.deltaTime;
        material.mainTextureOffset += offset;
    }
}
