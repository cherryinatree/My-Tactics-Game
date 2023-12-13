using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeath : MonoBehaviour
{
    Timer timer;
    bool isDead = false;


    // Update is called once per frame
    void Update()
    {
        if (isDead)
        {
            if (timer.ClockTick())
            {
                Destroy(gameObject);
            }
        }
    }

    public void Death(float deathDelay)
    {
        CombatSingleton.Instance.Combatants.Remove(gameObject);
        WinLossCheck();
        transform.LeanScaleX(0, deathDelay);

        gameObject.GetComponent<CombatCharacter>().myCube.GetComponent<Cube>().MyType = GROUNDTYPE.Ground;
        timer = new Timer(deathDelay);
        isDead = true;
    }

    private void WinLossCheck()
    {
        int team = CombatSingleton.Instance.Combatants[0].GetComponent<CombatCharacter>().team;
        bool multipleTeams = false;
        foreach (var character in CombatSingleton.Instance.Combatants)
        {
            if(team != character.GetComponent<CombatCharacter>().team)
            {
                multipleTeams = true;
                break;
            }
        }

        if (!multipleTeams)
        {
            if(team == 0)
            {

                GameObject.Find("GameMaster").GetComponent<WinChecker>().Win();
            }
            else
            {

                GameObject.Find("GameMaster").GetComponent<WinChecker>().Loss();
            }
        }
    }

}
