using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public static class BaseStatsEnemies
    {
        public static class Golem
        {
            public static float[] Floats =
            {
                //Speed, Attack Range
                20,6
            };

            public static int[] Ints =
             {
                10,//Health,
                7,//Armor Class
                1,//Magic Resistance
                2,//Dodge
                5,//Dexterity
                10,//Attack Power
                1,//Magic Power
                0,//Cover
                1,//Level
                0,//Expirence
                2,//Actions
                2//ActionsLeft
            };
            public static string[] Strings =
             {
                //Name, Class, Status Effect, Picture
                "Golem",
                "Fighter",
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
