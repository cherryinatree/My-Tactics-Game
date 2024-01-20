using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerReward : TriggerEvent
{
    public int gold = -1;
    public int item = -1;
    public int armor = -1;
    public int weapon = -1;

    private GameObject AcquiredPanel;
    private AcquiredDisplay acquiredDisplay;

    private void Awake()
    {
        AcquiredPanel = GameObject.Find("AcquiredPanel");
        acquiredDisplay = AcquiredPanel.GetComponent<AcquiredDisplay>();
    }



    public override void TriggerMe()
    {
        AcquiredPanel.SetActive(true);
        acquiredDisplay.TextReset();

        if (gold != -1) RewardGold(gold);
        if (item != -1) RewardItem(item);
        if (armor != -1) RewardArmor(armor);
        if (weapon != -1) RewardWeapon(weapon);

        Invoke("TurnOffAcquiredPanel", 5);
    }

    private void TurnOffAcquiredPanel()
    {
        AcquiredPanel.SetActive(false);
    }

    private void RewardGold(int reward)
    {
        SaveData.Current.mainData.playerData.money += reward;
        acquiredDisplay.GainedGold(reward.ToString());
    }
    private void RewardItem(int reward)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        Item item = jsonRetriever.Load1Item(reward);
        SaveData.Current.mainData.itemInventory.Add(item);
        acquiredDisplay.GainedItem(item.itemName);
    }
    private void RewardArmor(int reward)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        Equipment item = jsonRetriever.Load1Equipment(reward);
        SaveData.Current.mainData.equipmentStorage.Add(item);
        acquiredDisplay.GainedArmor(item.equipmentName);
    }
    private void RewardWeapon(int reward)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        Equipment item = jsonRetriever.Load1Equipment(reward);
        SaveData.Current.mainData.equipmentStorage.Add(item);
        acquiredDisplay.GainedWeapon(item.equipmentName);
    }
}
