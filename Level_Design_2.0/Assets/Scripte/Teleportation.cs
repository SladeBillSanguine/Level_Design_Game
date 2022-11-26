using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleportation : MonoBehaviour
{
    [SerializeField] GameObject yPosition;
    [SerializeField] GameObject Player;

    public void newPosition()
    {
       Player.transform.position = new Vector3(Player.transform.position.x, yPosition.transform.position.y, Player.transform.position.z);
    }
}
