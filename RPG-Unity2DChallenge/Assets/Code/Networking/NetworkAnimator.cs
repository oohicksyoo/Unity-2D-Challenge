using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SocketIO;
using UnityEngine;

namespace Project.Networking {
    [RequireComponent(typeof(NetworkIdentity))]
    [RequireComponent(typeof(Animator))]
    public class NetworkAnimator : MonoBehaviour {

        private NetworkIdentity networkIdentity;
        private Animator animator;
        private NetworkAnimatorState animatorState;
        private NetworkAnimatorDirection animatorDirection;
        private AnimatorData animatorData;
        private List<KeyValuePair<NetworkAnimatorDirection, Vector2Int>> directionPairs = new List<KeyValuePair<NetworkAnimatorDirection, Vector2Int>>() {
            {new KeyValuePair<NetworkAnimatorDirection, Vector2Int>(NetworkAnimatorDirection.Idle, new Vector2Int(0, 0))},
            {new KeyValuePair<NetworkAnimatorDirection, Vector2Int>(NetworkAnimatorDirection.Down, new Vector2Int(0, -1))},
            {new KeyValuePair<NetworkAnimatorDirection, Vector2Int>(NetworkAnimatorDirection.Up, new Vector2Int(0, 1))},
            {new KeyValuePair<NetworkAnimatorDirection, Vector2Int>(NetworkAnimatorDirection.Right, new Vector2Int(1, 0))},
            {new KeyValuePair<NetworkAnimatorDirection, Vector2Int>(NetworkAnimatorDirection.Left, new Vector2Int(-1, 0))}
        };

        //Animation hashes
        int walking = Animator.StringToHash("Base Layer.Walking");
        int attacking = Animator.StringToHash("Base Layer.Attack");

        public void Start () {
            animatorState = NetworkAnimatorState.Walking;
            animatorDirection = NetworkAnimatorDirection.Down;
            networkIdentity = GetComponent<NetworkIdentity>();
            animator = GetComponent<Animator>();
            animatorData = new AnimatorData();

            NetworkClient.OnAnimationUpdate += onAnimationUpdate;
        }

        public void Update() {
            if (networkIdentity.IsControlling()) {
                AnimatorStateInfo asi = animator.GetCurrentAnimatorStateInfo(0);
                float x = animator.GetFloat("x");
                float y = animator.GetFloat("y");
                //Debug.LogFormat("{0},{1}", x, y);
                NetworkAnimatorDirection nad = getDirection((int)x, (int)y);
                NetworkAnimatorState nas = NetworkAnimatorState.Walking;

                if (asi.fullPathHash == attacking) {
                    nas = NetworkAnimatorState.Attacking;
                }

                if (animatorDirection != nad || animatorState != nas) {
                    animatorDirection = nad;
                    animatorState = nas;
                    animatorData.state = (int)animatorState;
                    animatorData.direction = (int)animatorDirection;
                    networkIdentity.GetSocket().Emit(NetworkTags.ANIMATOR_STATE, new JSONObject(JsonUtility.ToJson(animatorData)));
                    //Debug.LogFormat("Sending: ({0},{1})", (int)x, (int)y);
                }
            }
        }

        public void ForceToIdle() {
            animator.SetFloat("x", 0);
            animator.SetFloat("y", 0);
        }

        private void onAnimationUpdate(SocketIOEvent E) {
            string id = E.data["id"].ToString();
            if(id == networkIdentity.GetID()) {
                animatorState = (NetworkAnimatorState)((int)E.data["state"].f);
                animatorDirection = (NetworkAnimatorDirection)((int)E.data["direction"].f);
                Vector2Int vect = getValues(animatorDirection);
                Debug.LogFormat("Received: ({0},{1})", vect.x, vect.y);
                animator.SetFloat("x", vect.x);
                animator.SetFloat("y", vect.y);

                if(animatorState == NetworkAnimatorState.Attacking) {
                    animator.SetTrigger("isAttacking");
                }
            }
        }

        private NetworkAnimatorDirection getDirection(int X, int Y) {
            X = (Y != 0) ? 0 : X;
            Vector2Int vect = new Vector2Int(X, Y);
            return directionPairs.Single(x => x.Value == vect).Key;
        }

        private Vector2Int getValues(NetworkAnimatorDirection Direction) {
            return directionPairs.First(x => x.Key == Direction).Value;
        }
	}

    public enum NetworkAnimatorState {
        Walking = 0,
        Attacking
    }

    public enum NetworkAnimatorDirection {
        Up = 0,
        Right,
        Down,
        Left,
        Idle
    }
}
