using Project.Networking;
using Project.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Manager {
    public class MainMenuManager : MonoBehaviour {

        [Header("UI")]
        [SerializeField]
        private TextMeshProUGUI usernameText;
        [SerializeField]
        private TextMeshProUGUI queueTimer;

        [Header("Queue Button")]
        [SerializeField]
        private Button queueButton;
        [SerializeField]
        private TextMeshProUGUI queueButtonText;

        [Header("Leave Queue")]
        [SerializeField]
        private GameObject leaveQueue;

        private NetworkClient networkClient;

        //queue timer
        private float timer;
        private bool isInQueue;

		public void Start () {
            usernameText.text = PlayerInformation.Instance.PlayerName.ToUpper();
            queueButton.interactable = false;
            queueButtonText.color = queueButtonText.color.SetAlpha(0.5f);
            leaveQueue.SetActive(false);
            isInQueue = false;

            NetworkClient.OnValidatedToServer += onServerValidation;
            NetworkClient.OnJoinLobby += onJoinLobby;

            LoaderManager.Instance.LoadLevel(SceneList.ONLINE, (E) => {});
		}

        public void Update() {
            if(isInQueue) {
                timer += Time.deltaTime;
                queueTimer.text = ((int)(timer / 60)).ToString("00") + ":" + ((int)(timer % 60)).ToString("00");
            }
        }

        private void onServerValidation(NetworkClient obj) {
            queueButton.interactable = true;
            queueButtonText.color = queueButtonText.color.SetAlpha(1.0f);
            networkClient = obj;
        }

        public void OnJoinQueue() {
            networkClient.OnJoinQueue();
            queueButton.gameObject.SetActive(false);
            leaveQueue.SetActive(true);
            queueTimer.text = "00:00";
            isInQueue = true;
            timer = 0;
        }

        public void OnLeaveQueue() {
            networkClient.OnLeaveQueue();
            queueButton.gameObject.SetActive(true);
            leaveQueue.SetActive(false);
            isInQueue = false;
        }

        private void onJoinLobby() {
            leaveQueue.SetActive(false);
            isInQueue = false;
            LoaderManager.Instance.LoadLevel(SceneList.GAME_LOBBY, (E) => {
                LoaderManager.Instance.UnLoadLevel(SceneList.MAIN_MENU_SCREEN);
            });
        }
    }
}
