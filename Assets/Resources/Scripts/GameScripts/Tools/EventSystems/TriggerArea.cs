using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public int id;

    private void OnTriggerStay(Collider other)
    {
        if (other.name != "Main Camera")
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log(other.gameObject.name);
                if (gameObject.name == "Piviot")
                {
                    GameEvents.current.DoorwayTriggerEnter(id);
                }
                else if (gameObject.tag == "NPC")
                {
                    GameEvents.current.TalkToNPC(id);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
