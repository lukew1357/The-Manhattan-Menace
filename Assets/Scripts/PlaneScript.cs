using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    public float speed;
    private float direction = -1;
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
            transform.position = new Vector3(0f, 0f, 0f);
        }
        
    }
}
