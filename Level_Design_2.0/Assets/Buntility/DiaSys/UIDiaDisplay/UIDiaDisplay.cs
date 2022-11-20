/* =========================================
 * UI_DIA_DISPLAY
 * 
 * This GO is responsible for displaying the content of DiaElement.cs
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Buntility.Input;

namespace Buntility.DialogueSystem
{
    public class UIDiaDisplay : MonoBehaviour
    {
        DiaElement _curDiaElement;

        // DIALOGUE_PANEL
        [SerializeField] GameObject _goDiaPanel;
        [SerializeField] TextMeshProUGUI _txtHeader;

        // TEXT_PANEL
        [SerializeField] GameObject _goBoxText;
        [SerializeField] TextMeshProUGUI _txtDialogue;

        // OPTIONS_PANEL
        [SerializeField] GameObject _goBoxOptions;
        [SerializeField] Button[] _buttons;
        TextMeshProUGUI[] _buttonsTxt;


        // -----------------------------------------
        // CONSTRUCTION
        #region constr
        private void Awake()
        {
            cacheButtonTxt();
            hideDiaSys();
        }

        private void OnEnable()
            => DiaSys.SubscribeToUIDiaDisplay(this);

        private void OnDisable()
            => DiaSys.UnsubscribeUIDiaDisplay();
        #endregion


        // -----------------------------------------
        // PUBLIC_ACCESSORS
        #region publicAccess
        public void DisplayDiaElement(DiaElement inElement)
        {
            //Debug.Log($"Opening: {inElement.gameObject.name}");
            

            _goDiaPanel.SetActive(true);

            _curDiaElement = inElement;
            _txtHeader.text = inElement.Header;

            switch (inElement.DiaType)
            {
                case EDiaElementType.Text:
                    setupDialogue();
                    return;
                case EDiaElementType.Option:
                    setupOption();
                    return;
            }
        }
        #endregion


        // -----------------------------------------
        // SETUP_DISPLAY
        #region setup
        void setupDialogue()
        {
            _goBoxText.SetActive(true);
            _txtDialogue.text = _curDiaElement.DialogueText;
            subscribeInteract();
        }

        void setupOption()
        {
            _goBoxOptions.SetActive(true);
            int activeButtonCounter = 0;

            // Push Text to Buttons
            for (int i = 0; i < _curDiaElement.NextDia.Length; i++)
            {
                if (_curDiaElement.NextDia[i].IsValid() || _curDiaElement.NextDia[i].DispIfInvalid)
                {
                    // SetupButton
                    _buttons[activeButtonCounter].gameObject.SetActive(true);
                    _buttonsTxt[activeButtonCounter].text = _curDiaElement.NextDia[i].OptionText;

                    _buttons[activeButtonCounter].enabled =
                                    (_curDiaElement.NextDia[i].IsValid()) ? true : false;

                    activeButtonCounter++;
                }
            }

            // Deactive all Buttons which are not needed!
            for (int i = activeButtonCounter; i < _buttons.Length; i++)
            {
                _buttons[i].gameObject.SetActive(false);
            }
        }
        #endregion


        // -----------------------------------------
        // GET_BUTTON_FEEDBACK
        #region feedback
        void spaceWasPressed()
            => OptionWasPressed(-1);

        public void OptionWasPressed(int inOption)
        {
            if (_curDiaElement == null)
                return;

            if (inOption == -1)
                unsubscribeInteract();

            _curDiaElement = null;
            hideDiaSys();
            DiaSys.GetDiaFeedback(inOption);
        }
        #endregion


        // -----------------------------------------
        // UTILITY
        #region util
        void hideDiaSys()
        {
            _goBoxText.SetActive(false);
            _goDiaPanel.SetActive(false);
            _goDiaPanel.SetActive(false);
            hideAllButtons();
        }

        void hideAllButtons()
        {
            foreach (Button button in _buttons)
            {
                button.gameObject.SetActive(false);
            }
        }

        void cacheButtonTxt()
        {
            _buttonsTxt = new TextMeshProUGUI[_buttons.Length];
            for (int i = 0; i < _buttons.Length; i++)
            {
                _buttonsTxt[i] = _buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            }
        }
        #endregion


        // -----------------------------------------
        // SUBSCRIPTION
        #region subs
        void subscribeInteract()
            => InputHub.SubscribeToInteract(spaceWasPressed);
        void unsubscribeInteract()
            => InputHub.UnSubscribeInteract(spaceWasPressed);
        #endregion
    }
}
