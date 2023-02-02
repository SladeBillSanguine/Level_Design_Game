using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAndCloseAnimation : MonoBehaviour
{

  Animator animator;
  
  
  AudioSource audioSource;
  
  void Start()
  {
    animator = this.gameObject.GetComponent<Animator>();
    audioSource = this.gameObject.GetComponent<AudioSource>();
  }

  public void PlayDoorAnimation()
  {
    Debug.Log("Using Door");
    if(animator.GetBool("isOpen") == false)
    {
      animator.SetBool("isOpen", true);
      PlayDoorCloseAudio();
    }
    else
    {
      animator.SetBool("isOpen", false);
      PlayDoorOpenAudio();
    }
  }
  
  void PlayDoorOpenAudio()
  {
    audioSource.Play();
  }
  void PlayDoorCloseAudio()
  {
    audioSource.Play();
  }
  public void PlayOpen()
  {
    if (animator.GetBool("isOpen") == true)
    {
      animator.SetBool("isOpen", false);
       PlayDoorOpenAudio();
    }  
  }

  public void PlayClose()
  {
    if (animator.GetBool("isOpen") == false)
    {
        animator.SetBool("isOpen", true);
        //  PlayDoorCloseAudio();
    }
  }
}
