using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector3 Direction;
    [SerializeField] private float bulletSpeed;
    public AudioClip Sound;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Direction * bulletSpeed;
    }

    public void SetDirection(Vector3 direction)
    {
        Direction = direction;
    }

    public void DestroyBullet()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //GruntScript grunt = other.GetComponent<GruntScript>();
        //JohnMovement john = other.GetComponent<JohnMovement>();
        //if (grunt != null)
        //{
        //    grunt.Hit();
        //}
        //if (john != null)
        //{
        //    john.Hit();
        //}
        DestroyBullet();
    }
}
