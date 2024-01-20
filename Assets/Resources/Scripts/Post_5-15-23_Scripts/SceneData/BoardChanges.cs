using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoardChanges 
{
   
    public List<MyChange> changes;
    public List<TriggerData> triggers;
    public List<TriggerMoveData> movers;

}
