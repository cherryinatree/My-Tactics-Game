using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AcquiredDisplay : MonoBehaviour
{

    public Text GoldText;
    public Text ItemText;
    public Text ArmorText;
    public Text WeaponText;

    public GameObject GoldImage;
    public GameObject ItemImage;
    public GameObject ArmorImage;
    public GameObject WeaponImage;


    public void TextReset()
    {
        GoldImage.SetActive(false);
        ItemImage.SetActive(false);
        ArmorImage.SetActive(false);
        WeaponImage.SetActive(false);
    }

    public void GainedGold(string gold)
    {
        GoldImage.SetActive(true);
        GoldText.text = "+ " + gold;
    }
    public void GainedItem(string item)
    {
        ItemImage.SetActive(true);
        ItemText.text = "+ " + item;
    }
    public void GainedArmor(string armor)
    {
        ArmorImage.SetActive(true);
        ArmorText.text = "+ " + armor;
    }
    public void GainedWeapon(string weapon)
    {
        WeaponImage.SetActive(true);
        WeaponText.text = "+ " + weapon;
    }
}
