using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project.Player.Player_FlipJoe
{
    public class WallSlide : WallStick {

        [SerializeField]
        private float slideSpeed; 

	    // Use this for initialization
	    void Start () {
            playerInput.OnMovement += onMovement; 
	    }
	
	    // Update is called once per frame
	    override protected  void Update () {
            base.Update();
            Vector2 vel = rb.velocity;

            if (againstWall && !collisionState.CheckGround())
            {
                rb.velocity = new Vector2(vel.x, -slideSpeed);
            }
        }

        private void onMovement(Vector3 walk)
        {
            Vector2 vel = rb.velocity; 

            if (walk.x != playerStats.GetFaceDir())
            {
              rb.velocity = new Vector2(walk.x * playerStats.GetSpeed(), vel.y);     
            }
        }


        override protected void OnStick()
        {
            ToggleScripts(false); 
            rb.velocity = Vector2.zero;
        }

        override protected void OffWall()
        {
            ToggleScripts(true);

        }
    }
}

