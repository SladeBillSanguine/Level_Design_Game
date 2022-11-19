/* =========================================
 * UI_DEBUG_INV
 * 
 * This is simply there to debug the Inventory.
 * Though it offers examples on how to use the Cond+CommPusher.
 * 
 * Make sure to exclude this script from your build because this is basically a CheatConsole
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Buntility.Inventory.Util;

namespace Buntility.Inventory
{
    public class UIDebugInv : MonoBehaviour
    {
        [SerializeField] GameObject _debugPanel;

        [SerializeField] TextMeshProUGUI _txtPlayerInv;
        [SerializeField] TextMeshProUGUI _txtDataInv;

        [SerializeField] TMP_InputField _inputCommand;
        [SerializeField] TMP_InputField _inputCheck;
        [SerializeField] TextMeshProUGUI _txtResult;

        [SerializeField] CommPusher _debugPushObj;

        private void Start()
        {
            PlayerInventoryHub.PlayerInventory.SubscribeToInvUpdated(updatePlayerInv);
            PlayerInventoryHub.DataInventory.SubscribeToInvUpdated(updateDataInv);
            CloseMenu();
        }


        void updatePlayerInv(List<InvItemInstance> inInventory)
            => DumpPlayerInv();
        void updateDataInv(List<InvItemInstance> inInventory)
            => DumpData();



        public void DumpPlayerInv()
            => dumpData("PlayerInv", PlayerInventoryHub.PlayerInventory, _txtPlayerInv);
        public void DumpData()
            => dumpData("Data", PlayerInventoryHub.DataInventory, _txtDataInv);


        void dumpData(string inName, Inventory inInv, TextMeshProUGUI inField)
        {
            string data = $"[{inName}]\n\n" + inInv.DebugInv();
            inField.text = data;
        }



        public void PushToInventory()
        {
            if (_inputCommand.text == "") { return; }

            CommandInterpreter.InterpretInvCommand(_inputCommand.text);
        }

        public void PushToData()
        {
            if (_inputCommand.text == "") { return; }

            CommandInterpreter.InterpretDataCommand(_inputCommand.text);
        }

        public void CheckInventory()
        {
            if (_inputCheck.text == "") { return; }

            _txtResult.text = (ConditionInterpreter.InterpretInvCondition(_inputCheck.text)) ? "True" : "False";
        }

        public void CheckData()
        {
            if (_inputCheck.text == "") { return; }

            _txtResult.text = (ConditionInterpreter.InterpretDataCondition(_inputCheck.text)) ? "True" : "False";
        }


        public void DebugCommPush()
        {
            if (_debugPushObj == null)
            {
                _txtResult.text = "Cannot do PushGO because it is null!";
                return;
            }

            _debugPushObj.PushData();
            _txtResult.text = $"PushedData. Removed {_debugPushObj.name}!";
            _debugPushObj = null;
        }



        public void OpenMenu() => setMenu(true);
        public void CloseMenu() => setMenu(false);
        void setMenu(bool inState)
        {
            _debugPanel.SetActive(inState);
            if (inState)
            {
                DumpPlayerInv();
                DumpData();
            }
        }
    }
}