using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behind : MonoBehaviour
{
   [SerializeField] GameObject _player;

    private void Start()
    {
        transform.position = _player.transform.position;
    }

    private void Update()
    {
        transform.position = _player.transform.position;
    }
}
