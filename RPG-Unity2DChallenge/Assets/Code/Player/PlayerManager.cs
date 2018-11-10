using Project.Gameplay;
using Project.Networking;
using Project.Scriptable;
using Project.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Player {
    public class PlayerManager : MonoBehaviour {

        /*[Header("User Interface")]
        [SerializeField]
        private TextMeshPro usernameText;
        [SerializeField]
        private GameObject rpgLogo;*/

        /*[Header("Emotes")]
        [SerializeField]
        private EmoteObjects emotes;
        [SerializeField]
        private EmoteHandler emoteHandler;*/

        [Header("Class References")]
        [SerializeField]
        private PlayerInput playerInput;
        [SerializeField]
        private PlayerStats playerStats;
        [SerializeField]
        private Animator animator;
        [SerializeField]
        private NetworkTransform networkTransform;
        [SerializeField]
        private Rigidbody2D rb;

        private Vector3 lastMove;
        private Vector3 oldPosition;

        //Interactions
        //private bool canInteract;
        //private IInteraction interaction;

		public void Start () {
            playerInput.OnMovement += onMovement;
            playerInput.OnAction += onAction;
            playerInput.OnInteractionRequest += onInteractionRequest;
            playerInput.OnDash += onEmote;
            playerInput.OnJump += onJump;
            oldPosition = transform.position;
        }        

        public void Update() {
        }

        public void OnTriggerEnter2D(Collider2D collision) {
            /*if(collision.tag == "Interactable") {
                canInteract = true;
                interaction = collision.GetComponent<IInteraction>();
            }*/
        }

        public void OnTriggerExit2D(Collider2D collision) {
            /*if (collision.tag == "Interactable") {
                canInteract = false;
                interaction = collision.GetComponent<IInteraction>();
            }*/
        }

        public void SetUsername(string Name) {
            //usernameText.text = Name.RemoveQuotes();
        }

        public void SetIsAdmin(bool Value) {
            //rpgLogo.SetActive(Value);
        }

        private void onAction() {
            animator.SetTrigger("isAttacking");
        }

        private void onMovement(Vector3 Move) {
            float multX = (Move.x < 0) ? -1 : 1;
            float multY = (Move.y < 0) ? -1 : 1;
            Move.x = (Move.x == 0) ? 0 : 1;
            Move.x *= multX;
            Move.y = (Move.y == 0) ? 0 : 1;
            Move.y *= multY;

            animator.SetFloat("x", (Move.y != 0) ? 0 : Move.x);
            animator.SetFloat("y", Move.y);

            lastMove = Move;

            networkTransform.SendMoveCommand((int)Move.x, (int)Move.y);

            if (Move.x != 0 && Move.y != 0) {
                Move *= 0.75f; //Half speed when walking diagonal
            }

            transform.position += Move * playerStats.GetSpeed() * Time.deltaTime; //Local move to avoid lag
            oldPosition = transform.position;
        }

        private void onInteractionRequest() {
            //Determine if there is something we can interact with
            /*if(canInteract) {
                //Fire off interactive interface call
                interaction.OnInteract();
            }*/
        }

        private void onEmote() {
            //emoteHandler.SwitchEmote(emotes.Emotes[UnityEngine.Random.Range(0, emotes.Emotes.Length)].Sprites);
        }

        private void onJump() {
            rb.AddForce(Vector2.up * playerStats.GetJumpForce(), ForceMode2D.Impulse);
        }
    }
}
