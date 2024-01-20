using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateTargeting
{

    public GameObject BestTargetForAbility(List<GameObject> enemies, CombatCharacter character)
    {
        if(enemies.Count == 1)
        {
            return enemies[0];
        }

        GameObject bestTarget = enemies[0];
        /*
        for (int i = 1; i < enemies.Count; i++)
        {
            if(enemies[i].GetComponent<CombatCharacter>().myStats.currentHealth < bestTarget.GetComponent<CombatCharacter>().myStats.currentHealth)
            {
                bestTarget = enemies[i];
            }
        }

        for (int i = 1; i < enemies.Count; i++)
        { 
            if(bestTarget != enemies[i])
            {
                if (enemies[i].GetComponent<CombatCharacter>().myStats.currentHealth == bestTarget.GetComponent<CombatCharacter>().myStats.currentHealth)
                {
                    bestTarget = WhichIsCloser(bestTarget, enemies[i], character);
                }
            }
        }*/
        for (int i = 0; i < enemies.Count; i++)
        {
            if (bestTarget != enemies[i])
            {

                bestTarget = WhichIsCloser(bestTarget, enemies[i], character);
            }
        }


        return bestTarget;
    }

    private GameObject WhichIsCloser(GameObject current, GameObject challenger, CombatCharacter character)
    {
        float currentDistance = Vector3.Distance(current.transform.position, character.gameObject.transform.position);
        float challengerDistance = Vector3.Distance(challenger.transform.position, character.gameObject.transform.position);

        if (currentDistance < challengerDistance)
        {
            return current;
        }
        else
        {
            return challenger;
        }
    }











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
