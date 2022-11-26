using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionLabelingTrigger : MonoBehaviour
{

    [SerializeField] GameObject Labeling;
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Labeling.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Labeling.SetActive(false);
        }
    }
}
