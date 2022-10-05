using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeController : MonoBehaviour
{
    public Image[] hearts;
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
        for(int i=hearts.Length-1; i >= 0; i--)
        {
            if(i < playerStats.healthCounter)
            {
                hearts[i].enabled = true;
            } else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
