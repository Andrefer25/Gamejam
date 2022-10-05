using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoAItemController : MonoBehaviour
{
    private PlayerStats playerStats;
    // Start is called before the first frame update
    void Start()
    {
        var GO = GameObject.FindGameObjectWithTag("Player");
        playerStats = GO.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyItem()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("Collision with player");
            playerStats.AddAmmoB(3);
            DestroyItem();
        }
    }
}
