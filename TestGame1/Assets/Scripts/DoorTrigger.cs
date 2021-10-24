using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public DoorController targetDoor;
    public bool playerStay = false;
    public bool DoorOpen = false;

    private void Update()
    {
        if(playerStay && Input.GetKeyDown(KeyCode.E))
        {
            DoorOpen = !DoorOpen;
        }
        targetDoor.doorOpen = DoorOpen;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playerStay = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerStay = false;
        }
    }

}
