using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PeopleSpawnScript : MonoBehaviour
{
    public float spawnRate = 3f;
    public Transform leftSpawn;
    public Transform rightSpawn;
    private Transform spawnSide;
    public GameObject person;
    private int generationNum;
    private float spawnCooldown = 0f;
    private float timeCount = 0f;
    private float spawnCounter = 0f;

    DataScript dataScript;
    public GameObject dataHandler;

    private void Start()
    {
        dataHandler = GameObject.FindGameObjectWithTag("DataHandler");
        dataScript = dataHandler.GetComponent<DataScript>();
    }

    void Update()
    {
        SpawnPeople();
        SpawnRate();
    }

    void SpawnPeople()
    {
        bool paused = dataScript.Paused;
        if (!paused)
        {
            timeCount += Time.deltaTime;
            if (Time.time > spawnCooldown)
            {
                System.Random side = new System.Random();
                int spawnNum = side.Next(1, 3);
                if (spawnNum == 1)
                {
                    spawnSide = leftSpawn;
                }
                else
                {
                    spawnSide = rightSpawn;
                }
     
                Instantiate(person, spawnSide);
                spawnCooldown = Time.time + (1/spawnRate);
            }
        }
    }

    void SpawnRate()
    {
        if (spawnCounter < Time.time)
        {
            spawnRate += 0.1f;
            spawnCounter = Time.time + 5f;
        }
    }
}
