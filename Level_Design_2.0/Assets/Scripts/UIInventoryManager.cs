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

        [SerializeField] public Transform inventoryTransform;
        [SerializeField] public GameObject inventoryContainer;

        float y = 375f;

        // Start is called before the first frame update
        void Start()
        {
            PlayerInventoryHub.PlayerInventory.SubscribeToInvUpdated(updatePlayerInv);
         
        }

        void updatePlayerInv(List<InvItemInstance> inInventory)
        {
            clear();
            
            
            
            foreach (InvItemInstance item in inInventory)
            {
                
                if(item.Amount > 0)
                {
                GameObject obj = Instantiate(inventoryContainer, inventoryTransform.transform);
                item.SetupUI(obj);
                obj.transform.position = new Vector3 (obj.transform.position.x, y, obj.transform.position.z);
                y = y-35f;
                
                }
               
              
            }
             
        }

        void clear()
        {
            
                foreach (Transform child in inventoryTransform)
                {
                    GameObject.Destroy(child.gameObject);
                }
             y = 375f;
        }

       
    }
}