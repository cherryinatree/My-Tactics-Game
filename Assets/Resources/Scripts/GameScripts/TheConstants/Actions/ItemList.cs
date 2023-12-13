using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Constants
{
    public static class ItemList
    {
        // [0] Item name,[1] Target Type,[2] RangeType,
        // [3] Range,[4] min damage,[5] max damage,[6] ActionType

        public static string[,] items = new string[2, 7]
        {
        {"Heal", "Friend", "Close", "5", "1", "4", "Healing"},
        {"FireBall", "Foe", "Medium", "20", "2", "5", "Damage" }
        };
    }
}