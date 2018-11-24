using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project.Player.Player_FlipJoe
{
    public class Dash : AbstractBehaviour
    {

        // Use this for initialization
        void Start()
        {
            playerInput.OnDash += onMaxDash;
            playerInput.OnDashStop += onMinDash; 
        }

        // Update is called once per frame
        void Update()
        {
            if (dashBoost > 0)
            {
                dashBoost += playerStats.GetDashLag();

            }
        }

        public void onMaxDash()
        {
            Vector2 vel = rb.velocity; 

            dashBoost = playerStats.GetMaxDashVelocity();
            rb.velocity = new Vector2(dashBoost * Mathf.Sign(vel.x), rb.velocity.y); 
        }

        public void onMinDash()
        {
            Vector2 vel = rb.velocity;

            dashBoost = playerStats.GetMinDashVelocity();
            rb.velocity = new Vector2(dashBoost * Mathf.Sign(vel.x), rb.velocity.y);
        }

    }
}
