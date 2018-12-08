using Project.Networking;
using Project.Utility;
using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Manager {
    public class GameLobbyManager : MonoBehaviour {

        [Header("Game Lobby Assets")]
        [SerializeField]
        private TextMeshProUGUI timeText;
        [SerializeField]
        private TextMeshProUGUI[] blueTeamText;
        [SerializeField]
        private TextMeshProUGUI[] orangeTeamText;
        [SerializeField]
        private TextMeshProUGUI mapNameText;

        public void Start () {
            NetworkClient.OnLobbyUpdate += onLobbyUpdate;
		}

        private void onLobbyUpdate(SocketIOEvent E) {
            string time = E.data["time"].ToString().RemoveQuotes();
            List<JSONObject> blueTeam = E.data["blueTeam"].list;
            List<JSONObject> orangeTeam = E.data["orangeTeam"].list;
            string map = E.data["mapName"].ToString();

            timeText.text = string.Format("00:{1}{0}", time, (time.Length == 1) ? "0" : "");
            mapNameText.text = string.Format("Map: {0}", map.RemoveQuotes().ToUpper());
            for (int i = 0; i < blueTeamText.Length; i++) {
                blueTeamText[i].text = (string.IsNullOrEmpty(blueTeam[i].ToString())) ? "EMPTY" : blueTeam[i].ToString().RemoveQuotes().ToUpper(); //Do null check
                orangeTeamText[i].text = (string.IsNullOrEmpty(orangeTeam[i].ToString())) ? "EMPTY" : orangeTeam[i].ToString().RemoveQuotes().ToUpper();

                //Debug.LogFormat("{0} | {1}", blueTeam[i].ToString(), orangeTeam[i].ToString());
            }
            
        }
    }
}
