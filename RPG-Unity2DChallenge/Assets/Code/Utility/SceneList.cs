using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utility {
    public static class SceneList {
        //Standard levels
        public static string LOGIN = "Login";
        public static string MAIN_MENU_SCREEN = "MainMenu";
        public static string ONLINE = "Online";

        private static Dictionary<int, string> MapDictionary = new Dictionary<int, string>() {
            {1, "Map_Testing"},
            {2, "Map_"}
        };

        public static string GetMapByIndex(int Value) {
            return MapDictionary[Value];
        }
    }
}
