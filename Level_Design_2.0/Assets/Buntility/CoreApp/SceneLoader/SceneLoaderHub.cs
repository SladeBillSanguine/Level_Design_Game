/* ======================================================
 * SCENE_LOADER_HUB
 * 
 * Loads new Scenes, as long as they weren't loaded yet.
 * 
 * In case you need to be informed once the Loading is done, you can add a Callback.
 */

using System;

namespace Buntility.GameMenu
{
    public static class SceneLoaderHub
    {
        static SceneLoader _sceneLoader;

        public static bool DoesLoadAction { get { return _sceneLoader.DoesLoadAction; } }


        // ----------------------------------------
        // PUBLIC_ACCESSORS
        public static void LoadScene(string inScene, Action inCallback = null)
            => _sceneLoader.LoadScene(inScene, inCallback);

        public static void UnLoadScene(string inScene, Action inCallback = null)
            => _sceneLoader.UnLoadScene(inScene, inCallback);

        public static bool IsSceneLoaded(string inName)
        {
            return _sceneLoader.IsSceneLoaded(inName);
        }

        public static void SetActiveScene(string inScene)
            => _sceneLoader.SetActiveScene(inScene);

        // ----------------------------------------
        // SUBSCRIPTION
        public static void SubscribeSceneLoader(SceneLoader inLoader)
            => _sceneLoader = inLoader;
        public static void UnsubscribeSceneLoader()
            => _sceneLoader = null;

    }
}
