using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{

    public class WallStick : AbstractBehaviour
    {

        private float defaultGravity;

        private bool againstWall;

        private Walk playerWalk; 

        // Use this for initialization
        void Start()
        {
            defaultGravity = playerStats.GetGravity();
            playerWalk = GetComponent<Walk>();
            againstWall = false; 
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            if (collisionState.OnWall)
            {
                if (!againstWall)
                {
                    OnStick();
                    Debug.Log("hey");
                    // playerWalk.OnWall();
                    againstWall = true;
                }
            } else
            {
                if (againstWall)
                {
                    OffWall();
                    //playerWalk.OffWall();
                    againstWall = false; 
                }
            }


        }

        private void OnStick()
        {
            if (!collisionState.CheckGround() && rb.velocity.y > 0)
            {
                Debug.Log("hey");
                playerStats.SetGravity(0);
            }
        }

        private void OffWall()
        {
            if (playerStats.GetGravity() != defaultGravity)
            {
                playerStats.SetGravity(defaultGravity);
            }
        }
    }
}
