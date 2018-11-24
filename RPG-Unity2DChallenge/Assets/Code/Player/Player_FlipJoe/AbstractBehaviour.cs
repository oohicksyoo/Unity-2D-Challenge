﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class AbstractBehaviour : MonoBehaviour {

        [Header("Class References")]
        protected PlayerInput_Flippo playerInput;        
        protected PlayerStats_Flippo playerStats;
        protected CollisionState collisionState;
        protected Rigidbody2D rb; 

        // Use this for initialization
        void Awake() {
            rb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput_Flippo>();
            playerStats = GetComponent<PlayerStats_Flippo>(); 
            collisionState = GetComponent<CollisionState>();
        }

        void FixedUpdate()
        {
            rb.velocity += new Vector2(rb.velocity.x,playerStats.GetGravity()) * Time.deltaTime; 

        }


        private void OnCollision2D(Collision2D other)
        {
           
        }

    }
}
