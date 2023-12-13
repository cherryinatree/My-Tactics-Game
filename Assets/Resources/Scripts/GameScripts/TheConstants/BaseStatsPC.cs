using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Constants
{
    public static class BaseStatsPC
    {
        public static class Fighter
        {
            public static float[] Floats =
            {
                //Speed, Attack Range
                20,6
            };

            public static int[]  Ints =
             {
                //Health, Armor Class, Magic Resistance, Dodge, Dexterity,
                //Attack Power, Magic Power, Cover, Level, Expirence, 
                //Actions, ActionsLeft
                10,10,3,2,5,10,1,0,1,0,2,2
            };
            public static string[] Strings =
             {
                //Name, Class, Status Effect, Picture
                NameCostants.MaleNames[Random.Range(0,NameCostants.MaleNames.Length-1)],
                "Fighter",
                "None",
                "noRightsToImage1"
            };
            public static string[] Actions =
             {
            "BasicAttack",
            "InstaKill"
            };
            public static string[] Spells =
             {
            
            };
            public static string[] Items =
             {
            "HealthPotion"
            };
        }
        public static class Wizard
        {
            public static float[] Floats =
            {
                //Speed, Attack Range
                15,5
            };

            public static int[] Ints =
             {
                //Health, Armor Class, Magic Resistance, Dodge, Dexterity,
                //Attack Power, Magic Power, Cover, Level, Expirence, 
                //Actions, ActionsLeft
                6,4,8,3,3,2,10,0,1,0,2,2
            };
            public static string[] Strings =
             {
                //Name, Class, Status Effect, Picture
                NameCostants.MaleNames[Random.Range(0,NameCostants.MaleNames.Length-1)],
                "Wizard",
                "None",
                "noRightsToImage1"
            };
            public static string[] Actions =
             {
            "BasicAttack"
            };
            public static string[] Spells =
             {
                "FireBall"
            };
            public static string[] Items =
             {
            "HealthPotion"
            };
        }
        public static class Rogue
        {
            public static float[] Floats =
            {
                //Speed, Attack Range
                30,6
            };

            public static int[] Ints =
             {
                //Health, Armor Class, Magic Resistance, Dodge, Dexterity,
                //Attack Power, Magic Power, Cover, Level, Expirence, 
                //Actions, ActionsLeft
                8,5,5,10,8,4,4,0,1,0,2,2
            };
            public static string[] Strings =
             {
                //Name, Class, Status Effect, Picture
                NameCostants.MaleNames[Random.Range(0,NameCostants.MaleNames.Length-1)],
                "Rogue",
                "None",
                "noRightsToImage1"
            };
            public static string[] Actions =
             {
            "BasicAttack"
            };
            public static string[] Spells =
             {
               
            };
            public static string[] Items =
             {
            "HealthPotion"
            };
        }
    }
}
