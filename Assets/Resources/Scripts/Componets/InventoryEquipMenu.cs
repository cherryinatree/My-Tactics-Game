using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryEquipMenu : MonoBehaviour
{
    Button left;
    Button right;
    Text myName;


    // Start is called before the first frame update
    void Start()
    {
        left = gameObject.transform.Find("Left").GetComponent<Button>();
        right = gameObject.transform.Find("Right").GetComponent<Button>();
        myName = gameObject.transform.Find("Left").GetComponent<Text>();
        //Debug.Log(TownMain._data.char1);
        //myName.text = TownMain._data.char1.MyName;
        //Debug.Log(TownMain._data.char1.MyName);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
