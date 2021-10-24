using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool doorOpen = false;
    public GameObject door;
    
    
  
    void Update()
    {
        if(doorOpen)
        {
            door.SetActive(!doorOpen);
        }
    }
}
