using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Player_FlipJoe
{
    public class PlayerStats_Flippo : MonoBehaviour
    {
        [Header("Movement")]
        [Range(0.1f, 10)]
        [SerializeField]
        private float speed = 2;
        [Range(0.1f, 500)]
        [SerializeField]
        private float jumpForce = 100;

        [Header("Personal Stats")]
        [SerializeField]
        private float health = 1;
        [SerializeField]
        private float damage = 1;
        [SerializeField]
        private float tolerance = 1;

        private float gravity;


        [Header("Jump Stats")]
        [SerializeField]
        private float timeToJumpApex = 0.4f;
        [SerializeField]
        private float maxJumpHeight = 4.0f;
        [SerializeField]
        private float minJumpHeight = 1.0f;

        private float dashLag; 

        [Header("Dash Stats")]
        [SerializeField]
        private float timeToDashMax = 0.4f;
        [SerializeField]
        private float maxDashLength = 4.0f;
        [SerializeField]
        private float minDashLength = 1.0f; 

        private float maxJumpVelocity;
        private float minJumpVelocity;

        private float maxDashVelocity;
        private float minDashVelocity; 

        void Awake()
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);


            dashLag = -(2 * maxDashLength) / Mathf.Pow(timeToDashMax, 2);
            maxDashVelocity = Mathf.Abs(dashLag) * timeToDashMax; 
            minDashVelocity = Mathf.Sqrt(2 * Mathf.Abs(dashLag) * minDashLength);
        }


        public float GetSpeed()
        {
            return speed;
        }

        public float GetJumpForce()
        {
            return jumpForce;
        }

        public float GetGravity()
        {
            return gravity; 
        }

        public float GetMaxJumpVelocity()
        {
            return maxJumpVelocity; 
        }

        public float GetMinJumpVelocity()
        {
            return minJumpVelocity; 
        }

        public float GetMaxDashVelocity()
        {
            return maxDashVelocity; 
        }

        public float GetMinDashVelocity()
        {
            return minDashVelocity;
        }

        public float GetDashLag()
        {
            return dashLag; 
        }

    }
}
