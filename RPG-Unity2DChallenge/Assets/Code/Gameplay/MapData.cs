using Project.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Gameplay {
    public class MapData : MonoBehaviour {

        [Header("Map Information")]
        [SerializeField]
        private string mapName = "";
        [SerializeField]
        private int realmID = -1;
        [SerializeField]
        private Color backgroundClear = Color.black;

        [Header("Spawn Data")]
        [SerializeField]
        private Transform[] blueSpawns;
        [SerializeField]
        private Transform[] orangeSpawns;

        public int GetRealmID() {
            return realmID;
        }

        public void SetBackgroundColour() {
            Camera.main.backgroundColor = backgroundClear;
        }

        public Vector3 GetStartingPosition(Team Team) {
            switch (Team) {
                case Team.Orange:
                    return orangeSpawns[UnityEngine.Random.Range(0, orangeSpawns.Length)].position;
                case Team.Blue:
                    return blueSpawns[UnityEngine.Random.Range(0, blueSpawns.Length)].position;
                default:
                    return blueSpawns[UnityEngine.Random.Range(0, blueSpawns.Length)].position;
            }
        }

        public Vector3 GetRandomSpawn() {
            List<Transform> newList = new List<Transform>();
            newList.AddRange(orangeSpawns);
            newList.AddRange(blueSpawns);
            return newList[UnityEngine.Random.Range(0, newList.Count)].position;
        }
	}    
}
