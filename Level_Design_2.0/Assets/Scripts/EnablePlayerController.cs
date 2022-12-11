using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablePlayerController : MonoBehaviour
{
    [SerializeField]  CharacterController characterController;
    bool isEnabled = false;

    public void Changenable()
    {
        if (isEnabled)
        {
            characterController.enabled = false;
            isEnabled = false;
        }
        else
        {
            characterController.enabled = true;
            isEnabled = true;
        }
        Debug.Log(isEnabled);
    }
}
