using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Project.Utility {
    public class GameobjectSceneTag : MonoBehaviour {

        [SerializeField]
        private Color debugColour = Color.cyan;

		public void Start () {
            Destroy(this);
		}

        public void OnDrawGizmos() {
            Gizmos.color = debugColour;
            Gizmos.DrawIcon(transform.position, "SpawnIcon.png", true);
            //Gizmos.DrawSphere(transform.position, 0.5f);

#if UNITY_EDITOR
            Handles.color = debugColour;
            Handles.DrawWireDisc(transform.position, Vector3.forward, 0.25f);
#endif

        }
    }
}
