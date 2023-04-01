using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class planeSpawnScript : MonoBehaviour
{
    public float spawnRate = 10f;
    public Transform leftSpawn;
    public Transform rightSpawn;
    private Transform spawnSide;
    public GameObject plane1;
    public GameObject plane2;

    private int generationNum;
    private float spawnCooldown = 0f;
    private float timeCount = 0f;

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

                System.Random num = new System.Random();
                int planeType = side.Next(1, 3);
                if (planeType == 1)
                {
                    Instantiate(plane1, spawnSide);
                    spawnCooldown = Time.time + spawnRate;
                }
                else
                {
                    Instantiate(plane2, spawnSide);
                    spawnCooldown = Time.time + spawnRate;
                }
                
            }
        }
    }
}
