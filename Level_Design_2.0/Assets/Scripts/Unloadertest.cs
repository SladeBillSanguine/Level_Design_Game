using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buntility.GameMenu
{
    public class Unloadertest : MonoBehaviour
    {
        [SerializeField] string Unloadszene;
        private SceneLoader loader;
        public void Unload()
        {
            loader.UnLoadScene(Unloadszene);
        }
    }
}
