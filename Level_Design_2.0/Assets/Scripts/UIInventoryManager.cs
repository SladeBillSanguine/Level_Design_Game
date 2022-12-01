using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Buntility.Inventory.Util;

namespace Buntility.Inventory
{
    public class UIInventoryManager : MonoBehaviour
    {
      
        public List<InvItemSO> inventory = new List<InvItemSO>();

        [SerializeField] Transform inventoryTransform;
        [SerializeField] GameObject inventoryContainer;

        // Start is called before the first frame update
        void Start()
        {
            PlayerInventoryHub.PlayerInventory.SubscribeToInvUpdated(updatePlayerInv);
        }

        void updatePlayerInv(List<InvItemInstance> inInventory)
        {
            foreach (InvItemInstance item in inInventory)
            {
                GameObject obj = Instantiate(inventoryContainer, inventoryTransform);
                item.SetupUI(obj);

                
            }
        }

       
    }
}