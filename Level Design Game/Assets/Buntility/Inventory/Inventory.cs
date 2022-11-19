/* =========================================
 * INVENTORY
 * 
 * Contains InvItem.cs as a List, which contain all Items.
 * Subscibre to _invUpdated in case you want for example your Inventory UI to know if the Inventory was manipulated.
 * 
 * In theory you can build as many inventories as you wish. Define this in InvHub.cs
 * 
 * IMPORTANT:
 * If you add an Item by STRING, then it will search the InvSOHub if said Item exists, then generate it.
 * If possible, only add Items via enum EItemIDs.cs
 */

using System.Collections.Generic;
using System;
using Buntility.Inventory.Util;
using UnityEngine;

namespace Buntility.Inventory
{
    public class Inventory
    {
        List<InvItemInstance> _invItems;
        public List<InvItemInstance> InvItems { get { return _invItems; } }

        Action<List<InvItemInstance>> _invUpdated;

        // ---------------------------------------
        // CONSTRUCTION
        public Inventory()
        {
            _invItems = new List<InvItemInstance>();
        }


        // ---------------------------------------
        // PUBLIC_ACCESSORS
        public void AddItem(string inName, int inAmount)
            => AddItem(InvSOHub.Singleton.GetItemByName(inName, inAmount));
        public void AddItem(InvItemInstance inItem)
        {
            if (inItem == null)
            {
                Debug.Log("Error! Item does not exist!");
                return;
            }

            int index = FindItemInInventoryByName(inItem.Name);

            if (index != -1)
            {
                // Item in Inventory. Subtract!
                addQuantity(index, inItem.Amount);
            }
            else
            {
                // Item not in Inventory. Add!
                _invItems.Add(inItem);
            }
            _invUpdated?.Invoke(_invItems);
        }


        public void SubItem(string inName, int inAmount = 1)
        {
            int index = FindItemInInventoryByName(inName);

            if (index == -1) return;
            subQuantity(index, inAmount);

            _invUpdated?.Invoke(_invItems);
        }


        public void SetValue(string inName, int inSetValue = 1)
        {
            int index = FindItemInInventoryByName(inName);

            if (index == -1) return;

            if (inSetValue == 0)
            {
                _invItems.RemoveAt(index);
            } else
            {
                _invItems[index].Amount = inSetValue;
            }

            _invUpdated?.Invoke(_invItems);
        }

        public int GetItemAmount(string inName)
        {
            int index = FindItemInInventoryByName(inName);
            return (index >= 0) ? _invItems[index].Amount : 0;
        }


        public int FindItemInInventoryByName(string inName)
        {
            for (int i = 0; i < _invItems.Count; i++)
            {
                if (_invItems[i].Name.ToLower() == inName.ToLower()) return i;
            }
            return -1;
        }


        // ---------------------------------------
        // ADD_ITEMS
        void addQuantity(int inIndex, int inAmount)
        {
            _invItems[inIndex].Amount += inAmount;
            checkIfZero(inIndex);
        }
        void subQuantity(int inIndex, int inAmount)
        {
            _invItems[inIndex].Amount -= inAmount;
            checkIfZero(inIndex);
        }

        void checkIfZero(int inIndex)
        {
            if (_invItems[inIndex].Amount == 0)
                _invItems.RemoveAt(inIndex);
        }




        // ---------------------------------------
        // DEBUG
        public string DebugInv()
        {
            string returner = "";
            foreach (InvItemInstance item in _invItems)
            {
                returner += $"{item.DebugItem()}\n";
            }
            return returner;
        }


        // ---------------------------------------
        // SUBSCRIPTION
        public void SubscribeToInvUpdated(Action<List<InvItemInstance>> inAction)
            => _invUpdated += inAction;
        public void UnsubscribeInvUpdated(Action<List<InvItemInstance>> inAction)
            => _invUpdated -= inAction;
    }
}
