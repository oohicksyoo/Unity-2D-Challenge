//using Project.Gameplay;
using Project.Manager;
//using Project.Player;
using Project.Scriptable;
using Project.Utility;
using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Project.Networking {
    public class NetworkClient : SocketIOComponent {

        private const float PING_SERVER_TIME = 3;

        public static Action<SocketIOEvent> OnConnected = (E) => { };
        public static Action<SocketIOEvent> OnSpawn = (E) => { };
        public static Action<SocketIOEvent> OnDisconnected = (E) => { };
        //public static Action<SocketIOEvent> OnRegistered = (E) => { };
        public static Action<SocketIOEvent> OnChat = (E) => { };
        public static Action<SocketIOEvent> OnAnimationUpdate = (E) => { };
        public static Action<SocketIOEvent> OnUpdateStats = (E) => { };
        public static Action<SocketIOEvent> OnGeneralEvent = (E) => { };
        public static Action<NetworkClient> OnValidatedToServer = (E) => { };
        public static Action OnJoinLobby = () => { };

        [Header("Network Client")]
        [SerializeField]
        private GameObject playerPrefab;
        [SerializeField]
        private Transform playerContainer;

        [Header("Server Objects")]
        [SerializeField]
        private ServerObjects serverObjects;
        [SerializeField]
        private Transform serverContainer;        

        [Header("Ping")]
        [SerializeField]
        private TextMeshProUGUI pingText;

        public static string ClientID { get; private set; }
        public static string ApplicationVersion { get; private set; }

        private List<NetworkIdentity> networkIdentities;
        private float timer;
        private float pingTimer;
        private bool isCountingPing;
        private bool isConnected = true;

        public override void Start () {
            base.Start();
            
            networkIdentities = new List<NetworkIdentity>();
            ApplicationVersion = Application.version;
            timer = 0;
            pingTimer = 0;
            isCountingPing = false;

            On(NetworkTags.OPEN, (E) => {
                Debug.Log("Connection made to server");
                OnConnected.Invoke(E);
            });

            On(NetworkTags.CLOSE, (E) => {
                Debug.Log("On Close!");
                isConnected = false;
            });

            On(NetworkTags.REGISTER, (E) => {
                ClientID = E.data["id"].ToString();
                Debug.LogFormat("Registering: Our Client Is ({0})", ClientID);

                VersionData vd = new VersionData();
                vd.Username = PlayerInformation.Instance.PlayerName;
                vd.version = ApplicationVersion;
                vd.IsAdmin = PlayerInformation.Instance.IsAdmin;
                Emit(NetworkTags.SERVER_VALIDATION, new JSONObject(JsonUtility.ToJson(vd)));
            });

            On(NetworkTags.SPAWN, (E) => {
                OnSpawn.Invoke(E);
                string username = E.data["username"].ToString();
                string id = E.data["id"].ToString();
                bool admin = E.data["isAdmin"].b;
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                Debug.LogFormat("Spawning: Client ({0}:{1}) they are an admin? {2}", username, id, admin);

                if (!networkIdentities.Any(obj => obj.GetID() == id)) {
                    var player = Instantiate(playerPrefab, playerContainer);
                    player.transform.position = PlayerInformation.Instance.SpawnLocation;
                    var ni = player.GetComponent<NetworkIdentity>();
                    ni.SetControllerID(id);
                    ni.SetSocketReference(this);

                    //var pm = player.GetComponent<PlayerManager>();
                    //pm.SetUsername(username);
                    //pm.SetIsAdmin(admin);

                    //Check for game events involving adming players
                    /*if (ni.IsControlling() && admin) {
                        foreach (var item in removeableAreas) {
                            item.SetActive(false);
                        }
                    }*/

                    networkIdentities.Add(ni);
                }
            });

            On(NetworkTags.DISCONNECT, (E) => {               
                string id = E.data["id"].ToString();
                Debug.LogFormat("Disconnected: Client ({0})", id);

                List<NetworkIdentity> clientObjects = networkIdentities.Where(x => x.GetID() == id).ToList();
                foreach (var item in clientObjects) {
                    networkIdentities.Remove(item);
                    DestroyImmediate(item.gameObject);                    
                }

                OnDisconnected.Invoke(E);
            });

            On(NetworkTags.UPDATE_POSITION, (E) => {
                //Debug.LogFormat("JSON DATA: {0}", E.data.ToString());
                string id = E.data["id"].ToString();
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                //Debug.LogFormat("Updating position for player {0}:({1},{2})", id, x, y);
                NetworkIdentity ni = networkIdentities.Single(i => i.GetID() == id);
                ni.transform.position = new Vector3(x, y, 0);
            });

            On(NetworkTags.CHAT, (E) => {              
                OnChat.Invoke(E);
            });

            On(NetworkTags.ANIMATOR_STATE, (E) => {
                OnAnimationUpdate.Invoke(E);
            });

            On(NetworkTags.SERVER_SPAWN, (E) => {                
                string name = E.data["name"].str;
                string id = E.data["id"].ToString();
                float x = E.data["position"]["x"].f;
                float y = E.data["position"]["y"].f;
                Debug.LogFormat("Server wants us to spawn a '{0}'", name);

                if (!networkIdentities.Any(obj => obj.GetID() == id)) {
                    ServerObjectData sod = serverObjects.GetObjectByName(name);
                    var spawnedObject = Instantiate(sod.Prefab, serverContainer);
                    spawnedObject.transform.position = new Vector3(x, y, 0);
                    var ni = spawnedObject.GetComponent<NetworkIdentity>();
                    ni.SetControllerID(id);
                    ni.SetSocketReference(this);

                    networkIdentities.Add(ni);
                }
            });

            On(NetworkTags.SERVER_UNSPAWN, (E) => {
                string id = E.data["id"].ToString();
                NetworkIdentity ni = networkIdentities.Single(i => i.GetID() == id);
                networkIdentities.Remove(ni);
                DestroyImmediate(ni.gameObject);
            });

            On(NetworkTags.SERVER_VALIDATION, (E) => {
                Debug.Log("Close down socket");
                LoaderManager.Instance.LoadLevel(SceneList.LOGIN, (LevelName) => {
                    LoaderManager.Instance.UnLoadLevel(SceneList.ONLINE);
                    ApplicationManager.Instance.ShowIntroGraphics();
                });
                Close();
            });

            On(NetworkTags.PING, (E) => {
                //Ping from server
                isCountingPing = false;
                pingText.text = string.Format("Ping: {0}", (pingTimer * 100).ToString("F0"));
                pingTimer = 0;
            });

            On(NetworkTags.UPDATE_STATS, (E) => {
                OnUpdateStats.Invoke(E);
            });

            On(NetworkTags.GENERAL_EVENT, (E) => {
                OnGeneralEvent.Invoke(E);
            });

            On(NetworkTags.LOAD_REALM, (E) => {
                float realm = E.data["realm"].n;
                float oldRealm = E.data["oldRealm"].n;

                Debug.LogFormat("Leaving Realm ({0}) and Entering Realm ({1})", oldRealm, realm);

                if(oldRealm != -1) {
                    LoaderManager.Instance.UnLoadLevel(SceneList.GetMapByIndex((int)oldRealm));
                }

                PlayerInformation.Instance.OldRealm = (int)oldRealm;
                PlayerInformation.Instance.CurrentRealm = (int)realm;

                LoaderManager.Instance.LoadLevel(SceneList.GetMapByIndex((int)realm), (Level) => {
                    //MapData md = FindObjectsOfType<MapData>().First(x => x.GetRealmID() == PlayerInformation.Instance.CurrentRealm);
                    //PlayerInformation.Instance.SpawnLocation = md.GetStartingPosition(PlayerInformation.Instance.OldRealm);

                    //NetworkIdentity ni = networkIdentities.Single(obj => obj.GetID() == ClientID);
                    //ni.transform.position = PlayerInformation.Instance.SpawnLocation;
                }, true);
            });

            On(NetworkTags.SERVER_VALIDATION_COMPLETE, (E) => {
                OnValidatedToServer.Invoke(this);
            });

            On(NetworkTags.JOIN_LOBBY, (E) => {
                OnJoinLobby.Invoke();
            });
		}

        public override void Update() {
            base.Update();

            //Debug.LogFormat("Network Identities {0}", networkIdentities.Count);

            //Ping update every minute to keep server running smoothly when someone is connected
            timer += Time.deltaTime;
            if(timer >= PING_SERVER_TIME) {
                Emit(NetworkTags.PING);
                timer = 0;
                pingTimer = 0;
                isCountingPing = true;
            }

            if(isCountingPing) {
                pingTimer += Time.deltaTime;
            }  
            
            if(!isConnected) {
                Debug.Log("Close down socket");
                isConnected = true;
                Close();
                LoaderManager.Instance.LoadLevel(SceneList.MAIN_MENU_SCREEN, (LevelName) => {
                    LoaderManager.Instance.UnLoadLevel(SceneList.ONLINE);
                    LoaderManager.Instance.UnLoadLevel(SceneList.GetMapByIndex(PlayerInformation.Instance.CurrentRealm));
                    ApplicationManager.Instance.ShowIntroGraphics();
                });                
            }
        } 

        public void ForceGameClose() {
            Close();
        }
        
        public void ForcePlayerToIdle() {
            NetworkIdentity ni = networkIdentities.Single(i => i.GetID() == ClientID);
            ni.GetComponent<NetworkAnimator>().ForceToIdle();
        }

        public NetworkIdentity GetMyPlayer() {
            return networkIdentities.Single(i => i.GetID() == ClientID);
        }

        public void OnJoinQueue() {
            Emit(NetworkTags.JOIN_QUEUE);
        }

        public void OnLeaveQueue() {
            Emit(NetworkTags.LEAVE_QUEUE);
        }
    }

    [Serializable]
    public class Player {
        public string id;
        public Position position;
    }

    [Serializable]
    public class Position {
        public float x;
        public float y;
    }

    [Serializable]
    public class Chat {
        public string text;
    }

    [Serializable]
    public class AnimatorData {
        public int state;
        public int direction;
    }

    [Serializable]
    public class VersionData {
        public string Username;
        public string version;
        public bool IsAdmin;
    }
}
