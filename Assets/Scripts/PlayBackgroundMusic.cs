﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayBackgroundMusic : MonoBehaviour
{
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.player == null)
        {
            player = GameObject.Find("Player").transform;
        }
        this.transform.position = player.position;
    }
}