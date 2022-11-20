/* =========================================
 * INV_SO_HUB
 * 
 * Make sure to have this GO in a scene which will NEVER be deleted!
 * Add ALL ITEMS you need into _allItems as InvItemSO.
 * That way Items can be fetched whenever necessary.
 */


using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Buntility.Inventory.Util
{
    public class InvSOHub : MonoBehaviour
    {
        public static InvSOHub Singleton;

        [SerializeField] List<InvItemSO> _itemsInv;
        [SerializeField] List<InvDataSO> _itemsData;

        private void Awake()
        {
            Singleton = this;
            Debug.Log($"{name}: currently {_itemsInv.Count} Inventory Items and {_itemsData.Count} Data Items are registered!");
        }


        public bool ItemExists(string inName)
        {
            return (findItemByName(inName) == null) ? false : true;
        }

        public InvItemInstance GetItemByName(string inName, int inAmount = 1)
        {
            InvElementSO element = findItemByName(inName);

            if (element == null) 
                return null;


            InvItemInstance newInstance = new InvItemInstance(element, inAmount);
            return newInstance;
        }





        InvElementSO findItemByName(string inName)
        {
            InvElementSO obj = findDataItem(inName);

            if (obj != null)
                return obj;

            obj = findInvItem(inName);
            return obj;
        }

        InvElementSO findDataItem(string inName)
        {
            foreach (InvDataSO data in _itemsData)
            {
                if (data.Name.ToLower() == inName.ToLower())
                    return data;
            }
            return null;
        }

        InvElementSO findInvItem(string inName)
        {
            foreach (InvItemSO data in _itemsInv)
            {
                if (data.Name.ToLower() == inName.ToLower())
                    return data;
            }
            return null;
        }

        
    }
}