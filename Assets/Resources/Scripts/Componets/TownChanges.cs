using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TownChanges
{
    int _townHeight;
    int _townWidth;
    List<Building> _buildings;
    int _gold;
    int _buildingMaterials;
    int _day;
    int _numberOfCitizens;
    int _housedCitizens;

    public TownChanges()
    {
        TownHeight = 10;
        TownWidth = 10;
        Gold = 500;
        BuildingMaterials = 500;
        Day = 1;
        NumberOfCitizens = 5;
    }
    public TownChanges(MainSaveData data)
    {
        TownHeight = data.Town.TownHeight;
        TownWidth = data.Town.TownWidth;
        Gold = data.Town.Gold;
        BuildingMaterials = data.Town.BuildingMaterials;
        Day = data.Town.Day;
        NumberOfCitizens = data.Town.NumberOfCitizens;
    }

    public int TownHeight
    {
        get { return _townHeight; }
        set { _townHeight = value; }
    }
    public int TownWidth
    {
        get { return _townWidth; }
        set { _townWidth = value; }
    }
    public List<Building> Buildings
    {
        get { return _buildings; }
        set { _buildings = value; }
    }
    public int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }
    public int BuildingMaterials
    {
        get { return _buildingMaterials; }
        set { _buildingMaterials = value; }
    }
    public int Day
    {
        get { return _day; }
        set { _day = value; }
    }
    public int NumberOfCitizens
    {
        get { return _numberOfCitizens; }
        set { _numberOfCitizens = value; }
    }
    public int HousedCitizens
    {
        get { return _housedCitizens; }
        set { _housedCitizens = value; }
    }
}
