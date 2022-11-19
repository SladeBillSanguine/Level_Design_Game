using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteSoundSource : MonoBehaviour
{


    [SerializeField] float _deleteTime = -1;

    // Start is called before the first frame update
    void Start()
    {
        if (_deleteTime == -1)
            _deleteTime = GetComponent<AudioSource>().clip.length;
        Destroy(gameObject, _deleteTime); 
    }



}
