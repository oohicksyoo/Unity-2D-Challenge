using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Networking {
    public class NetworkTags : MonoBehaviour {
        //Socket IO Default
        public const string OPEN = "open";
        public const string CLOSE = "close";
        public const string DISCONNECT = "disconnected";

        //Custom
        public const string GENERAL_EVENT = "generalEvent";
        public const string REGISTER = "register";
        public const string SPAWN = "spawn";
        public const string UPDATE_POSITION = "updatePosition";
        public const string PING = "ping";
        public const string UPDATE_STATS = "updateStats";
        public const string LOAD_REALM = "loadRealm";

        //Chat
        public const string CHAT = "chat";

        //Animation
        public const string ANIMATOR_STATE = "animationState";        

        //Server Spawning
        public const string SERVER_SPAWN = "serverSpawn";
        public const string SERVER_UNSPAWN = "serverUnspawn";

        //Validation
        public const string SERVER_VALIDATION = "serverValidation";
        public const string SERVER_VALIDATION_COMPLETE = "validationComplete";

        //Queue
        public const string JOIN_QUEUE = "joinQueue";
        public const string LEAVE_QUEUE = "leaveQueue";

        //Lobby
        public const string JOIN_LOBBY = "joinLobby";
        public const string LEAVE_LOBBY = "leaveLobby";
        public const string CHANGE_TEAM_LOBBY = "changeTeamLobby";
        public const string LOBBY_UPDATE = "lobbyUpdate";
        public const string START_GAME = "startGame";

        //Game
        public const string SHOOT_BULLET = "shootBullet";
        public const string DESTROY_BULLET_HELPER = "destroyBulletHelper";
    }
}
