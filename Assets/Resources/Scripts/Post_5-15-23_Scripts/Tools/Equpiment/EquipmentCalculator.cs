using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EquipmentCalculator
{

    public enum STATTYPE {MaxHealth = 0, MaxMana = 1, Attack = 2,
    Defense = 3, Speed = 4, Intelligence = 5};

    private static JsonRetriever jsonRetriever = new JsonRetriever();


    public static int FullStat(Character SelectedCharacter, STATTYPE statType)
    {
        int fullStat = 0;
        int[,] itemStats = new int[6,6];

        for (int x = 0; x < 6; x++)
        {

            for (int i = 0; i < 6; i++)
            {
                itemStats[x,i] = 0;
            }
        }

        itemStats = EquipmentEffects(SelectedCharacter, itemStats);

        if (statType == STATTYPE.MaxHealth)
        {
            for (int i = 0; i < 6; i++)
            {
                fullStat += itemStats[i,0];
            }
            fullStat += SelectedCharacter.maxHealth;
        }
        else if (statType == STATTYPE.MaxMana)
        {

            for (int i = 0; i < 6; i++)
            {
                fullStat += itemStats[i,1];
            }
            fullStat += SelectedCharacter.maxMana;
        }
        else if (statType == STATTYPE.Attack)
        {

            for (int i = 0; i < 6; i++)
            {
                fullStat += itemStats[i,2];
            }
            fullStat += SelectedCharacter.attack;
        }
        else if (statType == STATTYPE.Defense)
        {

            for (int i = 0; i < 6; i++)
            {
                fullStat += itemStats[i,3];
            }
            fullStat += SelectedCharacter.defense;
        }
        else if (statType == STATTYPE.Speed)
        {

            for (int i = 0; i < 6; i++)
            {
                fullStat += itemStats[i,4];
            }
            fullStat += SelectedCharacter.speed;
        }
        else if (statType == STATTYPE.Intelligence)
        {
            for (int i = 0; i < 6; i++)
            {
                fullStat += itemStats[i,5];
            }
            fullStat += SelectedCharacter.intelligence;
        }

        return fullStat;
    }

    private static int[,] EquipmentEffects(Character SelectedCharacter, int[,] itemStats)
    {

        if (SelectedCharacter.weapon != -1)
        {

            Equipment item = jsonRetriever.Load1Equipment(SelectedCharacter.weapon);
            itemStats[0, 0] = item.health;
            itemStats[0, 1] = item.mana;
            itemStats[0, 2] = item.attack;
            itemStats[0, 3] = item.defence;
            itemStats[0, 4] = item.speed;
            itemStats[0, 5] = item.intelligence;
        }
        if (SelectedCharacter.offHand != -1)
        {

            Equipment item = jsonRetriever.Load1Equipment(SelectedCharacter.offHand);
            itemStats[1, 0] = item.health;
            itemStats[1, 1] = item.mana;
            itemStats[1, 2] = item.attack;
            itemStats[1, 3] = item.defence;
            itemStats[1, 4] = item.speed;
            itemStats[1, 5] = item.intelligence;
        }
        if (SelectedCharacter.helm != -1)
        {

            Equipment item = jsonRetriever.Load1Equipment(SelectedCharacter.helm);
            itemStats[2, 0] = item.health;
            itemStats[2, 1] = item.mana;
            itemStats[2, 2] = item.attack;
            itemStats[2, 3] = item.defence;
            itemStats[2, 4] = item.speed;
            itemStats[2, 5] = item.intelligence;
        }
        if (SelectedCharacter.armor != -1)
        {

            Equipment item = jsonRetriever.Load1Equipment(SelectedCharacter.armor);
            itemStats[3, 0] = item.health;
            itemStats[3, 1] = item.mana;
            itemStats[3, 2] = item.attack;
            itemStats[3, 3] = item.defence;
            itemStats[3, 4] = item.speed;
            itemStats[3, 5] = item.intelligence;
        }
        if (SelectedCharacter.boots != -1)
        {

            Equipment item = jsonRetriever.Load1Equipment(SelectedCharacter.boots);
            itemStats[4, 0] = item.health;
            itemStats[4, 1] = item.mana;
            itemStats[4, 2] = item.attack;
            itemStats[4, 3] = item.defence;
            itemStats[4, 4] = item.speed;
            itemStats[4, 5] = item.intelligence;
        }
        if (SelectedCharacter.amulet != -1)
        {

            Equipment item = jsonRetriever.Load1Equipment(SelectedCharacter.amulet);
            itemStats[5, 1] = item.mana;
            itemStats[5, 2] = item.attack;
            itemStats[5, 3] = item.defence;
            itemStats[5, 4] = item.speed;
            itemStats[5, 5] = item.intelligence;
        }

        return itemStats;
    }
}
