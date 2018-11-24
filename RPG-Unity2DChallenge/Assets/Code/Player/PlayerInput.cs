﻿using Project.Networking;
//using Project.UserInterface;
using Project.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player {
    public class PlayerInput : MonoBehaviour {

        public Action<Vector3> OnMovement = delegate (Vector3 Value) { };
        public Action<Quaternion> OnRotation = delegate (Quaternion Value) { };
        public Action OnAction = delegate () { };
        public Action OnDash = delegate () { };
        public Action OnInteractionRequest = delegate () { };
        public Action OnJump = () => { };
        
        [Header("Movement")]
        [SerializeField]
        private KeyCode up = KeyCode.W;
        [SerializeField]
        private KeyCode down = KeyCode.S;
        [SerializeField]
        private KeyCode left = KeyCode.A;
        [SerializeField]
        private KeyCode right = KeyCode.D;

        /*[Header("Rotation")]
        [SerializeField]
        private LayerMask levelMask;
        [SerializeField]
        private Camera playerCamera;*/

        [Header("Interaction")]
        [SerializeField]
        private KeyCode interaction = KeyCode.E;

        [Header("Jumping")]
        [SerializeField]
        private KeyCode jump = KeyCode.Space;

        [Header("Debugging")]
        [SerializeField]
        private bool isTesting = false;

        /*[Header("Class References")]
        [SerializeField]
        private PlayerManager playerManager;*/

        private NetworkIdentity networkIdentity;

        private Cooldown actionCooldown;
        private Cooldown interactionCooldown;
        private Cooldown dashCooldown;
        private Cooldown jumpCooldown;

        public void Start() {
            actionCooldown = new Cooldown(0.5f);
            interactionCooldown = new Cooldown(0.1f);
            dashCooldown = new Cooldown(6.5f);
            jumpCooldown = new Cooldown(1.0f);

            networkIdentity = GetComponent<NetworkIdentity>();
        }

        public void FixedUpdate () {
            if ((networkIdentity.IsControlling() || isTesting)) {
                checkMovement();                
            }
        }

        public void Update() {
            if ((networkIdentity.IsControlling() || isTesting)) {
                //checkRotation();
                checkActions();
            } else {
                actionCooldown.StartCooldown();
            }
        }

        private void checkMovement() {
            OnMovement.Invoke(new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0));
        }

        /*private void checkRotation() {
            Ray camRay = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, Mathf.Infinity, levelMask)) {
                Vector3 playerToMouse = floorHit.point - playerManager.GetCurrentPosition();
                playerToMouse.y = 0f;
                Quaternion newRotation = Quaternion.identity;
                newRotation = Quaternion.LookRotation(playerToMouse);
                OnRotation.Invoke(newRotation);
            }
        }*/

        private void checkActions() {
            //Action (Attack/Swing)
            //Interaction
            //Dash (Utility?)

            //Update cooldowns
            actionCooldown.CooldownUpdate();
            interactionCooldown.CooldownUpdate();
            dashCooldown.CooldownUpdate();
            jumpCooldown.CooldownUpdate();

            //Handle input
            if (Input.GetMouseButton(0) && !actionCooldown.IsOnCooldown()) {
                actionCooldown.StartCooldown();
                OnAction.Invoke();
            }

            if(Input.GetMouseButton(1) && !dashCooldown.IsOnCooldown()) {
                dashCooldown.StartCooldown();
                OnDash.Invoke();
            }

            if(Input.GetKeyDown(interaction) && !interactionCooldown.IsOnCooldown()) {
                interactionCooldown.StartCooldown();
                OnInteractionRequest.Invoke();
            }

            if (Input.GetKeyDown(jump) && !jumpCooldown.IsOnCooldown()) {
                jumpCooldown.StartCooldown();
                OnJump.Invoke();
            }
        }
    }
}
