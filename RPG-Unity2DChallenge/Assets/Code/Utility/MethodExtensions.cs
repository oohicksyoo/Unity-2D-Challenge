using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Utility {
    public static class MethodExtensions {
        
        //Set alpha of a colour property
		public static Color SetAlpha(this Color C, int Alpha) {
            Alpha = Mathf.Clamp(Alpha, 0, 1);
            C.a = Alpha;
            return C;
        }

        public static List<T> ClearInGame<T>(this List<T> List) where T : MonoBehaviour {
            if(List != null) {
                foreach (var item in List) {
                    GameObject.DestroyImmediate(item.gameObject);
                }
            }
            List = new List<T>();

            return List;
        }

        public static T[] SetAllNull<T>(this T[] Array) where T : MonoBehaviour {
            for (int i = 0; i < Array.Length; i++) {
                Array[i] = null;
            }

            return Array;
        }

        public static string RemoveQuotes(this string Value) {
            return Value.Replace("\"", "");
        }
	}
}
