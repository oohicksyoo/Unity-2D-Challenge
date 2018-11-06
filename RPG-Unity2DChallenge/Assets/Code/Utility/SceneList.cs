﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utility {
    public static class SceneList {
        //Standard levels
        public static string LOGIN = "Login";
        public static string MAIN_MENU_SCREEN = "MainMenu";
        public static string GAME = "Online";

        private static Dictionary<int, string> MapDictionary = new Dictionary<int, string>() {
            {1, "Map_TheHub"},
            {2, "Map_Dungeon"}
        };

        public static string GetMapByIndex(int Value) {
            return MapDictionary[Value];
        }
    }
}
