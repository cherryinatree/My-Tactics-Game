using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{

    public static GameEvents current;

    // Start is called before the first frame update
    void Awake()
    {
        current = this;
    }

    public event Action<int> onDoorwayTriggerEnter;

    public void DoorwayTriggerEnter(int id)
    {
        if (onDoorwayTriggerEnter != null)
        {
            onDoorwayTriggerEnter(id);
        }
    }

    public event Action<int> onTalkToNPC;

    public void TalkToNPC(int id)
    {
        if (onTalkToNPC != null)
        {
            onTalkToNPC(id);
        }
    }

}
