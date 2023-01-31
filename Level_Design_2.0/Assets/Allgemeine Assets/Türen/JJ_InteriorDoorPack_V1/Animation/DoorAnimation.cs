using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

    Animator animator; 
    
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void PlayDoorAnimation()
    {
        if(animator.GetBool("isOpen") == false)
        {
            animator.SetBool("isOpen", true);
        }
        else
        {
            animator.SetBool("isOpen", false);
        }
    }

    public void PlayOpen()
    {
        if (animator.GetBool("isOpen") == true)
        {
            animator.SetBool("isOpen", false);
        }  
    }

    public void PlayClose()
    {
        if (animator.GetBool("isOpen") == false)
        {
            animator.SetBool("isOpen", true);
        }
    }
}
