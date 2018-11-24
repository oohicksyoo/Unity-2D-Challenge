using Project.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player {
    public class PlayerAiming : MonoBehaviour {

        [SerializeField]
        private NetworkIdentity networkIdentity;
        [SerializeField]
        private Transform arm;
        [SerializeField]
        private float rotationOffset = 0;

        private bool isFacingRight = true;
        private Vector3 currentRotation = Vector3.zero;

		public void Start () {

		}
		
		public void Update () {
            if (networkIdentity.IsControlling()) {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector3 dif = mousePosition - transform.position;
                dif.Normalize();
                float rot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

                //Debug.Log(rot);

                if (rot > 90 || rot < -90) {
                    transform.localScale = new Vector3(-1, 1, 1);
                    currentRotation = new Vector3(0, 0, rot - 180f + rotationOffset);
                    arm.rotation = Quaternion.Euler(currentRotation);
                    isFacingRight = false;
                } else {
                    transform.localScale = new Vector3(1, 1, 1);
                    currentRotation = new Vector3(0, 0, rot + rotationOffset);
                    arm.rotation = Quaternion.Euler(currentRotation);
                    isFacingRight = true;
                }
            }            
		}

        public bool IsFacingRight() {
            return isFacingRight;
        }

        public Vector3 CurrentRotation() {
            return currentRotation;
        }
	}
}
