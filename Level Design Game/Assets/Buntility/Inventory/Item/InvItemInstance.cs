/* =========================================
 * INV_ITEM_INSTANCE
 * 
 * The only purpose of the Item Instance is to hold a reference to the ItemSOs (InvDataSO / InvItemSO).
 * This allows to dynamically set the Amount in the inventory, and will not affect the SO.
 */

using UnityEngine;

namespace Buntility.Inventory
{
    public class InvItemInstance
    {
        InvElementSO _invElement;

        public string Name { get { return _invElement.Name; } }
        public int Amount { get; set; }


        // ---------------------------------
        // CONSTRUCTION
        public InvItemInstance (InvElementSO inSO, int inAmount = 1)
        {
            _invElement = inSO;
            Amount = inAmount;
        }

        // ---------------------------------
        // PUBLIC_ACCESSORS
        public void SetupUI(GameObject inObj)
        {
            _invElement.SetupUI(inObj);
        }

        public void UseItem(GameObject inObj)
        {
            _invElement.UseItem(inObj);
        }


        // ---------------------------------
        // DEBUG
        public string DebugItem()
        {
            return $"{_invElement.DebugItem()} - Amount: {Amount}";
        }
    }
}
