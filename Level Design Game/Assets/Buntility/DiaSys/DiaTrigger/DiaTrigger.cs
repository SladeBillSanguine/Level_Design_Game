/* =========================================
 * DIA_TRIGGER
 * 
 * User this to Trigger the DialogueSystem.
 * If you need an automatic triggering, use AutoTrigger.cs
 * 
 * [1] _dialogueStarter is supposed to contain a GO with DiaElement.cs
 * [2] _interactGraphics will only be shown if _autoTriggerinRange is set to true
 * [3] Set _autoTriggerinRange to true if you want the Dialogue to start in Range
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buntility.Input;
using Buntility.Inventory;

namespace Buntility.DialogueSystem
{
    public class DiaTrigger : MonoBehaviour
    {
        [SerializeField] DiaElement _dialogueStarter;

        [SerializeField] GameObject _interactDispText;
        [SerializeField] GameObject _interactGraphics;

        [SerializeField] bool _autoTriggerInRange = false;
        [SerializeField] bool _hideIfInvalid = false;


        // ----------------------------------
        // CONSTRUCTION
        #region constr
        private void OnEnable()
        {
            _interactDispText.SetActive(false);

            if (_hideIfInvalid)
            {
                setVisibility();
                subscribeToInventoryChanged();
            }
                
        }
        private void OnDisable()
        {
            unsubscribeInteract();

            if (_hideIfInvalid)
                unsubscribeInventoryChanged();
        }
        #endregion


        // ----------------------------------
        // ON_TRIGGER
        #region trigger
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == TagHub.PLAYER)
            {
                if (!_dialogueStarter.IsValid())
                    return;

                if (_autoTriggerInRange)
                {
                    doInteract();
                    return;
                }

                _interactDispText?.SetActive(true);
                subscribeInteract();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.tag == TagHub.PLAYER)
            {
                if (!_dialogueStarter.IsValid())
                    return;

                if (_autoTriggerInRange)
                    return;

                _interactDispText?.SetActive(false);
                unsubscribeInteract();
            }
        }
        #endregion

        // ----------------------------------
        // INTERACTION
        void doInteract()
            => _dialogueStarter.TriggerDialogue();


        // ----------------------------------
        // VISIBILITY
        #region visibility
        void inventoryChanged(List<InvItemInstance> inInv)
            => setVisibility();
        void setVisibility()
            => _interactGraphics.SetActive(_dialogueStarter.IsValid());
        #endregion


        // ----------------------------------
        // SUBSCRIPTION
        #region sub
        void subscribeInteract()
            => InputHub.SubscribeToInteract(doInteract);
        void unsubscribeInteract()
            => InputHub.UnSubscribeInteract(doInteract);

        void subscribeToInventoryChanged()
        {
            PlayerInventoryHub.PlayerInventory.SubscribeToInvUpdated(inventoryChanged);
            PlayerInventoryHub.DataInventory.SubscribeToInvUpdated(inventoryChanged);
        }
        void unsubscribeInventoryChanged()
        {
            PlayerInventoryHub.PlayerInventory.UnsubscribeInvUpdated(inventoryChanged);
            PlayerInventoryHub.DataInventory.UnsubscribeInvUpdated(inventoryChanged);
        }
        #endregion
    }
}