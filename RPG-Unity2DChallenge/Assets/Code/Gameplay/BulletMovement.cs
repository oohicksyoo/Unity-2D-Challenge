using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project {
    public class BulletMovement : MonoBehaviour {

        private Vector3 startLocation;

        public void Start() {
            startLocation = transform.position;
        }

        public void Update () {
            transform.position += Vector3.right * 3 * Time.deltaTime;
		}

        public void OnCollisionEnter2D(Collision2D collision) {
            transform.position = startLocation;
        }
    }
}
