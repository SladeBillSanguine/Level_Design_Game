/* =========================================
 * INV_ITEM_SO
 * 
 * Use this for Items which are supposed to be seen by the player.
 * 
 * Important: if you want to give an Item a "USE"-Method, then make sure to make a NEW class!
 */

using UnityEngine;

namespace Buntility.Inventory.Util
{
    [CreateAssetMenu(fileName = "ItemSO", menuName = "Inventory/ItemSO", order = 2)]
    [System.Serializable]
    public class InvItemSO : InvElementSO
    {
        [SerializeField] string _desc;

        [SerializeField] Sprite _sprite;

        public override void SetupUI(GameObject inObj)
        {
            Debug.Log("SetupItem");
        }

        public override void UseItem(GameObject inObj)
        {
            Debug.Log("UseItem");
        }

        public override string DebugItem()
        {
            return $"{Name}:[{_desc}]";
        }
    }
}