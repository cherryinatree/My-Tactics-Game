using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{

    private static SaveData _current;
    public MainSaveFile mainData;

    public static SaveData Current
    {
        get
        {
            if(_current == null)
            {
                _current = new SaveData();
                SaveManipulator.LoadSceneChange();
            }
            return _current;
        }

        set { _current = value; }
    }

}
