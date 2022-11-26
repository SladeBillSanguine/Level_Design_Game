using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] float yPosition;
    [SerializeField] Transform player;
    [SerializeField] CharacterController playerController;

    public void NewPosition()
    {
        playerController.enabled = false; 
        player.transform.position += new Vector3(0, yPosition, 0);
        playerController.enabled = true;
    }
}