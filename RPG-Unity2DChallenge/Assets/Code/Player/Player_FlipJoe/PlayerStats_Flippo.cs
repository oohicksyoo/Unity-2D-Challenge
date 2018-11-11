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
        [SerializeField]
        private float gravity;
        [SerializeField]
        private float timeToJumpApex = 0.4f;
        [SerializeField]
        private float maxJumpHeight = 4.0f;
        [SerializeField]
        private float minJumpHeight = 1.0f;

        private float maxJumpVelocity;
        private float minJumpVelocity; 

        void Start()
        {
            gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToJumpApex, 2);
            maxJumpVelocity = Mathf.Abs(gravity) * timeToJumpApex;
            minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight); 
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
    }
}
