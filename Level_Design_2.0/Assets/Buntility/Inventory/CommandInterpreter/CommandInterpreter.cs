/* =========================================
 * COMMAND_INTERPRETER
 * 
 * In here Commands get interpreted which manipulate the Inventory.
 * 
 * Commands look like this:
 * "[ITEM_NAME] [OPERAND] [AMOUNT]"
 * Make sure to ALWAYS have " " (Space) between those three elements!
 * Consider that you need the NAME of the Item, not the EItemID!
 * If an Item is NOT registered as ItemSO in InvSOHub.cs it cannot be added!
 * 
 * Examples:
 * [1] "myItem = 1"
 *      => sets myItem to 1. When myItem is already declared, any other value will be overwritten!
 *      (e.g: if it was on 5 before, then it will now be on 1!)
 *      
 * [2] "myItem += 1"
 *      => adds 1 to myItem. If it does not exist, then "myItem" will be added
 *      
 * [3] "myItem -= 1"
 *      => reduces 1 from myItem as long as it exists. Otherwise nothing happens
 *      Consider that if "myItem" reaches "0", then it will be deleted dout of the Inventory!
 */

using System;
using UnityEngine;

namespace Buntility.Inventory.Util
{
    public static class CommandInterpreter
    {
        static string _curCommand;
        static string[] _parsedCommand;

        static Inventory _curInv;


        // --------------------------------------------
        // PUBLIC_ACESSORS
        #region publicAccessors
        public static void InterpretInvCommands(string[] inCommands)
        {
            _curInv = PlayerInventoryHub.PlayerInventory;
            foreach (string comm in inCommands)
            {
                interpretCommand(comm);
            }
        }
        public static void InterpretInvCommand(string inCommand)
        {
            _curInv = PlayerInventoryHub.PlayerInventory;
            interpretCommand(inCommand);
        }

        public static void InterpretDataCommands(string[] inCommands)
        {
            _curInv = PlayerInventoryHub.DataInventory;
            foreach (string comm in inCommands)
            {
                interpretCommand(comm);
            }
        }

        public static void InterpretDataCommand(string inCommand)
        {
            _curInv = PlayerInventoryHub.DataInventory;
            interpretCommand(inCommand);
        }
        #endregion


        // --------------------------------------------
        // INTERPRETATION
        #region interpret
        static void interpretCommand(string inCommand)
        {
            _curCommand = inCommand;
            _parsedCommand = inCommand.Split(' ');

            if (_parsedCommand.Length != 3)
            {
                Debug.Log($"Error: command got a length of {_parsedCommand.Length}. {commandNotExecutable()}");
                return;
            }

            int index = findItem(_parsedCommand[0]);
            int value = parseStringToInt(_parsedCommand[2]);

            switch (_parsedCommand[1])
            {
                case "=":
                    setValue(index, value);
                    return;
                case "+=":
                    addValue(index, value);
                    return;
                case "-=":
                    subtractValue(index, value);
                    return;
                default:
                    Debug.Log($"Error: operator {_parsedCommand[1]} is invalid. {commandNotExecutable()}");
                    return;
            }
        }
        #endregion


        // --------------------------------------------
        // SET_VALUES
        #region setVal
        static void setValue(int inIndex, int inValue)
        {
            if (inIndex == -1)
            {
                createItem(inValue);
                return;
            }

            _curInv.SetValue(_parsedCommand[0], inValue);
        }

        static void addValue(int inIndex, int inValue)
        {
            if (inIndex == -1)
            {
                createItem(inValue);
                return;
            }

            _curInv.AddItem(_parsedCommand[0], inValue);
        }

        static void subtractValue(int inIndex, int inValue)
        {   
            if (inIndex == -1)
            {
                Debug.Log($"Error: the variable {_parsedCommand[0]} does not exist! {commandNotExecutable()}");
                return;
            }

            if (inValue == 0) return;

            _curInv.SubItem(_parsedCommand[0], inValue);
        }
        #endregion


        // --------------------------------------------
        // UTILITY
        #region util
        static int findItem(string inName)
        {
            return _curInv.FindItemInInventoryByName(inName);
        }

        static void createItem(int inValue)
        {
            InvItemInstance newItem = InvSOHub.Singleton.GetItemByName(_parsedCommand[0], inValue);
            if (newItem == null)
            {
                Debug.Log($"Error: item '{_parsedCommand[0]}' was never declared in InvSOHub! {commandNotExecutable()}");
                return;
            }

            _curInv.AddItem(newItem);
        }

        static int parseStringToInt(string inValue)
        {
            int value = 0;
            if (Int32.TryParse(inValue, out value))
            {
                return value;
            }
            return 0;
        }

        static string commandNotExecutable()
        {
            return $"Command '{_curCommand}' not executable.";
        }
        #endregion
    }
}
