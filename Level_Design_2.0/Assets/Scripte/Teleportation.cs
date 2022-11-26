using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] float yPosition;
    [SerializeField] GameObject Player;

    public void NewPosition()
    {
       Player.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y + yPosition, Player.transform.position.z);
    }
}