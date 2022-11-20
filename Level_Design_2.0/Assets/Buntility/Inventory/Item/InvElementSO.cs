/* =========================================
 * INV_ELEMENT_SO
 * 
 * This is an abstract Item, which is the BASE of every Item. Ensure to always have an override of SetupUI, UseItem and DebugItem in every child-class you define
 */

using UnityEngine;

[System.Serializable]
public abstract class InvElementSO : ScriptableObject
{
    [SerializeField] protected string _name;
    public string Name { get { return _name; } }


    public abstract void SetupUI(GameObject inObj);
    public abstract void UseItem(GameObject inObj);

    public abstract string DebugItem();
}
