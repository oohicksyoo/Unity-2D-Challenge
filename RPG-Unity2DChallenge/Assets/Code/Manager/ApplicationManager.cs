using Project.Utility;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Manager {
    public class ApplicationManager : Singleton<ApplicationManager> {

        [SerializeField]
        private GameObject userInterface;
        [SerializeField]
        private GameObject loadingScreen;

		public void Start () {
            LoaderManager.Instance.LoadLevel(SceneList.LOGIN, (LevelName) => {
                HideLoadingScreen();
            });
		}
		
		public void Update () {

		}

        public void HideIntroGraphics() {
            userInterface.SetActive(false);
        }

        public void ShowIntroGraphics() {
            userInterface.SetActive(true);
        }

        public void ShowLoadingScreen() {
            loadingScreen.SetActive(true);
        }

        public void HideLoadingScreen() {
            loadingScreen.SetActive(false);
        }
    }
}
