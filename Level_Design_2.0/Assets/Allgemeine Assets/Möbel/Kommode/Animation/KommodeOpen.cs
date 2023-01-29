using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KommodeOpen : MonoBehaviour
{

    Animator animator;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    public void PlayObenAnimation()
    {

        if (animator.GetBool("isOpen") == false)
        {
            animator.SetBool("isOpen", true);
        }
        else
        {
            animator.SetBool("isOpen", false);
        }
    }

    public void PlayMitteAnimation()
    {

        if (animator.GetBool("isOpenMiddle") == false)
        {
            animator.SetBool("isOpenMiddle", true);
        }
        else
        {
            animator.SetBool("isOpenMiddle", false);
        }
    }

    public void PlayUntenAnimation()
    {

        if (animator.GetBool("isOpenUnten") == false)
        {
            animator.SetBool("isOpenUnten", true);
        }
        else
        {
            animator.SetBool("isOpenUnten", false);
        }
    }
}
