/* =======================================
 * UI_MAIN_MENU
 * 
 * This is a basic Menu which allows to switch between Menu / Game / Options / Credits.
 * Furthermore it will trigger to Quit the Game
 * 
 * If you want to define options, consider NOT touching this Menu, but instead making an own Options-Script.
 * 
 * If you need more menus, make sure to define that in EMenuStates
 */ 

using UnityEngine;
using System;
using TMPro;

namespace Buntility.GameMenu
{
    public class UIMainMenu : MonoBehaviour
    {
        [SerializeField] GameObject _graphics_CoreMenu;

        [SerializeField] GameObject _graphics_MainMenu;
        [SerializeField] TextMeshProUGUI _continueButton;

        [SerializeField] GameObject _graphics_Options;
        [SerializeField] GameObject _graphics_Credits;

        EMenuState _menuState;

        Action _startGame;
        Action _quitGame;

        // -----------------------------
        // CONSTRUCTION
        private void Awake()
        {
            closeMenu();
            disableAll();
        }

        public void SetupCoreAction(Action inStartGame, Action inQuitGame)
        {
            _startGame = inStartGame;
            _quitGame = inQuitGame;
        }
            


        // -----------------------------
        // PUBLIC_ACCESSORS
        public void OpenMenu()
            => setupMenu(EMenuState.inMain);

        public void CloseMenu()
            => setupMenu(EMenuState.closed);

        public void NewGame()
        {
            setupMenu(EMenuState.closed);
            _startGame?.Invoke();
        }

        public void Options()
            => setupMenu(EMenuState.inOptions);

        public void Credits()
            => setupMenu(EMenuState.inCredits);

        public void QuitGame()
        {
            setupMenu(EMenuState.closed);
            _quitGame?.Invoke();
        }


        // -----------------------------
        // SET_MENU
        void setupMenu(EMenuState inState)
        {
            _menuState = inState;
            disableAll();
            switch (_menuState)
            {
                case EMenuState.closed:
                    closeMenu();
                    setCursorState(false);
                    return;
                case EMenuState.inMain:
                    openMenu();
                    _graphics_MainMenu?.SetActive(true);
                    setContinueButtonText();
                    setCursorState(true);
                    return;
                case EMenuState.inOptions:
                    _graphics_Options?.SetActive(true);
                    return;
                case EMenuState.inCredits:
                    _graphics_Credits?.SetActive(true);
                    return;
            }
        }



        // -----------------------------
        // MENU_CLOSE_OPEN
        void openMenu()
            => setMenu(true);
        void closeMenu()
            => setMenu(false);

        void setMenu(bool inState)
        {
            _graphics_CoreMenu.SetActive(inState);
        }

        void disableAll()
        {
            _graphics_MainMenu?.SetActive(false);
            _graphics_Options?.SetActive(false);
            _graphics_Credits?.SetActive(false);
        }



        void setContinueButtonText()
        {
            _continueButton.text = (GameStateHub.GameState == EGameState.inMenu_GameLoaded) ? "Continue Game" : "New Game";
        }


        void setCursorState(bool inState)
        {
            Cursor.visible = inState;

            if (inState)
            {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }


    }


    public enum EMenuState
    {
        closed,
        inMain,
        inOptions,
        inCredits
    }

}