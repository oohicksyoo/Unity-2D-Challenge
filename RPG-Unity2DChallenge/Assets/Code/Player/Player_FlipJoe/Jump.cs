using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class Jump : AbstractBehaviour
    {
        // Use this for initialization
        void Start()
        {
            playerInput.OnJump += onMaxJump;
            playerInput.OnStopJump += onMinJump; 
        }

        public void onMaxJump()
        {
            if(collisionState.CheckGround()) 
            {
                Vector2 vel = rb.velocity;
                rb.velocity = new Vector2(vel.x,playerStats.GetMaxJumpVelocity()); 
            }
        }

        public void onMinJump()
        {
            Vector3 vel = rb.velocity;
            if (vel.y > playerStats.GetMinJumpVelocity())
                rb.velocity = new Vector2(vel.x,playerStats.GetMinJumpVelocity());
        }
    }
}
