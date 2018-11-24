using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class DoubleJump : AbstractBehaviour
    {
        private bool secondJump;

        // Use this for initialization
        void Start()
        {
            playerInput.OnJump += onMaxJump;
            collisionState.TouchGround += grounded;
        }

        public void onMaxJump()
        {
            if(!collisionState.CheckGround() && !secondJump)
            {
                Vector2 vel = rb.velocity;
                rb.velocity = new Vector2(vel.x,playerStats.GetMaxJumpVelocity());
                secondJump = true;
            }
        }

        public void grounded()
        {
            secondJump = false;
        }
    }
}
