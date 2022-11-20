using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Buntility.Inventory
{
    public static class PlayerInventoryHub
    {
        static Inventory _playerInventory;
        public static Inventory PlayerInventory { get { return _playerInventory; } }


        static Inventory _dataInventory;
        public static Inventory DataInventory { get { return _dataInventory; } }


        static PlayerInventoryHub()
        {
            _playerInventory = new Inventory();
            _dataInventory = new Inventory();

            //pushDebugData();
        }


        /*
        static void pushDebugData()
        {
            _playerInventory.AddItem(EItemIDs.iGold, 5);
            _playerInventory.AddItem(EItemIDs.iSilver, 1);

            _playerInventory.AddItem(EItemIDs.iGold, 5);
        }*/

    }

}
