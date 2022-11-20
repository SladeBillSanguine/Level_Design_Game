/* =========================================
 * GAME_STATE_HANDLER
 * 
 * GameStateHandler takes care of the communication between the Application Menu and the Game itself.
 * Ensure that you open the Menu via GameStateHandler with GameStateHandler.Singleton.OpenMainMenu();
 * 
 * DEBUG:
 * Input gets activated by GameStateHandler: InputHub.EnableGameControls();
 * Keep that in mind in case you want to change that!
 * 
 * To Load additional Scenes contact SceneLoaderHub directly!
 */

using UnityEngine;
using Buntility.Input;

namespace Buntility.GameMenu
{
    public class GameStateHandler : MonoBehaviour
    {
        public static GameStateHandler Singleton; 

        public bool GameIsLoaded { get { return SceneLoaderHub.IsSceneLoaded(_gameScene); } }

        [SerializeField] UIMainMenu _mainMenu;
        [SerializeField] IntroHandler _introHandler;

        [SerializeField] string _gameScene;

        [SerializeField] bool _skipIntro = false;

        // ---------------------------------------
        // CONSTRUCTION
        private void Awake()
        {
            Singleton = this;
            GameStateHub.SetGameState(EGameState.inStartUp);
        }

        private void Start()
        {
            _mainMenu.SetupCoreAction(goToGame, quitApplication);

            InputHub.SubscribeToEscape(OpenMainMenu);

            if (_skipIntro)
            {
                openMainMenu();
            } else
            {
                doIntro();
            }
            
        }



        // ---------------------------------------
        // DO_CORE_ROUTINES
        void doIntro()
        {
            GameStateHub.SetGameState(EGameState.inIntro);
            _introHandler.DoIntro(openMainMenu);
        }


        public void OpenMainMenu()
        {
            if (openMenuIsAllowed())
                openMainMenu();
        }

        void openMainMenu()
        {
            InputHub.DisableGameControls();
            InputHub.DisableExternControls();

            GameStateHub.SetGameState(EGameState.inMenu_GameLoaded);
            if (GameIsLoaded)
                GameStateHub.SetGameState(EGameState.inMenu_noGameLoaded);

            _mainMenu.OpenMenu();
        }



        // ---------------------------------------
        // START/LOAD_GAME
        void goToGame()
        {
            // GameScene is already loaded
            if (GameIsLoaded)
            {
                loadingDone();
                return;
            }

            // GameScene is not loaded yet - load it!
            loadGame();
        }

        void loadGame()
        {
            GameStateHub.SetGameState(EGameState.loadingScene);
            SceneLoaderHub.LoadScene(_gameScene, loadingDone);
        }

        void loadingDone()
        {
            GameStateHub.SetGameState(EGameState.inGame);

            SceneLoaderHub.SetActiveScene(_gameScene);
            InputHub.EnableGameControls();
            InputHub.EnableExternControls();
            Debug.Log($"{name} enabled InputControls");
        }



        // ---------------------------------------
        // QUIT
        void quitApplication()
        {
            Application.Quit();
        }



        // ---------------------------------------
        // UTILITY
        bool openMenuIsAllowed()
        {
            return (GameStateHub.GameState == EGameState.inGame) ? true : false;
        }

    }
}
