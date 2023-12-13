using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;
using Constants;

public static class BattleMechanics
{
    
   public enum AttackType
    {
        Magic = 0, MagicRange = 1, Attack = 2, Range = 3, Item = 4
    };

    private const float chanceRoll = 20;
    public static float HitChance(GameObject attacker, GameObject defender, AttackType type)
    {
        float chanceToHit = 0;
        Stats attackerStats = attacker.GetComponent<CharacterController>().myStats;
        Stats defenderStats = defender.GetComponent<CharacterController>().myStats;

        float hightDifference = attacker.transform.position.y - defender.transform.position.y;
        if(hightDifference < 0)
        {
            hightDifference = 0;
        }
        hightDifference = hightDifference / 5;

        if (type == AttackType.Attack)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower - defenderStats.dodge - defenderStats.armorClass;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        if (type == AttackType.Range)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower + hightDifference - defenderStats.dodge - defenderStats.armorClass - defenderStats.cover;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        if (type == AttackType.MagicRange)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower + hightDifference - defenderStats.dodge - defenderStats.armorClass - defenderStats.cover;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        if (type == AttackType.Magic)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower - defenderStats.dodge - defenderStats.MagicResistance;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        if (type == AttackType.Item)
        {
            chanceToHit = chanceRoll + attackerStats.attackPower - defenderStats.dodge - defenderStats.armorClass;
            chanceToHit = (chanceToHit / chanceRoll) * 100;
            return chanceToHit;
        }
        return chanceToHit;
    }

    public static void Attack(GameObject attacker, GameObject defender, AttackType type, string attackName)
    {

        attacker.GetComponent<CharacterController>().myStats.actionsLeft -= 1;
        if(attacker.GetComponent<CharacterController>().myStats.actionsLeft <= 0)
        {
            BattleCamera.SwitchDelay(3);
        }
        float chance = HitChance(attacker, defender, type);

        int isItOne = Random.Range(1, 100);
        if(isItOne <= chance)
        {
            attacker.transform.LookAt(defender.transform);
            ProceedWithAttack(attacker, defender, type, attackName);
        }
        else
        {
            FailAttack(attacker, defender, type, attackName);
        }

    }

    private static void ProceedWithAttack(GameObject attacker, GameObject defender, AttackType type, string attackName)
    {
        SoundController.MakeSound(attacker, BattleSounds.MaleHuman.Attacking[0]);
        int action = 0;
        string[,] ActionList = null;
        if (type == AttackType.Magic)
        {
            ActionList = SpellList.spells;
        }
        if (type == AttackType.Attack)
        {
            ActionList = ActionsList.Actions;
        }
        
            for (int i = 0; i < ActionList.GetLength(0); i++)
            {
                if (ActionList[i, 0] == attackName)
                {
                    action = i;
                }
            }
            int damage = Random.Range(int.Parse(ActionList[action, 4]), int.Parse(ActionList[action, 5]));
            defender.GetComponent<CharacterController>().myStats.health -= damage;
            attacker.GetComponent<Animator>().SetTrigger(ActionList[action, 8]);
            if (defender.GetComponent<CharacterController>().myStats.health <= 0)
            {
                defender.GetComponent<CharacterController>().myStats.health = 0;
            Targeting.SelectTargetExecptOne(defender);
            UserInterface.ShowCharacterInfo(Targeting.target.GetComponent<CharacterController>().myStats);
            attacker.transform.LookAt(defender.transform);
        }
        else
        {

            UserInterface.ShowCharacterInfo(defender.GetComponent<CharacterController>().myStats);
        }
            if(ActionList[action, 9] != "None")
        {
            ParticleController.PlaceAttackParticles(attacker.transform.Find("pEffect1").gameObject, ActionList[action, 9], defender);
        }

    }

    private static void FailAttack(GameObject attacker, GameObject defender, AttackType type, string attackName)
    {
        Debug.Log("Miss");
    }
}
