/* =========================================
 * DIA_ELEMENT
 * 
 * This is ONE DialogueElement which will be displayed in UIDiaDisplay.
 * Connect more DiaElement.cs to _nextDia if you want to make the Dialogue bigger.
 * 
 * 
 * [1] _conditions
 * Contains an Array of CondPusher.cs. There you got to define the conditions which need to apply to show this DiaElement in UIDiaDisplay.
 * All conditions MUST apply!
 * 
 * [2] _commands
 * This will push Data into the Inventory.
 *
 * [3] _diaType
 * There are three EDiaElementTypes:
 *      [a] "Text": This will simply display Text
 *      [b] "Option": This Dialogue will NOT be displayed, but instead it needs further DiaElment.cs
 *      defined as Options in _nextDia. Those will be displayed as Buttons out of which can be choosen.
 *      [c] "PassThrough": this is similar to "Option", except that the Player got no choice. Instead all
 *      _nextDia will be crawled, and the FIRST one with true conditions will be triggered!
 *      [d] "Empty": this will do not display anything. BUT: the conversation does NOT get closed.
 *      DiaSys will wait for Feedback. Use this for example to trigger a Timeline.
 *      
 * [4] _optionText
 * If this DiaElement is an option, then this is the text which will be displayed in the BUTTON
 * 
 * [5] _dispIfInvalid
 * Set this to "false" if you do not want the Player to see invalid Options
 * 
 * [6] _header
 * This is the Header of the UIDiaDisplay. You can insert the name of the speaking character, for example.
 * 
 * [7] _dialogueText
 * This is the Text which gets displayed in the Dialogue.
 * By default, the text should not be longer than 5 rows and exceed 400 characters.
 * 
 * [8] _nextDia
 * Insert the next DiaElement.cs here. If you want to END the conversation, then leave it empty.
 * If you want to have Options, make sure to set THIS DiaElement to "Option" and add more than 1 _nextDia, but not more than 5!
 * If you want to make a hidden branching, set THIS DiaElement to "PassThrough" and ensure to come up with a Condition-Logic so the more unlikely conditions are the UPPER ones, and the DEFAULT the LOWER ones!
 * 
 * [9] _onStartEvent / _onEndEvent
 * Use those Events to trigger whatever necessary at your convenience when this DiaElement starts/ends. For example a Timeline, Playing a Sound, FTBTriggering etc.
 */

using Buntility.Inventory;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Buntility.DialogueSystem
{
    public class DiaElement : MonoBehaviour
    {
        // ------------------------------------
        // COND+COMMAND
        CondPusher[] _conditions;
        CommPusher[] _commands;

        // ------------------------------------
        // OPTION_DATA
        [SerializeField] EDiaElementType _diaType;
        public EDiaElementType DiaType { get { return _diaType; } }


        [SerializeField] string _optionText = "";
        public string OptionText { get { return _optionText; } }


        [SerializeField] bool _dispIfInvalid = true;
        public bool DispIfInvalid { get { return _dispIfInvalid; } }

        // ------------------------------------
        // DISP_DATA
        [SerializeField] string _header;
        public string Header { get { return _header; } }

        [TextArea(5, 10)]
        [SerializeField] string _dialogueText = "DialogueText";
        public string DialogueText { get { return _dialogueText; } }

        [SerializeField] DiaElement[] _nextDia;
        public DiaElement[] NextDia { get { return _nextDia; } }

        const int MAX_CHARS = 400;
        const int MAX_BUTTON_INDEX = 4;
        // ------------------------------------
        // EVENTS
        [SerializeField] UnityEvent _onStartEvent;
        [SerializeField] UnityEvent _onEndEvent;


        // -----------------------------------------
        // CONSTRUCTION
        #region constr
        private void Awake()
        {
            _conditions = GetComponents<CondPusher>();
            _commands = GetComponents<CommPusher>();

            if (_nextDia.Length > MAX_BUTTON_INDEX)
                Debug.LogWarning($"WARNING: the DiaElement {gameObject.name}:{_header} got MORE Options than available Buttons! This might be a potential error!");

            if (_dialogueText.Length > MAX_CHARS)
                Debug.LogWarning($"WARNING: the DiaElement {gameObject.name}:{_header} got DialogueText of {_dialogueText.Length} (Max: {MAX_CHARS}). It might not be displayed properly.");
        }
        #endregion

        // -----------------------------------------
        // PUBLIC_ACESSORS
        #region public
        public void TriggerDialogue()
        {
            if (IsValid())
                DiaSys.StartDialogue(this);
        }


        public bool IsValid()
        {
            if (_conditions == null)
                return true;

            foreach (CondPusher pusher in _conditions)
            {
                if (!pusher.CheckConditions())
                {
                    return false;
                }
            }
            return true;
        }


        public void StartEvent()
            => _onStartEvent?.Invoke();
        public void EndEvent()
            => _onEndEvent?.Invoke();

        public void TriggerCommands()
        {
            if (_commands == null)
                return;

            foreach (CommPusher pusher in _commands)
            {
                pusher?.PushData();
            }
        }
        #endregion

    }

}
