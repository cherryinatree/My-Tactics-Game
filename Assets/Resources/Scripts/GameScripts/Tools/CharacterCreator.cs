using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GamingTools;
using Constants;

public static class CharacterCreator
{
    public enum CHARACTERCLASS {
    Fighter= 0, Wizard = 1, Rogue = 2
    }
    public enum ENEMYLIST
    {
        Golem = 0
    }


    public static Stats CreateCharacter(CHARACTERCLASS theClass, int id)
    {
        
        if (theClass == CHARACTERCLASS.Fighter)
        {
            Stats theStat = new Stats(BaseStatsPC.Fighter.Floats, BaseStatsPC.Fighter.Ints, BaseStatsPC.Fighter.Strings,
                BaseStatsPC.Fighter.Actions, BaseStatsPC.Fighter.Spells, BaseStatsPC.Fighter.Items,id);
            return theStat;
        }
        if (theClass == CHARACTERCLASS.Wizard)
        {
            Stats theStat = new Stats(BaseStatsPC.Wizard.Floats, BaseStatsPC.Wizard.Ints, BaseStatsPC.Wizard.Strings,
                BaseStatsPC.Wizard.Actions, BaseStatsPC.Wizard.Spells, BaseStatsPC.Wizard.Items, id);
            return theStat;
        }
        if (theClass == CHARACTERCLASS.Rogue)
        {
            Stats theStat = new Stats(BaseStatsPC.Rogue.Floats, BaseStatsPC.Rogue.Ints, BaseStatsPC.Rogue.Strings,
                BaseStatsPC.Rogue.Actions, BaseStatsPC.Rogue.Spells, BaseStatsPC.Rogue.Items, id);
            return theStat;
        }
        Stats theStats = new Stats();
        return theStats;
    }

    public static Stats EnemyCharacter(ENEMYLIST enemy, int id)
    {
        if (enemy == ENEMYLIST.Golem)
        {
            Stats theStat = new Stats(BaseStatsEnemies.Golem.Floats, BaseStatsEnemies.Golem.Ints, BaseStatsEnemies.Golem.Strings,
                BaseStatsEnemies.Golem.Actions, BaseStatsEnemies.Golem.Spells, BaseStatsEnemies.Golem.Items, id);
            return theStat;
        }
        Stats theStats = new Stats();
        return theStats;
    }
}
