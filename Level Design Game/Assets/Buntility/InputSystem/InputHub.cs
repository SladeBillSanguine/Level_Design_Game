/* ===========================================================
 * INPUT_HUB
 * 
 * In here Input is defined.
 * More Information: https://www.youtube.com/watch?v=YHC-6I_LSos
 * 
 * 
 * In general: if you do not need a CallbackContext, simply define static Actions for everything you need and Subscribers. Then let all Scripts which need that Input subscribe.
 * 
 * With this method you can ensure that the ControlSchemes will be only de/activated here!
 * ===========================================================
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;
using UnityEngine;

namespace Buntility.Input
{
    public static class InputHub
    {
        static ControlScheme _controlScheme;

        // Actions defined for all necessary ButtonInputs
        static Action _escapePressed;
        static Action _leftClickPressed;
        static Action _interactPressed;

        public static bool InputEnabled { get { return _inputEnabled; } }
        static bool _inputEnabled = false;

        // ----------------------------------
        // CONSTRUCTION
        static InputHub()
        {
            _controlScheme = new ControlScheme();
            setupControlScheme();
        }

        static void setupControlScheme()
        {
            _controlScheme.Player.Click.performed += doClick;
            _controlScheme.Player.Escape.performed += doEscape;
            _controlScheme.Player.Interact.performed += doInteract;
        }


        // ----------------------------------
        // EN/DISABLE_CONTROLS
        public static void EnableGameControls()
        {
            Debug.Log("EnableControls");
            _controlScheme.Player.Click.Enable();
            _controlScheme.Player.Escape.Enable();
            _controlScheme.Player.Interact.Enable();
        }

        public static void DisableGameControls()
        {
            Debug.Log("DisableControls");
            _controlScheme.Player.Click.Disable();
            _controlScheme.Player.Escape.Disable();
            _controlScheme.Player.Interact.Disable();
        }

        public static void EnableExternControls()
        {
            _inputEnabled = true;
        }
        public static void DisableExternControls()
        {
            _inputEnabled = false;
        }




        // ----------------------------------
        // DEFINED_ACTIONS
        static void doClick(InputAction.CallbackContext inContext)
            => _leftClickPressed?.Invoke();

        static void doEscape(InputAction.CallbackContext inContext)
            => _escapePressed?.Invoke();

        static void doInteract(InputAction.CallbackContext inContext)
            => _interactPressed?.Invoke();

        // ----------------------------------
        // ACTION_SUBSCRIPTION
        public static void SubscribeToEscape(Action inAction)
            => _escapePressed += inAction;
        public static void UnSubscribeEscape(Action inAction)
            => _escapePressed -= inAction;

        public static void SubscribeToClick(Action inAction)
            => _leftClickPressed += inAction;
        public static void UnSubscribeClick(Action inAction)
            => _leftClickPressed -= inAction;

        public static void SubscribeToInteract(Action inAction)
            => _interactPressed += inAction;
        public static void UnSubscribeInteract(Action inAction)
            => _interactPressed -= inAction;

    }
}
