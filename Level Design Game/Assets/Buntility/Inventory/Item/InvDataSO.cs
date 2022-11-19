/* =========================================
 * INV_DATA_SO
 * 
 * This is simply a placeholder for DataSOs. Use those ONLY to track progress which is NOT SUPPOSED TO BE SEEN by the player!
 */

using UnityEngine;

namespace Buntility.Inventory.Util
{
    [CreateAssetMenu(fileName = "DataSO", menuName = "Inventory/DataSO", order = 1)]
    [System.Serializable]
    public class InvDataSO : InvElementSO
    {
        public override void SetupUI(GameObject inObj)
        {
            Debug.Log("Displaying InvDataSO is only possible for debugging!");
        }

        public override void UseItem(GameObject inObj)
        {
            Debug.Log("Using InvDataSO is NOT possible!");
        }

        public override string DebugItem()
        {
            return $"{Name}";
        }
    }
}