using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private int ammoA = 10;
    [SerializeField] private int ammoB = 10;
    [SerializeField] public int healthCounter;
    private int ammoCounterA;
    private int ammoCounterB;
    [HideInInspector] public bool canTakeDamage = true;
    [HideInInspector] public bool hasHealth;
    private PlayerRespawn playerRespawn;
    [SerializeField] private AmmoASliderController ammoASlider;
    [SerializeField] private AmmoBSliderController ammoBSlider;
    private Animator animator;
    [Range(0, 10)]
    [SerializeField] private int playerPoints;
    [SerializeField] GameObject backgroundSano;
    [SerializeField] GameObject backgroundArruinado;
    [SerializeField] GameObject backgroundNeutro;

    private SpriteRenderer rendSano;
    private SpriteRenderer rendArruinado;
    private SpriteRenderer rendNeutro;

    // Start is called before the first frame update
    void Start()
    {
        playerRespawn = GetComponent<PlayerRespawn>();
        animator = GetComponent<Animator>();
        hasHealth = healthCounter > 0;
        RestartStats();
        playerPoints = 6;
        rendSano = backgroundSano.GetComponent<SpriteRenderer>();
        rendArruinado = backgroundArruinado.GetComponent<SpriteRenderer>();
        rendNeutro = backgroundNeutro.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerPoints < 5)
        {
            rendNeutro.sortingOrder = -10;
            rendArruinado.sortingOrder = -9;
            rendSano.sortingOrder = -10;
        }
        else if(5 <= playerPoints && playerPoints < 8)
        {
            rendNeutro.sortingOrder = -9;
            rendArruinado.sortingOrder = -10;
            rendSano.sortingOrder = -10;
        }
        else if(playerPoints > 8)
        {
            rendNeutro.sortingOrder = -10;
            rendArruinado.sortingOrder = -10;
            rendSano.sortingOrder = -9;
        }
    }

    public void TakeDamage(int damage)
    {
        if(canTakeDamage)
        {
            healthCounter = Math.Max(healthCounter-damage, 0);
            hasHealth = healthCounter > 0;
        }
        if(healthCounter == 0)
        {
            playerRespawn.RespawnD();
            RestartStats();
        }
    }

    public void AddPoints(int value)
    {
        playerPoints = Math.Max(playerPoints + value, 0);
    }

    private void RestartStats()
    {
        healthCounter = health;
        ammoCounterA = ammoA;
        ammoASlider.SetMaxAmmo(ammoA);
        ammoCounterB = ammoB;
        ammoBSlider.SetMaxAmmo(ammoB);
    }

    public void AddHealth(int cant)
    {
        healthCounter += cant;
    }

    public void AddAmmoA(int cant)
    {
        ammoCounterA = Math.Min(ammoCounterA + cant, ammoA);
        ammoASlider.SetAmmo(ammoCounterA);
    }

    public void AddAmmoB(int cant)
    {
        ammoCounterB = Math.Min(ammoCounterB + cant, ammoA);
        ammoBSlider.SetAmmo(ammoCounterB);
    }

    public void RedAmmoA(int cant)
    {
        ammoCounterA = Math.Max(ammoCounterA - cant, 0);
        ammoASlider.SetAmmo(ammoCounterA);
    }

    public void RedAmmoB(int cant)
    {
        ammoCounterB = Math.Max(ammoCounterB - cant, 0);
        ammoBSlider.SetAmmo(ammoCounterB);
    }

    public bool CanShootA()
    {
        return ammoCounterA > 0 && animator.GetBool("isShooting");
    }

    public bool CanShootB()
    {
        return ammoCounterB > 0 && animator.GetBool("isShooting");
    }
}
