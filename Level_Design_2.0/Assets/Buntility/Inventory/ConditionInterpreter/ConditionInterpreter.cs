/* =========================================
 * CONDITION_INTERPRETER
 * 
 * In here Conditions are interpreted to figure out if a value exists!
 * 
 * Commands look like this:
 * "[ITEM_NAME] [OPERAND] [AMOUNT]"
 * Make sure to ALWAYS have " " (Space) between those three elements!
 * Consider that you need the NAME of the Item, not the EItemID!
 * If an Item is NOT registered as ItemSO in InvSOHub.cs it cannot be added!
 * 
 * 
 * Examples:
 * [1] "myItem == 1"
 *      => does myItem equal 1?
 *      
 * [2] "myItem != 1"
 *      => does myItem unequal 1?
 *      
 * [3] "myItem <= 1"
 *      => is myItem smaller-equal to 1?
 *      
 * [4] "myItem < 1"
 *      => is myItem smaller than 1?
 * 
 * [5] "myItem >= 1"
 *      => is myItem bigger-equal to 1?
 * 
 * [6] "myItem > 1"
 *      => is myItem bigger to 1?
 *      
 *      
 * Generally it is smart to always initiate a variable before it might have relevancy for Conditions. Remember: you got to set a variable at least to 1 if you want it to be in the Inventory!
 * Though this Interpreter will check two specific case:
 * "myItem == 0"
 *      => if myItem is not in the Inventory, it will return "TRUE"
 * "myItem != X"
 *      => if X is UNEQUAL to 0, then this will return "TRUE"
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Buntility.Inventory.Util
{
    public static class ConditionInterpreter
    {
        static Inventory _curInv;
        static string _curCondition;
        static string[] _parsedCondition;

        const string EQUALS = "==";
        const string UNEQUALS = "!=";
        const string BIGGER = ">";
        const string BIGGEREQUALS = ">=";
        const string SMALLER = "<";
        const string SMALLEREQUALS = "<=";


        // --------------------------------------------
        // PUBLIC_ACCESSORS
        public static bool InterpretInvConditions(string[] inConditions)
        {
            _curInv = PlayerInventoryHub.PlayerInventory;

            foreach (string cond in inConditions)
            {
                if (!interpretCondition(cond))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool InterpretInvCondition(string inCondition)
        {
            _curInv = PlayerInventoryHub.PlayerInventory;
            return interpretCondition(inCondition);
        }

        public static bool InterpretDataConditions(string[] inConditions)
        {
            _curInv = PlayerInventoryHub.DataInventory;

            foreach (string cond in inConditions)
            {
                if (!interpretCondition(cond))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool InterpretDataCondition(string inCondition)
        {
            _curInv = PlayerInventoryHub.DataInventory;
            return interpretCondition(inCondition);
        }



        // --------------------------------------------
        // INTERPRET_CONDITIONS
        static bool interpretCondition(string inCondition)
        {
            //Debug.Log($"Interpreting: {inCondition}");

            _curCondition = inCondition.ToLower();
            _parsedCondition = inCondition.Split(' ');

            if (_parsedCondition.Length != 3)
            {
                Debug.Log($"Error: Condition Length is {_parsedCondition.Length}! {condNotValid()}");
                return false;
            }

            int valueA = getValue(_parsedCondition[0]);
            int valueB = getValue(_parsedCondition[2]);

            //Debug.Log($"{valueA} {_parsedCondition[1]} {valueB}");

            if (valueA == -1)
            {
                if ((_parsedCondition[2] == EQUALS) && (valueB == 0))
                {
                    // The Variable is not declared. Hence it counts as 0 and the condition applies!
                    return true;
                }
                else if ((_parsedCondition[2] == UNEQUALS) && (valueB != 0))
                {
                    // here we check: var != 5, whereas number cannot be 0
                    return true;
                }

                Debug.Log($"Error: {_parsedCondition[0]} does not exist! {condNotValid()}");
                return false;
            }
            if (valueB == -1)
            {
                Debug.Log($"Error: {_parsedCondition[2]} does not exist! {condNotValid()}");
                return false;
            }


            switch (_parsedCondition[1])
            {
                case EQUALS:
                    //Debug.Log($"{valueA} == {valueB}");
                    return (valueA == valueB);
                case UNEQUALS:
                    //Debug.Log($"{valueA} != {valueB}");
                    return (valueA != valueB);
                case SMALLEREQUALS:
                    //Debug.Log($"{valueA} <= {valueB}");
                    return (valueA <= valueB);
                case SMALLER:
                    //Debug.Log($"{valueA} < {valueB}");
                    return (valueA < valueB);
                case BIGGEREQUALS:
                    //Debug.Log($"{valueA} >= {valueB}");
                    return (valueA >= valueB);
                case BIGGER:
                    //Debug.Log($"{valueA} > {valueB}");
                    return (valueA > valueB);
                default:
                    Debug.Log($"Error: Operator '{_parsedCondition[1]}' not valid. {condNotValid()}");
                    return false;
            }
        }

        static int getValue(string inName)
        {
            //Debug.Log($"---------------------\nTrying: {inName}");
            if (InvSOHub.Singleton.ItemExists(inName))
            {
                // the Value is an Item - get the Amount if it exists!
                //Debug.Log($"Found {inName}: {_curInv.GetItemAmount(inName)}");
                return _curInv.GetItemAmount(inName);
            }

            int value = -1;
            if (Int32.TryParse(inName, out value))
            {
                //Debug.Log($"{inName}: parsed {value}");
                return value;
            }
            Debug.Log($"{inName}: invalid!");
            return value;
        }


        static string condNotValid()
        {
            return $"Condition '{_curCondition}' cannot be parsed. Returned: false";
        }
    }
}
