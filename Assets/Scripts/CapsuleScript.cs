using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleScript : MonoBehaviour
{
    private Rigidbody2D rb;
    private float capsuleSpeed = 200;
    private bool checkHit;
    private float detectionRange = 0.5f;
    private float attackCooldown;
    private float isFacingRight = 1f;

    public GameObject player;
    public float playerBounds = 2;
    public float shieldBounds = 3;
    public LayerMask playerLayer;
    public float capsuleDamage = 2;
    public float capsuleAttackSpeed = 2;

    DataScript dataScript;
    public GameObject dataHandler;
    
    // get the script on the object (make sure the script is a public class)      

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        dataScript = dataHandler.GetComponent<DataScript>();

        if (transform.position.x < player.transform.position.x)
        {
            isFacingRight = 1;
        }
        else if (transform.position.x > player.transform.position.x)
        {
            isFacingRight = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
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
                attackCooldown = Time.time + capsuleAttackSpeed;
            }
        }
        else if (paused)
        {
            rb.velocity = new Vector3(0f, 0f, 0f);
            attackCooldown += Time.deltaTime;
        }
    }
}
