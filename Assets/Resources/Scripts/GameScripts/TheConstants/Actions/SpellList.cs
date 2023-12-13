using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public static class SpellList
    {
        // [0] Spell name,[1] Target Type,[2] RangeType,
        // [3] Range,[4] min damage,[5] max damage,[6] ActionType,
        // [7] Spell Level,[8] Spell Animation, [9] Particle Effect

        public static string[,] spells = new string[2, 10]
        {
        {"Heal", "Friend", "Close", "5", "1", "4", "Healing", "1", "Attack1", "None"},
        {"FireBall", "Enemy", "Medium", "20", "2", "5", "Damage", "1", "Attack1", "FireBall" }
        };
    }
}
