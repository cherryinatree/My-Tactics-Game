using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character
{
    public int id;
    public string inventoryID;
    public string characterID;

    public List<Image> images;
    public string icon;
    public string characterImage;
    public string characterName;
    public string characterClass;
    public string modelPath;

    public string scriptName;
    public int scriptPosition;

    public int level;
    public int xp;
    public bool canCapture;
    public int maxActions;
    public int actionsRemaining;
    public int statusEffect;
    public int maxHealth;
    public int currentHealth;
    public int maxMana;
    public int currentMana;
    public int attack;
    public int defense;
    public int speed;
    public int intelligence;


    public int weapon;
    public int offHand;
    public int helm;
    public int armor;
    public int boots;
    public int amulet;


    public int item0;
    public int item1;
    public int item2;

    public int[] Abilities;
    public bool[] AquiredAbilites;

    public string myCubeName;

    public float facing;
}
