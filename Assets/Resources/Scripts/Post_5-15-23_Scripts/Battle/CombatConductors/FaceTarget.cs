using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceTarget
{

    public void FaceDirection(GameObject origin, GameObject target)
    {
        origin.GetComponent<CombatCharacter>().FaceDirection(CalculateFacing(origin, target));
    }

    private float CalculateFacing(GameObject origin, GameObject target)
    {
        origin.transform.LookAt(target.transform.position);
        origin.transform.eulerAngles = new Vector3(0, origin.transform.eulerAngles.y, 0);

        float facing = origin.transform.eulerAngles.y;

        if(facing <= 45 && facing >= -45)
        {
            facing = 0;
        }
        else if (facing <= 135 && facing >= 45)
        {
            facing = 90;
        }
        else if (facing <= 225 && facing >= 135)
        {
            facing = 180;
        }
        else if (facing <= 315 && facing >= 225)
        {
            facing = 270;
        }
        else if (facing <= 405 && facing >= 315)
        {
            facing = 0;
        }
        else if (facing >= -135 && facing <= -45)
        {
            facing = -90;
        }
        else if (facing >= -225 && facing <= -135)
        {
            facing = -180;
        }
        else if (facing >= -315 && facing <= -225)
        {
            facing = -270;
        }
        else if (facing >= -405 && facing <= -315)
        {
            facing = 0;
        }


        return facing;
    }
}
