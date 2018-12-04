using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Player.Player_FlipJoe {

    public class CollisionState : MonoBehaviour
    {
        private Collider2D col;

        private PlayerStats_Flippo playerStats; 

        [Header("Ground Checker Settings")]
        [SerializeField]
        private float colCheckRadius;
        [SerializeField]
        private Vector3 groundCheckerOffset;
        [SerializeField]
        private LayerMask groundLayer;


        [SerializeField]
        private Vector2 leftPosition, rightPosition;

        public Action TouchGround = () => { };
        private bool previousGroundState;

        private bool onWall; 

        // Use this for initialization
        void Start()
        {
            col = GetComponent<Collider2D>();
            playerStats = GetComponent<PlayerStats_Flippo>(); 
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            Vector2 pos = playerStats.GetFaceDir() == 1 ? rightPosition : leftPosition;
            pos.x += transform.position.x;
            pos.y += transform.position.y;

            onWall = Physics2D.OverlapCircle(pos, colCheckRadius, groundLayer);
        }

        public bool CheckGround()
        {
            bool currentGroundState = Physics2D.OverlapCircle(transform.position + groundCheckerOffset, colCheckRadius, groundLayer);

            if (currentGroundState && previousGroundState != currentGroundState)
                TouchGround.Invoke();

            previousGroundState = currentGroundState;
            return currentGroundState;
        }

        public bool OnWall
        {
            get { return onWall; }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;

            Vector2[] positions = new Vector2[] { rightPosition, leftPosition, groundCheckerOffset };

            foreach (Vector2 position in positions)
            {
                Vector2 pos = position;
                pos.x += transform.position.x;
                pos.y += transform.position.y;
                Gizmos.DrawWireSphere(pos, colCheckRadius * 1.2f);
            }

        }
    }
}