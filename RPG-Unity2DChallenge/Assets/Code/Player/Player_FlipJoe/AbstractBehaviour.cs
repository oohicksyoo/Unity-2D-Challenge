using Project.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class AbstractBehaviour : MonoBehaviour {

        protected enum PlayerState {Walking, WallSliding}
        protected PlayerState playerState;

        [SerializeField]
        private MonoBehaviour[] disableScripts;

        [Header("Class References")]
        protected PlayerInput_Flippo playerInput;        
        protected PlayerStats_Flippo playerStats;
        protected CollisionState collisionState;
        protected PlayerAiming playerAiming; 
        protected Rigidbody2D rb;
        protected NetworkIdentity networkIdentity;

        // Use this for initialization
        void Awake() {
            networkIdentity = GetComponent<NetworkIdentity>();
            rb = GetComponent<Rigidbody2D>();
            playerInput = GetComponent<PlayerInput_Flippo>();
            playerStats = GetComponent<PlayerStats_Flippo>();
            collisionState = GetComponent<CollisionState>();
        }

        protected bool isControlling() {
            return networkIdentity.IsControlling();
        }

        protected virtual void ToggleScripts(bool newValue)
        {
            foreach (MonoBehaviour scripts in disableScripts)
            {
                scripts.enabled = newValue; 
            }
        }


        

    }
}
