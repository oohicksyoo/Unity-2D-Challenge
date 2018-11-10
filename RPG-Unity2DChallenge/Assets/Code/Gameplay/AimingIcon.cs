using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay {
    public class AimingIcon : MonoBehaviour {

        public void Start() {
            Cursor.visible = false;
        }

        public void Update () {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            transform.position = mousePosition;
        }
	}
}
