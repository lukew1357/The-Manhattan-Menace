using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DataScript : MonoBehaviour
{
    [Header("Stats")]
    public float health = 100f;
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
    public GameObject spade;
    public GameObject club;
    public GameObject heart;
    public GameObject diamond;
    public GameObject spadeMenu;
    public GameObject clubMenu;
    public GameObject heartMenu;
    public GameObject diamondMenu;
    public GameObject spadeCD;
    public GameObject clubCD;
    public GameObject heartCD;
    public GameObject diamondCD;

    public bool usingClub = false;
    private float clubUseTime = 0f;
    private bool usingHeart = false;
    private bool usingDiamond = false;

    public bool Paused = false;

    public bool shieldUnlocked = false;
    public bool shieldEnabled = false;
    public int shieldLevel = 0;
    public GameObject shieldObject;
    public GameObject shieldUI;
    public GameObject shieldSlider;
    public TextMeshProUGUI shieldText;

    public int healthLevel = 0;
    public GameObject healthUI;
    public GameObject healthSlider;

    public GameObject pauseMenu;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI coinsText;

    public GameObject shieldHelpText;
    private float shieldHelpCount = 1f;

    private bool startScreen = true;
    public GameObject startScreenUI;

    PlayerAimWeapon2 playerAimWeapon2;
    public GameObject playerObject;

    public GameObject deathParticle;

    public Image clubImage;

    

    [Header("Audio")]
    public AudioSource music;
    public AudioSource evilAudio;
    public AudioSource damageAudioFile;
    public AudioSource explodeAudio;
    public AudioSource healthUpAudio;
    public AudioSource shieldUpAudio;

    public GameObject damageAudio;
    public GameObject spadeAudio;
    public GameObject heartAudio;
    public GameObject diamondAudio;


    void Start()
    {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerAimWeapon2 = playerObject.GetComponent<PlayerAimWeapon2>();
        health = maxHealth;
        shield = MaxShield;
        startScreen = true;
        music = GetComponent<AudioSource>();
        evilAudio = GetComponent<AudioSource>();
        damageAudioFile = GetComponent<AudioSource>();
        explodeAudio = GetComponent<AudioSource>();
        healthUpAudio = GetComponent<AudioSource>();
        shieldUpAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (health > 0)
        {
            AbilityHandler();
            PauseHandler();
            HealthHandler();
            ShieldHandler();
            CoinsHandler();
            StartHandler();
        }
        else
        {
            Die();
            Debug.Log("You died");
        }
    }

    void CoinsHandler()
    {
        coinsText.text = ""+coins;
    }

    void ShieldHandler()
    {
        shieldText.text = "shield: " + shield + "/" + MaxShield;
        if (Input.GetKeyDown(KeyCode.Space) && shieldUnlocked)
        {
            shieldEnabled = !shieldEnabled;
            shieldObject.SetActive(shieldEnabled);
            
        }
        if (shield == 0)
        {
            shieldObject.SetActive(false);
        }
        if (shieldLevel >= 1)
        {
            shieldUnlocked = true;
            
            if (shieldHelpCount == 1)
            {
                shieldHelpText.SetActive(true);
                Destroy(shieldHelpText, 5);
                shieldHelpCount++;
            }
        }
        if (shieldLevel == 5)
        {
            shieldUI.SetActive(false);
        }
    }

    void HealthHandler()
    {
        healthText.text = "health: "+health+"/"+maxHealth;

        if (healthLevel == 5)
        {
            healthUI.SetActive(false);
            Debug.Log("shield level 5");
        }
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

    void StartHandler()
    {
        if (Input.GetKeyDown(KeyCode.Space) && startScreen)
        {
            startScreen = !startScreen;
            Paused = !Paused;
        }
        if (startScreen)
        {
            startScreenUI.SetActive(true);
            Paused = true;
        }
        else
        {
            startScreenUI.SetActive(false);
        }
    }

    void AbilityHandler()
    {
        if (hasSpade && Time.time >= spadeCooldown)
        {
            spade.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                Spade();
                spade.SetActive(false);
                spadeCooldown = Time.time + 10f;
            }
        }
        if (hasClub && Time.time >= clubCooldown)
        {
            club.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                usingClub = true;
                clubUseTime = Time.time + 3f;
            }
            
        }
        if (hasHeart && Time.time >= heartCooldown)
        {
            heart.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                Heart();
                heart.SetActive(false);
                heartCooldown = Time.time + 10f;
            }
        }
        if (hasDiamond && Time.time >= diamondCooldown)
        {
            diamond.SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                Diamond();
                diamond.SetActive(false);
                diamondCooldown = Time.time + 10f;
            }
        }
        if (usingClub && clubUseTime > Time.time)
        {
            clubCooldown = Time.time + 5f;
            clubImage.color = new Color(255, 255, 255, 0.5f);
        }
        else if (usingClub && clubUseTime <= Time.time)
        {
            playerAimWeapon2.coolDown = 0.2f;
            usingClub = false;
            club.SetActive(false);
            clubCooldown = Time.time + 10f;
            clubImage.color = new Color(255, 255, 255, 1f);
        }
    }

    void Spade()
    {
        Instantiate(spadeAudio, transform.position, Quaternion.identity);
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        foreach (GameObject enemy in enemies)
            coins += 10;

        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
        Instantiate(deathParticle, transform.position, Quaternion.identity);
    }

    void Heart()
    {
        if (health < maxHealth - 20)
        {
            health += 20;
        }
        else
        {
            health = maxHealth;
        }
        Instantiate(heartAudio, transform.position, Quaternion.identity);
    }

    void Diamond()
    {
        if (shield < MaxShield - 20)
        {
            shield += 20;
        }
        else
        {
            shield = MaxShield;
        }
        Instantiate(diamondAudio, transform.position, Quaternion.identity);

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
        Instantiate(damageAudio, transform.position, Quaternion.identity);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game Quit");
    }

    public void ShieldLevelUp()
    {
        if (shieldLevel == 0)
        {
            shieldSlider.SetActive(true);
        }
        if (shieldLevel < 5 && coins >= 100)
        {
            shieldLevel++;
            MaxShield = 100 * (2 ^ (shieldLevel - 1));
            shield = MaxShield;
            shieldSlider.transform.position = new Vector3(shieldSlider.transform.position.x + 260f, shieldSlider.transform.position.y, shieldSlider.transform.position.z);
            coins -= 100;
        }
        if (shieldLevel == 3)
        {
            MaxShield = 400;
            shield = MaxShield;
        }
        if (shieldLevel == 4)
        {
            MaxShield = 500;
            shield = MaxShield;
        }
        Instantiate(diamondAudio, transform.position, Quaternion.identity);
    }

    public void HealthLevelUp()
    {
        if (healthLevel == 0)
        {
            healthSlider.SetActive(true);
        }
        if (healthLevel < 5 && coins >= 100)
        {
            healthLevel++;
            maxHealth = 100 * (2 ^ (healthLevel - 1));
            health = maxHealth;
            healthSlider.transform.position = new Vector3(healthSlider.transform.position.x + 260f, healthSlider.transform.position.y, healthSlider.transform.position.z);
            coins -= 100;
        }
        if (healthLevel == 3)
        {
            maxHealth = 400;
            health = maxHealth;
        }
        if (healthLevel == 4)
        {
            maxHealth = 500;
            health = maxHealth;
        }
        Instantiate(heartAudio, transform.position, Quaternion.identity);
    }

    public void UnlockSpade()
    {
        if (!hasSpade && coins >= 100)
        {
            hasSpade = true;
            coins -= 100;
            spadeMenu.SetActive(false);
        }
    }

    public void UnlockClub()
    {
        if (!hasClub && coins >= 100)
        {
            hasClub = true;
            coins -= 100;
            clubMenu.SetActive(false);
        }
    }

    public void UnlockHeart()
    {
        if (!hasHeart && coins >= 100)
        {
            hasHeart = true;
            coins -= 100;
            heartMenu.SetActive(false);
        }
    }

    public void UnlockDiamond()
    {
        if (!hasDiamond && coins >= 100)
        {
            hasDiamond = true;
            coins -= 100;
            diamondMenu.SetActive(false);
        }
    }

    public void Die()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("coin");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
        SceneManager.LoadScene("SampleScene");
    }
}
