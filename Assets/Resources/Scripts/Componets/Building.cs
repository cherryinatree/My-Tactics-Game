using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building
{
    private string _name;
    private float _positionX;
    private float _positionY;
    private float _positionZ;
    //private Sprite _sprite;
    private string _buildingPathLoacotion;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public float PositionX
    {
        get { return _positionX; }
        set { _positionX = value; }
    }
    public float _PositionY
    {
        get { return _positionY; }
        set { _positionY = value; }
    }

    public float PositionZ
    {
        get { return _positionZ; }
        set { _positionZ = value; }
    }
    /*
    public Sprite MySprite
    {
        get { return _sprite; }
        set { _sprite = value; }
    }
    */
    public string BuildingPathLoacotion
    {
        get { return _buildingPathLoacotion; }
        set { _buildingPathLoacotion = value; }
    }


}
