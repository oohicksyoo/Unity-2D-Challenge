using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class WallJump : AbstractBehaviour
    {
        private bool jumpingOffWall;

        private float timeElapsed;
        [SerializeField]
        private float resetDelay; 
        
        
        // Use this for initialization
        void Start()
        {
            playerInput.OnJump += onWallJump; 
        }

        // Update is called once per frame
        void Update()
        {
            if (jumpingOffWall)
            {
                timeElapsed += Time.deltaTime; 

                if (timeElapsed > resetDelay)
                {
                    ToggleScripts(true);
                    jumpingOffWall = false;
                    timeElapsed = 0; 
                }
            }
        }

        private void onWallJump()
        {
            if (collisionState.OnWall && !collisionState.CheckGround())
            {
                if (!jumpingOffWall)
                {
                    rb.velocity =  new Vector2 (playerStats.GetWallJumpVelocity().x * -playerStats.GetFaceDir(), playerStats.GetWallJumpVelocity().y) ;
                    ToggleScripts(false);
                    jumpingOffWall = true;
                }
            }
        }
            

    }
}
