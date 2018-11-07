using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Gameplay {
    public class MapData : MonoBehaviour {

        [SerializeField]
        private string mapName = "";
        [SerializeField]
        private int realmID = -1;
        [SerializeField]
        private Transform startingPosition;

        /*[Header("Extra Data")]
        [SerializeField]
        private SpawnFromRealmData[] extraSpawns;*/

		public int GetRealmID() {
            return realmID;
        }

        public Vector3 GetStartingPosition(int OldRealm) {

            /*if (extraSpawns != null) {
                if (extraSpawns.Any(x => x.FromRealm == OldRealm)) {
                    return extraSpawns.First(x => x.FromRealm == OldRealm).Spawn.position;
                }
            }*/

            return startingPosition.position;
        }
	}

    /*[Serializable]
    public class SpawnFromRealmData {
        public int FromRealm;
        public Transform Spawn;
    }*/
}
