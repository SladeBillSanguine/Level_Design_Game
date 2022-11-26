using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] float yPosition;
    [SerializeField] Transform player;

    public void NewPosition()
    {
       player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yPosition, player.transform.position.z);
       Debug.Log("Spieler Teleportiert");
       Debug.Log(player.transform.position);
       
    }
}