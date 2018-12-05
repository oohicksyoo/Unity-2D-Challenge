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
        }

        void OnEnable()
        {
            if (isControlling()) {
                playerInput.OnMovement += onMovement;
                playerInput.OnDash += onDash;
                ToggleScripts(false);
            }
        }

        void OnDisable()
        {
            if (isControlling()) {
                playerInput.OnMovement -= onMovement;
                playerInput.OnDash -= onDash;
                ToggleScripts(true);
            }
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
            if (walk.x < 0)
                playerStats.SetFaceDir(-1);
            else if (walk.x > 0)
                playerStats.SetFaceDir(1);

            rb.velocity = new Vector2((playerStats.GetFaceDir() * dashBoost) +  (walk.x * playerStats.GetSpeed()), vel.y);
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
