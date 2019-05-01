﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private Vector3 oldPos;
    private GameObject player;
    private float target;
    private int saveCount;

    void Start()
    {
        saveCount = 0;
        oldPos = Vector3.zero;
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        Vector3 v3 = player.transform.position - oldPos;
        transform.position += new Vector3(v3.x / 4.0f, v3.y / 4.0f);
        oldPos = player.transform.position;
        transform.LookAt(player.transform.position);
        if (GameGenerator.Star / 20 != saveCount)
        {
            target = -5 - GameGenerator.Star / 5 * 0.4f - transform.position.z;
            saveCount = GameGenerator.Star / 20;
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + target / 60f);
    }
}