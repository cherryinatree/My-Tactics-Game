using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoldUpdate : MonoBehaviour
{
    private int Gold;

    // Start is called before the first frame update
    void Start()
    {
        Gold = SaveData.Current.mainData.playerData.money;
        gameObject.GetComponent<TextMeshProUGUI>().text = "Gold:\n" + Gold.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(Gold != SaveData.Current.mainData.playerData.money)
        {
            Gold = SaveData.Current.mainData.playerData.money;
            gameObject.GetComponent<TextMeshProUGUI>().text = "Gold:\n" +Gold.ToString();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            SaveData.Current.mainData.playerData.money += 100;
        }
    }
}
