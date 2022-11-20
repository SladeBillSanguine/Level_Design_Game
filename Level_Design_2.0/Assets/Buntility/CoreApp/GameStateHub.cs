/* =============================================
 * GAME_STATE_HUB
 * 
 * Contains all GameStates concerning the MENU!
 * Do NOT add any States here which are responsible for the Game itself!
 * 
 * In case the Game gets paused, you can either get "GameIsPaused" or use SubscribeToPauseGame(Action<bool> inAction) to Subscribe necessary functions!
 */

using System;

namespace Buntility.GameMenu
{
    public static class GameStateHub
    {
        static EGameState _gameState;
        public static EGameState GameState { get { return _gameState; } }

        static bool _gameIsPaused;
        public static bool GameIsPaused { get { return _gameIsPaused; } }
        static Action<bool> _gamePaused;


        // ---------------------------------
        // SET_GAME_STATE
        public static void SetGameState(EGameState inGameState)
        {
            _gameState = inGameState;

            switch (_gameState)
            {
                case EGameState.inMenu_GameLoaded:
                    PauseGame();
                    return;
                case EGameState.inGame:
                    ContinueGame();
                    return;
            }
        }


        // ---------------------------------
        // PAUSE_GAME
        public static void PauseGame()
            => setPauseMode(true);
        public static void ContinueGame()
            => setPauseMode(false);
        static void setPauseMode(bool inState)
        {
            _gameIsPaused = inState;
            
            // Pauses ALL Routines!
            RoutineHub.PauseRoutines = _gameIsPaused;

            // Informs all other Components which need to know if the Game is paused
            _gamePaused?.Invoke(_gameIsPaused);
        }


        // ---------------------------------
        // SUBSCRIPTION
        public static void SubscribeToPauseGame(Action<bool> inAction)
            => _gamePaused += inAction;
        public static void UnsubscribePauseGame(Action<bool> inAction)
            => _gamePaused -= inAction;

    }

    public enum EGameState
    {
        inStartUp,
        inIntro,
        inMenu_noGameLoaded,
        inMenu_GameLoaded,
        inGame,
        loadingScene
    }
}
