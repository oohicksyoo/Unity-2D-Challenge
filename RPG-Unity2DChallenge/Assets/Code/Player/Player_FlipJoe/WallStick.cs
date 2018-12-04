using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{

    public class WallStick : AbstractBehaviour
    {

        private float defaultGravity;

        protected bool againstWall;

        
        // Use this for initialization
        void Start()
        {
            defaultGravity = playerStats.GetGravity();
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
                    ToggleScripts(false); 
                    againstWall = true;
                }
            }
            else
            {
                if (againstWall)
                {
                    OffWall();
                    ToggleScripts(true);
                    againstWall = false; 
                }
            }
        }

        protected virtual void OnStick()
        {
            if (!collisionState.CheckGround())
            {
                playerStats.SetGravity(0);
            }
        }

        protected virtual void OffWall()
        {

            if (playerStats.GetGravity() != defaultGravity)
            {
                playerStats.SetGravity(defaultGravity);
            }
        }
    }
}
