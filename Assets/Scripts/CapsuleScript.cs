using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    public float capsuleSpeed = 200;
    private bool checkHit;
    private float detectionRange = 0.5f;
    private float attackCooldown;
    private float isFacingRight = 1f;
    private Animator animator;

    public GameObject player;
    public float playerBounds = 2;
    public float shieldBounds = 3;
    public LayerMask playerLayer;
    public float capsuleDamage = 2;
    public float capsuleAttackSpeed = 2;

    public GameObject deathParticle;

    public GameObject coinObject;

    DataScript dataScript;
    public GameObject dataHandler;

    public float health = 2f;

    

    // get the script on the object (make sure the script is a public class)      



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dataHandler = GameObject.FindGameObjectWithTag("DataHandler");
        dataScript = dataHandler.GetComponent<DataScript>();
        animator = GetComponent<Animator>();
        System.Random rnd = new System.Random();
        int integer = rnd.Next(1, 8);
        animator.SetInteger("personType", integer);
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            Die();
        }

        if (rb.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            animator.SetBool("isRunning", true);
            
        }
        else if (rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        HandleDirection();

        bool paused = dataScript.Paused;
        if (!checkHit && !paused)
        {
            rb.velocity = new Vector3(isFacingRight * capsuleSpeed * Time.deltaTime, 0, 0);

            checkHit = Physics2D.Raycast(transform.position, new Vector2(isFacingRight * detectionRange, 0), detectionRange, playerLayer);
            Debug.DrawRay(transform.position, new Vector2(isFacingRight * detectionRange, 0), Color.blue);
        }
        
        else if (checkHit && !paused)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);

            if (Time.time >= attackCooldown)
            {
                dataScript.TakeDamage(capsuleDamage);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Instantiate(deathParticle, transform.position, Quaternion.identity);
                Instantiate(deathParticle, transform.position, Quaternion.identity);

                attackCooldown = Time.time + capsuleAttackSpeed;
            }
        }
        else if (paused)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            attackCooldown += Time.deltaTime;
        }
    }

    void HandleDirection()
    {
        if (transform.position.x < player.transform.position.x)
        {
            isFacingRight = 1;
        }
        else if (transform.position.x > player.transform.position.x)
        {
            isFacingRight = -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            health--;
        }
    }

    private void Die()
    {
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
        Instantiate(coinObject, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
