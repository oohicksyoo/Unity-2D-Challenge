using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player {
    public class PlayerStats : MonoBehaviour {

        [Header("Movement")]
        [Range(0.1f, 10)]
        [SerializeField]
        private float speed = 1;
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

		public float GetSpeed() {
            return speed;
        }

        public float GetJumpForce() {
            return jumpForce;
        }
	}
}
