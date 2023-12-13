using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerChoices 
{
    string _characterBeingControlled;
    int _gold = 300;
    int _buildingMaterials =300;


    
    public string CharacterBeingControlled 
    {
        get { return _characterBeingControlled; }
        set { _characterBeingControlled = value; }
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

}
