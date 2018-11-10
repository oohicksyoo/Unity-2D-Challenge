using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player {
    public class PlayerAiming : MonoBehaviour {

        [SerializeField]
        private Transform arm;
        [SerializeField]
        private float rotationOffset = 0;

		public void Start () {

		}
		
		public void Update () {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 dif = mousePosition - transform.position;
            dif.Normalize();
            float rot = Mathf.Atan2(dif.y, dif.x) * Mathf.Rad2Deg;

            Debug.Log(rot);

            if(rot > 90 || rot < -90) {
                transform.localScale = new Vector3(-1, 1, 1);
                arm.rotation = Quaternion.Euler(0, 0, rot - 180f + rotationOffset);
            } else {
                transform.localScale = new Vector3(1, 1, 1);
                arm.rotation = Quaternion.Euler(0, 0, rot + rotationOffset);
            }
            
		}
	}
}
