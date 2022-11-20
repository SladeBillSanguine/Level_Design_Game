using Buntility.Inventory.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buntility.Inventory
{
    public class CondPusher : MonoBehaviour
    {
        [SerializeField] EInvTypes _invType;

        [SerializeField] string[] _conditions;

        public bool CheckConditions()
        {
            switch (_invType)
            {
                case EInvTypes.PlayerInventory:
                    //Debug.Log($"Checking PlayerInv: {ConditionInterpreter.InterpretInvConditions(_conditions)}");
                    return ConditionInterpreter.InterpretInvConditions(_conditions);
                case EInvTypes.DataInventory:
                    //Debug.Log($"Checking DataInv: {ConditionInterpreter.InterpretDataConditions(_conditions)}");
                    return ConditionInterpreter.InterpretDataConditions(_conditions);
                default:
                    Debug.Log($"Error: inventory {_invType} does not exist!");
                    return false;
            }
        }
    }
}