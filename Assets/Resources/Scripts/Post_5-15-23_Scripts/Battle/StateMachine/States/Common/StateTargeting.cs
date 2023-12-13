using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTargeting
{
    public GameObject FindTarget(StateMachine stateMachine)
    {

        GameObject target = null;
        foreach (GameObject playerCharacter in CombatSingleton.Instance.Combatants)
        {
            if (playerCharacter.GetComponent<CombatCharacter>().team == 0)
            {
                if (target == null)
                {
                    target = playerCharacter;
                }
                else
                {
                    float currentTargetDistance = Vector3.Distance(stateMachine.gameObject.transform.position, target.transform.position);
                    float possibleTargetDistance = Vector3.Distance(stateMachine.gameObject.transform.position, playerCharacter.transform.position);


                    if (currentTargetDistance > possibleTargetDistance)
                    {
                        target = playerCharacter;
                    }
                }
            }
        }
        return target;
    }
}
