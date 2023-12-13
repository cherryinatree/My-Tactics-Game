using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Constants
{
    public static class EquipmentList
    {

        // [0] Item name,[1] Equipment Slot,[2] AC,
        // [3] buff 1,[4] buff 2,[5] buff 3,[6] buff 4
        public static string[,] Armor = new string[1, 7]
        {
        {"ChestPlate", "Armor", "3", "0", "0", "0", "0"}
        }; 
        public static string[,] Misc = new string[1, 7]
        {
        {"Amulet", "Amulet", "3", "0", "0", "0", "0"}
        };
    }
}
