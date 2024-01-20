using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMove : TriggerEvent
{
    public Transform[] Destination;
    private Transform DesiredDestination;
    public float speed = 1;

    private void Awake()
    {
        type = TriggerType.movement;
    }

    private void Start()
    {
        if (moveData == null)
        {
            moveData = new TriggerMoveData();
            moveData.currentDestination = 0;
            moveData.nameID = gameObject.name;
        }
        SetUpData();
        DesiredDestination = Destination[moveData.currentDestination];
    }


    public override void TriggerMe()
    {
        if(moveData.currentDestination < Destination.Length - 1)
        {
            moveData.currentDestination++;
        }
        else
        {
            moveData.currentDestination = 0;
        }
        DesiredDestination = Destination[moveData.currentDestination];
    }

    private void Update()
    {
        if(Vector3.Distance(transform.position, DesiredDestination.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, DesiredDestination.position, Time.deltaTime * speed);
        }
    }
}
