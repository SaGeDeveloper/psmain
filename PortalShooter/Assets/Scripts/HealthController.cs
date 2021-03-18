﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage(int amount){
        health -= amount;
        if (health <= 0){
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}