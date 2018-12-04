using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay {
    public class Parallax : MonoBehaviour {

        [SerializeField]
        private Transform[] backgrounds;
        public float smoothing = 1.0f;

        private float[] parallaxScales;
        private Transform cam;
        private Vector3 previousCameraPosition;


        public void Awake() {
            cam = Camera.main.transform;
        }

        public void Start () {
            previousCameraPosition = cam.position;

            parallaxScales = new float[backgrounds.Length];
            for (int i = 0; i < backgrounds.Length; i++) {
                parallaxScales[i] = backgrounds[i].position.z * -1;
            }
		}
		
		public void Update () {
            for (int i = 0; i < backgrounds.Length; i++) {
                float parallax = (previousCameraPosition.x - cam.position.x) * parallaxScales[i];
                float backgroundTargetPosX = backgrounds[i].position.x + parallax;
                Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgrounds[i].position.y, backgrounds[i].position.z);
                backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
            }

            previousCameraPosition = cam.position;
		}
	}
}
