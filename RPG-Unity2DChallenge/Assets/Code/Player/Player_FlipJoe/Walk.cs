using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class Walk : AbstractBehaviour
    {

        // Use this for initialization
        void Start()
        {
            playerInput.OnMovement += onMovement; 
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void onMovement(Vector3 walk)
        {
            Vector2 vel = rb.velocity;

            if (walk.x != 0)
                moveDir = Mathf.Sign(walk.x);

            if (vel.x < playerStats.GetMinDashVelocity())
                rb.velocity = new Vector2(walk.x * playerStats.GetSpeed(), vel.y);
            
        }
    }
}
