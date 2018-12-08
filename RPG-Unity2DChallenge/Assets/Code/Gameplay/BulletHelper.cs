using Project.Networking;
using Project.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Gameplay {
    public class BulletHelper : MonoBehaviour {

        [Header("Information")]
        [SerializeField]
        private string collisionLayer;

        [Header("Class References")]
        [SerializeField]
        private NetworkIdentity networkIdentity;

        public void OnCollisionEnter2D(Collision2D collision) {
            Debug.LogFormat("Bullet Collided: {0} | {1} | {2} | {3}", networkIdentity.GetID(), collision.gameObject.name, LayerMask.LayerToName(collision.gameObject.layer), collisionLayer);
            if(LayerMask.LayerToName(collision.gameObject.layer) == collisionLayer) {
                BulletData bd = new BulletData() {
                    id = networkIdentity.GetID().RemoveQuotes()
                };

                networkIdentity.GetSocket().Emit(NetworkTags.DESTROY_BULLET_HELPER, new JSONObject(JsonUtility.ToJson(bd)));
            }
        }
    }

    [Serializable]
    public class BulletData {
        public string id;
    }
}
