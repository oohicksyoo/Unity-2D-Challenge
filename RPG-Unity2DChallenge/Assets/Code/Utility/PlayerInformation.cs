using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utility {
    public class PlayerInformation : Singleton<PlayerInformation> {
        [HideInInspector]
        public string PlayerName = "Player";
        [HideInInspector]
        public bool IsAdmin = false;
        [HideInInspector]
        public Vector3 SpawnLocation = Vector3.zero;
        [HideInInspector]
        public int CurrentRealm = 1;
        [HideInInspector]
        public int OldRealm = -1;

        [Header("Virtual Machine Setup")]
        [SerializeField]
        private CinemachineVirtualCamera virtualCamera;

        public void SetVirtualCamera(Transform Trans) {
            virtualCamera.Follow = Trans;
        }

        public void UnsetVirtualCamera() {
            virtualCamera.Follow = null;
        }
	}

    public enum Team {
        Red = 0,
        Blue = 1
    }
}
