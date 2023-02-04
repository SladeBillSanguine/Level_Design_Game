/* =========================================
 * DIA_SYS
 * 
 * This is the core Dialogue System.
 * If you want to trigger a Dialogue, use DiaTrigger.cs or AutoTrigger.cs
 * 
 * If one Dialogue is already active, then none other can be started!
 * 
 * For more information to trigger a Dialogue, look into DiaTrigger.cs
 * For more information on how to setup a Dialogue, look into DiaElement.cs
 */

using UnityEngine;
using System;
using Buntility.Input;

namespace Buntility.DialogueSystem
{
    public static class DiaSys
    {
        static bool _activeDialogue;
        public static bool ActiveDialogue { get { return _activeDialogue; } }

        static DiaElement _curDiaElement;
        public static DiaElement curDiaElement { get { return _curDiaElement; } }

        static UIDiaDisplay _diaDisplay;


        static Action<bool> _dialogueStarted;

        

        


        static void setCursorState(bool inState)
        {
            Cursor.visible = inState;

            if (inState)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        // -----------------------------------------
        // PUBLIC_ACCESSORS
        #region publicAccessors
        public static void StartDialogue(DiaElement inDiaElement)
        {
            if (_activeDialogue)
                return;

            if (InputHub.InputEnabled)
                InputHub.DisableExternControls();

            _dialogueStarted?.Invoke(true);

            _activeDialogue = true;
            

            setupDialogue(inDiaElement);
        }
        #endregion


        // -----------------------------------------
        // SETUP_DIALOGUE
        #region setup
        static void setupDialogue(DiaElement inDiaElement)
        {
            if (inDiaElement == null)
            {
                endDialogue();
                return;
            }
            InputHub.DisableExternControls();
            _curDiaElement = inDiaElement;

            //Debug.Log($"DiaSys handling {_curDiaElement.name}");
            
            
            _curDiaElement.StartEvent();
            _curDiaElement.TriggerCommands();

            switch (_curDiaElement.DiaType)
            {
                case EDiaElementType.PassThrough:
                    findNextValidElement();
                    return;
                case EDiaElementType.Empty:
                    // DoNothing! Wait for Feedback
                    return;
                case EDiaElementType.Option:
                      setCursorState(true);
                    _diaDisplay.DisplayDiaElement(inDiaElement);
                    return;
                default:
                    _diaDisplay.DisplayDiaElement(inDiaElement);
                    return;
            }
        }
        #endregion



        // -----------------------------------------
        // NEXT_DIALOGUE
        #region nextDia
        public static void NextDia() => GetDiaFeedback(-1);
        public static void GetDiaFeedback(int inFeedback)
        {
            _curDiaElement.EndEvent();

            if (_curDiaElement.NextDia.Length == 0)
            {
                endDialogue();
                return;
            }

            switch (inFeedback)
            {
                case -1:
                    // NEXT
                    setupDialogue(_curDiaElement.NextDia[0]);
                    return;
                default:
                    // OPTION_WAS_CHOSEN
                    setupDialogue(_curDiaElement.NextDia[inFeedback]);
                    return;
            }
        }

        static void findNextValidElement()
        {
            foreach (DiaElement element in _curDiaElement.NextDia)
            {
                if (!element.IsValid())
                    continue;
                setupDialogue(element);
                return;
            }

            // no valid Element was found - end!
            Debug.Log($"{_curDiaElement.Header}: no Valid Element found for Passthrough. Ending Dialogue!");
        }
        #endregion  


        // -----------------------------------------
        // END_DIALOGUE
        #region end
        static void endDialogue()
        {
            if (!_activeDialogue)
                return;
            
            setCursorState(false);
            _dialogueStarted?.Invoke(false);
            _activeDialogue = false;
            _curDiaElement = null;

            InputHub.EnableExternControls();
        }
        #endregion


        // -----------------------------------------
        // SUBSCRIPTION
        #region subsc
        public static void SubscribeToUIDiaDisplay(UIDiaDisplay inDisplay)
            => _diaDisplay = inDisplay;
        public static void UnsubscribeUIDiaDisplay()
            => _diaDisplay = null;

        public static void SubscribeToDiaStarted(Action<bool> inAction)
            => _dialogueStarted += inAction;
        public static void UnsubscribeDiaStarted(Action<bool> inAction)
            => _dialogueStarted -= inAction;
        #endregion


        
    }
}