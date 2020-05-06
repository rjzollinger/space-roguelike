﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRoom : MonoBehaviour
{
    public Manager GameState;
    // Start is called before the first frame update
    void Start()
    {
        GameState = GameObject.Find("Manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameState.NextRoom();
        }
    }
}
