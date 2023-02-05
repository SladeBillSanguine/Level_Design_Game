using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Buntility.Input;
public class EnableControlls : MonoBehaviour
{
    public void disableControlls()
    {
        InputHub.DisableExternControls();
        
    }
    public void enableControlls()
    {
        InputHub.EnableExternControls();

    }
}
