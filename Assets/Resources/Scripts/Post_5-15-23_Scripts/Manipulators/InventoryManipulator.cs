using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class InventoryManipulator
{

    public static bool RemoveItem(Character stats, Item item)
    {
        return RemoveItem(stats, item.id);
    }

    public static bool RemoveItem(Character stats, int ItemID)
    {
        bool hasItem = false;
        if (stats.item0 == ItemID)
        {
            stats.item0 = -1; 
            hasItem = true;
        }
        else if (stats.item1 == ItemID)
        {
            stats.item1 = -1;
            hasItem = true;
        }
        else if (stats.item2 == ItemID)
        {
            stats.item2 = -1;
            hasItem = true;
        }
        return hasItem;
    }
    public static bool RemoveItem(int ItemID)
    {
        bool hasItem = false;
        JsonRetriever jsonRetriever = new JsonRetriever();
        Item item = jsonRetriever.Load1Item(ItemID);
        if (SaveData.Current.mainData.itemStorage.Contains(item))
        {
            SaveData.Current.mainData.itemStorage.Remove(item);
            hasItem = true;
        }
        return hasItem;
    }


    public static void RemoveItem(Item item)
    {
        SaveData.Current.mainData.itemStorage.Remove(item);
    }
    public static void RemoveEquipment(Equipment equip)
    {
        SaveData.Current.mainData.equipmentStorage.Remove(equip);
    }

    public static void RemoveEquipment(Character stats, Equipment equip)
    { 
        RemoveEquipment(stats, equip.id);
    }

    public static void RemoveEquipment(Character stats, int EquipmentID)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        Equipment SelectedEquipment = jsonRetriever.Load1Equipment(EquipmentID);

        switch (SelectedEquipment.type)
        {
            case "Weapon":
                if (stats.weapon == SelectedEquipment.id)
                {
                    stats.weapon = -1;
                }
                break;
            case "OffHand":
                if (stats.offHand == SelectedEquipment.id)
                {
                    stats.offHand = -1;
                }
                break;
            case "Helm":
                if (stats.helm == SelectedEquipment.id)
                {
                    stats.helm = -1;
                }
                break;
            case "Armor":
                if (stats.armor == SelectedEquipment.id)
                {
                    stats.armor = -1;
                }
                break;
            case "Boots":
                if (stats.boots == SelectedEquipment.id)
                {
                    stats.boots = -1;
                }
                break;
            case "Amulet":
                if (stats.amulet == SelectedEquipment.id)
                {
                    stats.amulet = -1;
                }
                break;
        }
    }

    public static bool AddItem(Character stats, int ItemID)
    {
        if (stats.item0 == -1)
        {
            stats.item0 = ItemID;
            return true;
        }
        else if (stats.item1 == -1)
        {
            stats.item1 = ItemID;
            return true;
        }
        else if (stats.item2 == -1)
        {
            stats.item2 = ItemID;
            return true;
        }

        return false;
    }
    public static bool AddItem(Character stats, Item item)
    {
        if (stats.item0 == -1)
        {
            stats.item0 = item.id;
            return true;
        }
        else if (stats.item1 == -1)
        {
            stats.item1 = item.id;
            return true;
        }
        else if (stats.item2 == -1)
        {
            stats.item2 = item.id;
            return true;
        }

        return false;
    }

    public static void AddItem(Item item)
    {
        SaveData.Current.mainData.itemStorage.Add(item);
    }
    public static void AddEquipment(Equipment equip)
    {
        SaveData.Current.mainData.equipmentStorage.Add(equip);
    }


    public static bool AddEquipment(Character stats, int EquipmentID)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        Equipment SelectedEquipment = jsonRetriever.Load1Equipment(EquipmentID);

        switch (SelectedEquipment.type)
        {
            case "Weapon":
                if (stats.weapon == -1)
                {
                    stats.weapon = SelectedEquipment.id;
                    return true;
                }
                else if(stats.weapon != SelectedEquipment.id)
                {
                    SaveData.Current.mainData.equipmentStorage.Add(jsonRetriever.Load1Equipment(stats.weapon));
                    stats.weapon = SelectedEquipment.id;
                    return true;
                }
                else
                {
                    return false;
                }
            case "OffHand":
                if (stats.offHand == -1)
                {
                    stats.offHand = SelectedEquipment.id;
                    return true;
                }
                else if (stats.offHand != SelectedEquipment.id)
                {
                    SaveData.Current.mainData.equipmentStorage.Add(jsonRetriever.Load1Equipment(stats.offHand));
                    stats.offHand = SelectedEquipment.id;
                    return true;
                }
                else
                {
                    return false;
                }
            case "Helm":
                if (stats.helm == -1)
                {
                    stats.helm = SelectedEquipment.id;
                    return true;
                }
                else if (stats.helm != SelectedEquipment.id)
                {
                    SaveData.Current.mainData.equipmentStorage.Add(jsonRetriever.Load1Equipment(stats.helm));
                    stats.helm = SelectedEquipment.id;
                    return true;
                }
                else
                {
                    return false;
                }
            case "Armor":
                if (stats.armor == -1)
                {
                    stats.armor = SelectedEquipment.id;
                    return true;
                }
                else if (stats.armor != SelectedEquipment.id)
                {
                    SaveData.Current.mainData.equipmentStorage.Add(jsonRetriever.Load1Equipment(stats.armor));
                    stats.armor = SelectedEquipment.id;
                    return true;
                }
                else
                {
                    return false;
                }
            case "Boots":
                if (stats.boots == -1)
                {
                    stats.boots = SelectedEquipment.id;
                    return true;
                }
                else if (stats.boots != SelectedEquipment.id)
                {
                    SaveData.Current.mainData.equipmentStorage.Add(jsonRetriever.Load1Equipment(stats.boots));
                    stats.boots = SelectedEquipment.id;
                    return true;
                }
                else
                {
                    return false;
                }
            case "Amulet":
                if (stats.amulet == -1)
                {
                    stats.amulet = SelectedEquipment.id;
                    return true;
                }
                else if (stats.amulet != SelectedEquipment.id)
                {
                    SaveData.Current.mainData.equipmentStorage.Add(jsonRetriever.Load1Equipment(stats.amulet));
                    stats.amulet = SelectedEquipment.id;
                    return true;
                }
                else
                {
                    return false;
                }
        }
        return false;
    }
    public static bool AddEquipment(Character stats, Equipment equip)
    {

        bool itemAdded = AddEquipment(stats, equip.id);
        return itemAdded;

    }
}
