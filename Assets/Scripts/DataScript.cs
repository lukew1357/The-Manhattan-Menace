using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DataScript : MonoBehaviour
{
    [Header("Stats")]
    public float health;
    public float maxHealth;
    public float damage;
    public float guns;
    public float shield;
    public float MaxShield;
    public float coins = 0;

    [Header("Abilities")]
    public bool hasSpade = false;
    public bool hasClub = false;
    public bool hasHeart = false;
    public bool hasDiamond = false;
    public float spadeCooldown = 5f;
    public float clubCooldown = 5f;
    public float heartCooldown = 5f;
    public float diamondCooldown = 5f;

    public bool Paused = false;

    public GameObject pauseMenu;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinsText;

    void Start()
    {
        health = maxHealth;
        shield = MaxShield;
    }

    void Update()
    {
        AbilityHandler();
        PauseHandler();
        HealthHandler();
    }

    void HealthHandler()
    {
        healthText.text = "health: "+health+"%";
    }

    void PauseHandler()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Paused = !Paused;
        }
        if (Paused)
        {
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenu.SetActive(false);
        }
    }

    void AbilityHandler()
    {
        if (hasSpade && Time.time >= spadeCooldown && Input.GetKeyDown(KeyCode.Alpha1))
        {
            Spade();
        }
        if (hasClub && Time.time >= clubCooldown && Input.GetKeyDown(KeyCode.Alpha2))
        {
            Spade();
        }
        if (hasHeart && Time.time >= heartCooldown && Input.GetKeyDown(KeyCode.Alpha3))
        {
            Spade();
        }
        if (hasDiamond && Time.time >= diamondCooldown && Input.GetKeyDown(KeyCode.Alpha4))
        {
            Spade();
        }
    }

    void Spade()
    {
        Debug.Log("spade activated");
    }

    void Club()
    {
        Debug.Log("club activated");
    }

    void Heart()
    {
        Debug.Log("heart activated");
    }

    void Diamond()
    {
        Debug.Log("diamond activated");
    }

    public void TakeDamage(float damageTaken)
    {
        if (shield <= 0)
        {
            health -= damageTaken;
        }
        else if (shield > 0 && shield < damageTaken)
        {
            float damageDifference = damageTaken - shield;
            shield -= damageTaken;
            health -= damageDifference;
        }
        else if (shield > 0 && shield >= damageTaken)
        {
            shield -= damageTaken;
        }
    }
}
