using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class coinScript : MonoBehaviour
{
    DataScript dataScript;
    public GameObject dataHandler;
    public AudioSource dieAudio;

    // Start is called before the first frame update
    void Start()
    {
        dataHandler = GameObject.FindGameObjectWithTag("DataHandler");
        dataScript = dataHandler.GetComponent<DataScript>();
        dataScript.coins += 10;
        Destroy(gameObject, 2);
        dieAudio = GetComponent<AudioSource>();

    }
}
