using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildSingleton 
{
    
    private static GuildSingleton instance;
    public List<GameObject> Panels;
    public List<GameObject> Content;
    public List<Character> RecruitableCharacters;

    public static GuildSingleton Instance { 
        
        get 
        { 
            if(instance == null)
            {
                instance = new GuildSingleton();
            }
            
            return instance; 
        } 
    }
}
