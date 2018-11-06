using Project.Manager;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Project.Utility {
    public class LoaderManager : Singleton<LoaderManager> {

        public delegate void OnLevelLoaded(string LevelName);

        private List<LevelLoadingData> levelsLoading;
        private List<string> currentlyLoadedScenes;

        public override void Awake() {
            base.Awake();
            levelsLoading = new List<LevelLoadingData>();
            currentlyLoadedScenes = new List<string>();
        }

        public void Update() {
            for (int i = levelsLoading.Count - 1; i >= 0; i--) {
                if (levelsLoading[i] == null) {
                    levelsLoading.RemoveAt(i);
                    continue;
                }

                if (levelsLoading[i].AO.isDone) {
                    levelsLoading[i].AO.allowSceneActivation = true;
                    levelsLoading[i].onLevelLoaded.Invoke(levelsLoading[i].SceneName);
                    currentlyLoadedScenes.Add(levelsLoading[i].SceneName);
                    levelsLoading.RemoveAt(i);
                    ApplicationManager.Instance.HideLoadingScreen();
                }
            }
        }

        public void LoadLevel(string LevelName, OnLevelLoaded OnLevelLoaded, bool ShowLoadingScreen = false) {
            bool value = currentlyLoadedScenes.Any(x => x == LevelName);

            if (value) {
                Debug.Log(string.Format("Current level ({0}) is already loaded into the game.", LevelName));
                return;
            }


            LevelLoadingData lld = new LevelLoadingData();
            lld.AO = SceneManager.LoadSceneAsync(LevelName, LoadSceneMode.Additive);
            lld.SceneName = LevelName;
            lld.onLevelLoaded += OnLevelLoaded;
            levelsLoading.Add(lld);

            if (ShowLoadingScreen) {
                ApplicationManager.Instance.ShowLoadingScreen();
            }
        }

        public void UnLoadLevel(string LevelName) {
            //TODO: Check active scenes and see if I can remove the incoming one
            foreach (string item in currentlyLoadedScenes) {
                if (item == LevelName) {
                    SceneManager.UnloadSceneAsync(LevelName);
                    currentlyLoadedScenes.Remove(item);
                    return;
                }
            }

            Debug.LogError(string.Format("Loader Manager: Failed to unload level ({0})", LevelName));
        }
    }

    [Serializable]
    public class LevelLoadingData {
        public AsyncOperation AO;
        public string SceneName;
        public LoaderManager.OnLevelLoaded onLevelLoaded;
    }
}
