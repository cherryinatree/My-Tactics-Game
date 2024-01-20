using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDataUploader : TriggerEvent
{

    private static bool isFirst = true;

    private void Awake()
    {
        type = TriggerType.changes;
    }

    // Start is called before the first frame update
    void Start()
    {
        if(myChanges == null)
        {
            myChanges = new MyChange();
            myChanges.position = transform.position;
            myChanges.rotation = transform.rotation.eulerAngles;
            myChanges.nameID = gameObject.name;
        }

        SetUpData();
    }


    public override void TriggerMe()
    {
        throw new System.NotImplementedException();
    }

  
}
