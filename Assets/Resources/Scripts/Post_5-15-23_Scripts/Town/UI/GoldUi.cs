using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUi : MonoBehaviour
{

    private Text gold;
    // Start is called before the first frame update
    void Start()
    {
        gold = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {

        GoldUpdate();
    }

    private void GoldUpdate()
    {
        gold.text = "Gold:\n" + SaveData.Current.mainData.playerData.money.ToString();
    }
}
