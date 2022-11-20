/* ======================================================
 * SCENE_LOADER
 * 
 * Use SceneLoaderHub to trigger the functions in SceneLoader!
 * 
 * By default, SceneLoader loads Async and Additive.
 * Async: the Loading-Process will take care in the background
 * Additive: the new Scene will be ADDED to the hierarchy and will not REPLACE the current Scene
 * 
 * Hence: ensure to always UNLOAD Scenes if not needed!
 * And Exception should be the Scene with Buntility_Hub!
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

namespace Buntility.GameMenu
{
    public class SceneLoader : MonoBehaviour
    {

        bool _doesLoadAction;
        public bool DoesLoadAction { get { return _doesLoadAction; } }

        string _sceneToLoad;
        Action _curCallback;

        List<Scene> _loadedScenes;


        // -------------------------------------------
        // CONSTRUCTION

        private void OnEnable()
        {
            SceneLoaderHub.SubscribeSceneLoader(this);
        }

        private void OnDisable()
        {
            SceneLoaderHub.UnsubscribeSceneLoader();
        }

        // -------------------------------------------
        // PUBLIC_ACCESSORS
        public void LoadScene(string inSceneName, Action inCallback = null)
        {
            if (_doesLoadAction)
            {
                Debug.Log($"Currently Scene {_sceneToLoad} is being handled! Scene {inSceneName} won't be loaded!");
                return;
            }
            if (IsSceneLoaded(inSceneName))
            {
                Debug.Log($"Scene {inSceneName} is already loaded!");
                inCallback?.Invoke();
                return;
            }

            _sceneToLoad = inSceneName;
            _curCallback = inCallback;

            StartCoroutine(loadScene());
        }

        public void UnLoadScene(string inSceneName, Action inCallback = null)
        {
            if (_doesLoadAction) return;

            // CLEAR_LOADED_SCENE_LIST
            if (IsSceneLoaded(inSceneName))
            {
                Debug.Log($"Cannot find scene '{inSceneName}' - is it loaded?");
                return;
            }
            // UNLOAD
            _sceneToLoad = inSceneName;
            StartCoroutine(unLoadScene());
        }

        public void SetActiveScene(string inName)
        {
            SceneManager.SetActiveScene(getSceneByName(inName));
        }

        // -------------------------------------------
        // LOAD_PROCESS
        IEnumerator loadScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(_sceneToLoad, LoadSceneMode.Additive);

            while (!asyncLoad.isDone)
            {
                yield return null;
            }

            finaliseLoad();
        }

        IEnumerator unLoadScene()
        {
            AsyncOperation asyncUnload = SceneManager.UnloadSceneAsync(_sceneToLoad);

            while (!asyncUnload.isDone)
            {
                yield return null;
            }

            finaliseLoad();
        }

        void finaliseLoad()
        {
            SetActiveScene(_sceneToLoad);
            _sceneToLoad = "";

            _doesLoadAction = false;

            updateScenes();

            _curCallback?.Invoke();
        }


        // ------------------------------------------
        // UTILITY
        public bool IsSceneLoaded(string inName)
        {
            updateScenes();
            foreach (Scene loadedScene in _loadedScenes)
            {
                if (loadedScene.name == inName) return true;
            }
            return false;
        }

        void updateScenes()
        {
            _loadedScenes = new List<Scene>();

            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                _loadedScenes.Add(SceneManager.GetSceneAt(i));
            }
        }

        Scene getSceneByName(string inName)
        {
            return SceneManager.GetSceneByName(inName);
        }
    }
}
