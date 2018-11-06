﻿using System.Collections;
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
    }
}
