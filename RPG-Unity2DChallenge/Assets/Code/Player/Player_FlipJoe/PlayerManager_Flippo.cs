using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project.Player.Player_FlipJoe
{
    public class PlayerManager_Flippo : MonoBehaviour
    {
        private Animator anim;
        private PlayerInput_Flippo playerInput;
        private CollisionState collisionState; 

        // Use this for initialization
        void Start()
        {
            playerInput = GetComponent<PlayerInput_Flippo>();
            anim = GetComponent<Animator>();
            collisionState = GetComponent<CollisionState>(); 
            playerInput.OnMovement += onMovement;
            playerInput.OnJump += onJump;
            playerInput.OnStopJump += onStopJump;
            playerInput.OnCheckGround = collisionState.CheckGround;
        }

        // Update is called once per frame
        void Update()
        {
            if (collisionState.Collided) { }
                //anim.SetBool("Jump", false);
        }

        private void onMovement(Vector3 move)
        {
            if (move.x > 0 || move.x < 0)
                anim.SetBool("Walk", true);
            else
                anim.SetBool("Walk", false);

        }

        private void onJump()
        {
            anim.SetTrigger("JumpTrigger"); 
        }

        private void onStopJump()
        {
           /// anim.SetBool("Jump", false);

        }

        private void onWallCollision()
        {

        }
    }
}
