using Cinemachine;
using Project.Networking;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player {
    public class PlayerCamera : MonoBehaviour {
               
		public void Start () {
            NetworkIdentity ni = GetComponent<NetworkIdentity>();
            if(ni.IsControlling()) {
                CinemachineVirtualCamera cvc = FindObjectOfType<CinemachineVirtualCamera>();
                cvc.Follow = transform;
            }
		}
	}
}
