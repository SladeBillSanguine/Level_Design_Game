using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Buntility.Input;

namespace Buntility.GameMenu
{

    public class LoadSzeneTest : MonoBehaviour
    {
        [SerializeField] string _oldScene;
        [SerializeField] GameObject _player;

        private string _newScene;

        void startLoading()
        {
            Debug.Log("loading Scene");
            Destroy(_player);
            SceneLoaderHub.LoadScene(_newScene, unloadOldScene);
        }

        void unloadOldScene()
        {
            Debug.Log("Unloading Scene");
            SceneLoaderHub.SetActiveScene(_newScene);
            SceneLoaderHub.UnLoadScene(_oldScene, finaliseLoad);
        }

        void finaliseLoad()
        {
            //SceneLoaderHub.SetActiveScene(_newScene);
            Debug.Log("LoadDone");
        }


        public void LoadTutorial()
        {
            _newScene = "Tutorial";
            startLoading();
        }
        public void LoadLevel1()
        {
            Debug.Log("loading: " + _newScene);
            _newScene = "Julians Level";
            startLoading();
        }
        public void LoadLevel2()
        {
            _newScene = "Julius Level";
            startLoading();
        }
        public void LoadLevel3()
        {
            _newScene = "Lisas Level";
            startLoading();
        }
        public void LoadLevel4()
        {
            _newScene = "Alicia - UX";
            startLoading();
        }
    }
}
