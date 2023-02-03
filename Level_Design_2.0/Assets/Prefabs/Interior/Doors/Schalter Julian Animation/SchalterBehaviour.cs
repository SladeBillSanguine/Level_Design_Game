using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchalterBehaviour : MonoBehaviour
{
    Animator animator;
    [SerializeField] AudioClip schalterAudio;
    
    AudioSource audioSource;
    
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void PlaySchalterAnimation()
    {
        if(animator.GetBool("isOn") == false)
        {
        animator.SetBool("isOn", true);
        PlaySchalterOff();
        }
        else
        {
        animator.SetBool("isOn", false);
        PlaySchalterOnAudio();
        }
    }
    
    void PlaySchalterOnAudio()
    {
        audioSource.PlayOneShot(schalterAudio);
    }
    void PlaySchalterOff()
    {
        audioSource.PlayOneShot(schalterAudio);
    }
}
