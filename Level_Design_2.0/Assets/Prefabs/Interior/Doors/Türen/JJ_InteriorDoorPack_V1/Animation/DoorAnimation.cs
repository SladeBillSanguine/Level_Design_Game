using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{

  Animator animator;
  [SerializeField] AudioClip doorOpen;
  [SerializeField] AudioClip doorClose;
  
  AudioSource audioSource;
  
  void Start()
  {
    animator = this.gameObject.GetComponent<Animator>();
    audioSource = this.gameObject.GetComponent<AudioSource>();
  }

  public void PlayDoorAnimation()
  {
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
    audioSource.PlayOneShot(doorOpen);
  }
  void PlayDoorCloseAudio()
  {
    audioSource.PlayOneShot(doorClose);
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
        PlayDoorCloseAudio();
    }
  }
}
