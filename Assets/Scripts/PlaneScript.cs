using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public float speed;
    private float direction;
    private Vector3 pos;

    DataScript dataScript;
    public GameObject dataHandler;

    private void Start()
    {
        System.Random rnd = new System.Random();
        int integer = rnd.Next(20, 80);
        speed = integer / 10000f;
        dataHandler = GameObject.FindGameObjectWithTag("DataHandler");
        dataScript = dataHandler.GetComponent<DataScript>();
        if (transform.position.x < 0)
        {
            direction = 1;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            direction = -1;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool paused = dataScript.Paused;
        if (!paused)
        {
            pos = transform.position;
            transform.position = new Vector3(pos.x + speed * direction, pos.y, pos.z);
        }
        else
        {
            transform.position = new Vector3(pos.x,pos.y,pos.z);
        }
        
    }
}
