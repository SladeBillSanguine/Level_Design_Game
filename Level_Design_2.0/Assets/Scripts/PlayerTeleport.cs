using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeleport : MonoBehaviour
{
    //Teleport
    [SerializeField] float xPosition;
    [SerializeField] float yPosition;
    [SerializeField] float zPosition;
    [SerializeField] Transform player;
    [SerializeField] CharacterController playerController;
    //Audio
    [SerializeField] AudioSource teleportAudio;

    public void Teleport()
    {
        NewPosition();
    }

    void NewPosition()
    {
        playerController.enabled = false;
        player.transform.position += new Vector3(xPosition, yPosition, zPosition);
        playerController.enabled = true;
    }
}
