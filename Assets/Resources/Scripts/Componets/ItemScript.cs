using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    int myId;
    static int number = 0;
    public Item myStats;
    Sprite sprite;


    // Start is called before the first frame update
    void Start()
    {
        
        string num = gameObject.name.Substring(4);
        myId = int.Parse(num);
        /*Debug.Log(myId);
        number++;
        myStats = new Item(myId);*/
        
    }

    public void LoadMyStats(Item newStats)
    {
        myStats = newStats;
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
