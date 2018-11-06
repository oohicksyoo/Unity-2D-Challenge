using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Manager {
    public class MainMenuManager : MonoBehaviour {

		public void Start () {
            LoaderManager.Instance.LoadLevel(SceneList.ONLINE, (E) => {

            });
		}
		
		public void Update () {

		}

        public void OnStart() {
            /*LoaderManager.Instance.LoadLevel(SceneList.GAME, delegate (string E) {
                LoaderManager.Instance.UnLoadLevel(SceneList.MAIN_MENU_SCREEN); //Unload after new level is in
                ApplicationManager.Instance.HideIntroGraphics();
            });*/
        }
	}
}
