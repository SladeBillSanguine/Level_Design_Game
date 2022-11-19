using Buntility.Inventory.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buntility.Inventory
{
    public class CommPusher : MonoBehaviour
    {
        [SerializeField] EInvTypes _targetInv;

        [SerializeField] string[] _pushCommands;

        public void PushData()
        {
            switch (_targetInv)
            {
                case EInvTypes.PlayerInventory:
                    CommandInterpreter.InterpretInvCommands(_pushCommands);
                    return;
                case EInvTypes.DataInventory:
                    CommandInterpreter.InterpretDataCommands(_pushCommands);
                    return;
            }
        }
    }
}
