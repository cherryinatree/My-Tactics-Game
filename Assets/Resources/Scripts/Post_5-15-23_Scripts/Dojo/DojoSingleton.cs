using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DojoSingleton
{

    private static DojoSingleton _instance;
    public StoreOwner storeOwner;
    public List<GameObject> Panels;
    public List<GameObject> Content;
    public List<Character> CharacterInventories;
    
    public static DojoSingleton Instance 
    { 
        get 
        { 
            if (_instance == null)
            {
                _instance = new DojoSingleton();
            }
            return _instance; 
        } 
    }

}
