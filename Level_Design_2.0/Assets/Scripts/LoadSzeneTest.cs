using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Buntility.GameMenu
{

    public class LoadSzeneTest : MonoBehaviour
    {
        [SerializeField ]string _oldScene;
       
        private string _newScene;
       
        void startLoading()
        {
            SceneLoaderHub.UnLoadScene(_oldScene, loadNewScene);
        }

        void loadNewScene()
        {
            SceneLoaderHub.LoadScene(_newScene, finaliseLoad);
        }

        void finaliseLoad()
        {
            SceneLoaderHub.SetActiveScene(_newScene);
        }

      

        public void LoadTutorial()
        {
            _newScene = "Tutorial";
            startLoading();
        }
        public void LoadLevel1()
        {
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
