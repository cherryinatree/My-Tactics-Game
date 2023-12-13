using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetGroupController : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PositionUpdate();
    }

    private void PositionUpdate()
    {
        if(CombatSingleton.Instance.battleSystem.State == BATTLESTATE.PLAYERTURN || CombatSingleton.Instance.battleSystem.State == BATTLESTATE.ENEMYTURN)
        {

            transform.position = CombatSingleton.Instance.CursorCube.transform.position;
            
        }
    }
}
