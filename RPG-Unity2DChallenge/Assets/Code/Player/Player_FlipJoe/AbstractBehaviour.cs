using System.Collections;
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

        protected float moveDir;
        protected float dashBoost;
        protected Vector2 velocity; 

        // Use this for initialization
        void Awake() {
            rb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput_Flippo>();
            playerStats = GetComponent<PlayerStats_Flippo>(); 
            collisionState = GetComponent<CollisionState>();

            dashBoost = 0;
            moveDir = 1; 
        }

        void FixedUpdate()
        {
            rb.velocity += new Vector2(0,playerStats.GetGravity()) * Time.deltaTime;
        }


        private void OnCollision2D(Collision2D other)
        {
           
        }

    }
}
