using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class Walk : AbstractBehaviour
    {

        private float dashBoost; 

        // Use this for initialization
        void Start()
        {
            dashBoost = 0; 
            playerInput.OnMovement += onMovement;
            playerInput.OnDash += onDash; 
        }

        // Update is called once per frame
        void Update()
        {
            if (dashBoost > 0)
            {
                dashBoost += playerStats.GetDashLag() * Time.deltaTime;
            }

            if (dashBoost < 0)
            {
                dashBoost = 0; 
            }
        }


        public void onMovement(Vector3 walk)
        {

            Vector2 vel = rb.velocity;
            playerStats.SetFaceDir(walk); 
            rb.velocity = new Vector2(walk.x * playerStats.GetSpeed(), vel.y);

            rb.velocity = new Vector2((Mathf.Sign(walk.x) * dashBoost) +  (walk.x * playerStats.GetSpeed()), vel.y);

        }


        public void OnWall()
        {
            playerInput.OnMovement -= onMovement;
        }

        public void OffWall()
        {
            playerInput.OnMovement += onMovement;
        }

        public void onDash()
        {
            dashBoost = playerStats.GetMaxDashVelocity();
        }


        public void onDashStop()
        {
            if (rb.velocity.x > playerStats.GetMinDashVelocity())
            {
                dashBoost = playerStats.GetMinDashVelocity(); 
            }
        }
    }
}
