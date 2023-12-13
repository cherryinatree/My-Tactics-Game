using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreSingleton
{
    private static StoreSingleton instance;
    public GameObject[] Panels;
    public GameObject[] Content;
    public StoreOwner storeOwner;
    public List<Item> itemList;
    public List<Equipment> equipmentList;
    public List<Character> stats;

    public static StoreSingleton Instance 
    { 
    
        
        
        get { 
            
            if(instance == null)
            {
                instance = new StoreSingleton();
            }
            
            return instance; 
        
        } 
    
    }
}
