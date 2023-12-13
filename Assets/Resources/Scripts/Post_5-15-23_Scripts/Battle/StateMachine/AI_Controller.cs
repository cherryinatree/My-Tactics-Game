using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI_Controller : MonoBehaviour
{
    public List<GameObject> Combatants;
    int currentEnemy;

    // Start is called before the first frame update
    void Start()
    {
        SetUpEnemyCycle();
        FillEnemyCombatants();
    }


    // Update is called once per frame
    void Update()
    {
        EnemyActivator();
    }

    private void SetUpEnemyCycle()
    {
        currentEnemy = 0;
    }

    private void EnemyActivator()
    {
        if(CombatSingleton.Instance.battleSystem.State == BATTLESTATE.ENEMYTURN)
        {
            if(CombatSingleton.Instance.FocusCharacter == null)
            {
                FillEnemyCombatants();
                CombatSingleton.Instance.FocusCharacter = Combatants[0];
                currentEnemy++;
            }
            else
            {

                if (CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().team == 0)
                {
                    CombatSingleton.Instance.FocusCharacter = Combatants[0];
                    currentEnemy++;
                }
            }

            if (CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats.actionsRemaining == 0)
            {
                if(currentEnemy < Combatants.Count)
                {

                    Debug.Log(currentEnemy);
                    CombatSingleton.Instance.FocusCharacter = Combatants[currentEnemy];
                    currentEnemy++;
                }
                else
                {
                    Debug.Log(CombatSingleton.Instance.actionData.AiAction);
                    if (CombatSingleton.Instance.actionData.AiAction==false)
                    {
                        Debug.Log(CombatSingleton.Instance.actionData.AiAction);
                        currentEnemy = 0;
                        CombatSingleton.Instance.FocusCharacter = null;
                        CombatSingleton.Instance.battleSystem.TurnChange();
                    }
                }

            }
        }
    }


    private void FillEnemyCombatants()
    {
        Combatants = new List<GameObject>();
        foreach (GameObject enemy in CombatSingleton.Instance.Combatants)
        {
            if (enemy.GetComponent<CombatCharacter>().team != 0)
            {
                Combatants.Add(enemy);
            }
        }
    }
}
