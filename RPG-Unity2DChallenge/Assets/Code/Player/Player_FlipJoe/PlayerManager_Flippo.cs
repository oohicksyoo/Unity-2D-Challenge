using Project.Networking;
using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Project.Player.Player_FlipJoe
{
    public class PlayerManager_Flippo : MonoBehaviour
    {
        [Header("Bullet")]
        [SerializeField]
        private Transform bulletSpawnPoint;

        [Header("Class References")]
        [SerializeField]
        private PlayerAiming playerAiming;
        [SerializeField]
        private NetworkIdentity networkIdentity;

        private Animator anim;
        private PlayerInput_Flippo playerInput;
        private CollisionState collisionState; 

        // Use this for initialization
        void Start()
        {
            playerInput = GetComponent<PlayerInput_Flippo>();
            anim = GetComponent<Animator>();
            collisionState = GetComponent<CollisionState>(); 
            playerInput.OnMovement += onMovement;
            playerInput.OnJump += onJump;
            playerInput.OnStopJump += onStopJump;
            playerInput.OnAction += onAction;
        }
        
        private void onMovement(Vector3 move)
        {
            if (move.x > 0 || move.x < 0)
                anim.SetBool("Walk", true);
            else
                anim.SetBool("Walk", false);

        }

        private void onJump()
        {
            anim.SetTrigger("JumpTrigger"); 
        }

        private void onStopJump()
        {
           /// anim.SetBool("Jump", false);

        }

        private void onAction() {
            //animator.SetTrigger("isAttacking");

            Vector3 facingDirection = (playerAiming.IsFacingRight()) ? bulletSpawnPoint.right : -bulletSpawnPoint.right;

            Bullet bullet = new Bullet();
            bullet.position = new Position();
            bullet.direction = new Position();
            bullet.id = networkIdentity.GetID();
            bullet.position.x = bulletSpawnPoint.position.x.TwoDecimals();
            bullet.position.y = bulletSpawnPoint.position.y.TwoDecimals();
            bullet.direction.x = facingDirection.x.TwoDecimals(); //Grab right direction which would be the gun pointing out
            bullet.direction.y = facingDirection.y.TwoDecimals();
            bullet.isRight = playerAiming.IsFacingRight();
            networkIdentity.GetSocket().Emit(NetworkTags.SHOOT_BULLET, new JSONObject(JsonUtility.ToJson(bullet)));
        }

        private void onWallCollision()
        {

        }
    }
}
