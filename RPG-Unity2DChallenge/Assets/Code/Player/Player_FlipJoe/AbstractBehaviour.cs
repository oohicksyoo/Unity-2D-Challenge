using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class AbstractBehaviour : MonoBehaviour {

        protected enum PlayerState {Walking, WallSliding}
        protected PlayerState playerState; 

        [Header("Class References")]
        protected PlayerInput_Flippo playerInput;        
        protected PlayerStats_Flippo playerStats;
        protected CollisionState collisionState;
        protected PlayerAiming playerAiming; 
        protected Rigidbody2D rb;

        // Use this for initialization
        void Awake() {
            rb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput_Flippo>();
            playerStats = GetComponent<PlayerStats_Flippo>(); 
            collisionState = GetComponent<CollisionState>();
        }

        
        protected virtual void ChangeState(PlayerState newState)
        {

        }

    }
}
