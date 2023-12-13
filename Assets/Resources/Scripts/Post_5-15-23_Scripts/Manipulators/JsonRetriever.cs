using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JsonRetriever
{

    public List<Character> LoadAllCharacters(string jsonLocation)
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(jsonLocation);
        CharacterData characterData = JsonUtility.FromJson<CharacterData>(jsonCharacterList.text);
        List<Character> characters = new List<Character>();
        for (int i = 0; i < characterData.characters.Count; i++)
        {
            characters.Add(new Character());

            characters[i].id = characterData.characters[i].id;
            characters[i].characterName = RandomName();
            characters[i].characterID = characterData.characters[i].id.ToString() + characters[i].characterName + 
                (System.DateTime.UtcNow.Ticks * Random.Range(2, int.MaxValue)).ToString();
            characters[i].characterClass = characterData.characters[i].characterClass;
            characters[i].modelPath = characterData.characters[i].modelPath;
            characters[i].scriptName = characterData.characters[i].scriptName;
            characters[i].icon = characterData.characters[i].icon;
            characters[i].characterImage = characterData.characters[i].characterImage;
            characters[i].scriptPosition = characterData.characters[i].scriptPosition;
            characters[i].level = characterData.characters[i].level;
            characters[i].xp = characterData.characters[i].xp;
            characters[i].maxActions = characterData.characters[i].maxActions;
            characters[i].actionsRemaining = characterData.characters[i].actionsRemaining;
            characters[i].statusEffect = characterData.characters[i].statusEffect;
            characters[i].canCapture = characterData.characters[i].canCapture;
            characters[i].maxHealth = characterData.characters[i].maxHealth;
            characters[i].currentHealth = characterData.characters[i].currentHealth;
            characters[i].maxMana = characterData.characters[i].maxMana;
            characters[i].currentMana = characterData.characters[i].currentMana;
            characters[i].attack = characterData.characters[i].attack;
            characters[i].defense = characterData.characters[i].defense;
            characters[i].speed = characterData.characters[i].speed;
            characters[i].intelligence = characterData.characters[i].intelligence;

            characters[i].weapon = characterData.characters[i].weapon;
            characters[i].offHand = characterData.characters[i].offHand;
            characters[i].helm = characterData.characters[i].helm;
            characters[i].armor = characterData.characters[i].armor;
            characters[i].boots = characterData.characters[i].boots;
            characters[i].amulet = characterData.characters[i].amulet;

            characters[i].item0 = characterData.characters[i].item0;
            characters[i].item1 = characterData.characters[i].item1;
            characters[i].item2 = characterData.characters[i].item2;

            characters[i].Abilities = characterData.characters[i].Abilities;
            characters[i].AquiredAbilites = characterData.characters[i].AquiredAbilites;
        }
        // Do something with the characters array
        return characters;
    }

    public List<Item> LoadAllItems()
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(RetrieverConstants.JsonItem);
        ItemData ItemData = JsonUtility.FromJson<ItemData>(jsonCharacterList.text);
        List<Item> items = new List<Item>();
        for (int i = 0; i < ItemData.items.Count; i++)
        {
            items.Add(new Item());

            items[i].id = ItemData.items[i].id;
            items[i].itemName = ItemData.items[i].itemName;
            items[i].itemDiscription = ItemData.items[i].itemDiscription;
            items[i].icon = ItemData.items[i].icon;
            items[i].consumable = ItemData.items[i].consumable;
            items[i].type = ItemData.items[i].type;
            items[i].buyPrice = ItemData.items[i].buyPrice;
            items[i].sellPrice = ItemData.items[i].sellPrice;
            items[i].subType = ItemData.items[i].subType;
            items[i].effect0 = ItemData.items[i].effect0;
            items[i].effect1 = ItemData.items[i].effect1;
            items[i].effect2 = ItemData.items[i].effect2;
            items[i].effect3 = ItemData.items[i].effect3;
        }
        // Do something with the characters array
        return items;
    }

    public List<Equipment> LoadAllEquipment()
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(RetrieverConstants.JsonEquipment);
        EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(jsonCharacterList.text);
        List<Equipment> equipment = new List<Equipment>();
        
        for (int i = 0; i < equipmentData.equipment.Count; i++)
        {

            equipment.Add(new Equipment());

            equipment[i].id = equipmentData.equipment[i].id;
            equipment[i].equipmentName = equipmentData.equipment[i].equipmentName;
            equipment[i].equipmentDiscription = equipmentData.equipment[i].equipmentDiscription;
            equipment[i].icon = equipmentData.equipment[i].icon;
            equipment[i].type = equipmentData.equipment[i].type;
            equipment[i].subType = equipmentData.equipment[i].subType;
            equipment[i].buyPrice = equipmentData.equipment[i].buyPrice;
            equipment[i].sellPrice = equipmentData.equipment[i].sellPrice;
            equipment[i].health = equipmentData.equipment[i].health;
            equipment[i].mana = equipmentData.equipment[i].mana;
            equipment[i].attack = equipmentData.equipment[i].attack;
            equipment[i].defence = equipmentData.equipment[i].defence;
            equipment[i].speed = equipmentData.equipment[i].speed;
            equipment[i].intelligence = equipmentData.equipment[i].intelligence;
        }
        
        

        // Do something with the characters array
        return equipment;
    }

    public List<StoreOwner> LoadAllStoreOwners()
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(RetrieverConstants.JsonStoreOwners);
        StoreOwnerData storeOwnerData = JsonUtility.FromJson<StoreOwnerData>(jsonCharacterList.text);
        List<StoreOwner> storeOwners = new List<StoreOwner>();

        for (int i = 0; i < storeOwnerData.owners.Count; i++)
        {
            storeOwners.Add(new StoreOwner());

            storeOwners[i].id = storeOwnerData.owners[i].id;
            storeOwners[i].ownerName = storeOwnerData.owners[i].ownerName;
            storeOwners[i].scriptName = storeOwnerData.owners[i].scriptName;
            storeOwners[i].scriptPosition = storeOwnerData.owners[i].scriptPosition;
            storeOwners[i].icon = storeOwnerData.owners[i].icon;
            storeOwners[i].items = storeOwnerData.owners[i].items;
            storeOwners[i].equipment = storeOwnerData.owners[i].equipment;
        }

        // Do something with the characters array
        return storeOwners;
    }

    public List<Abilities> LoadAllAbilities()
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(RetrieverConstants.JsonAbilities);
        AbilityData abilityData = JsonUtility.FromJson<AbilityData>(jsonCharacterList.text);
        List<Abilities> abilityList = new List<Abilities>();

        for (int i = 0; i < abilityData.abilities.Count; i++)
        {
            abilityList.Add(new Abilities());

            abilityList[i].id = abilityData.abilities[i].id;
            abilityList[i].abilityName = abilityData.abilities[i].abilityName;
            abilityList[i].abilityDiscription = abilityData.abilities[i].abilityDiscription;
            abilityList[i].animation = abilityData.abilities[i].animation;
            abilityList[i].animationType = abilityData.abilities[i].animationType;
            abilityList[i].icon = abilityData.abilities[i].icon;
            abilityList[i].buyPrice = abilityData.abilities[i].buyPrice;
            abilityList[i].requiredLevel = abilityData.abilities[i].requiredLevel;

            abilityList[i].shape = abilityData.abilities[i].shape;
            abilityList[i].phase = abilityData.abilities[i].phase;
            abilityList[i].minDistance = abilityData.abilities[i].minDistance;
            abilityList[i].maxDistance = abilityData.abilities[i].maxDistance;
            abilityList[i].multiTargetRange = abilityData.abilities[i].multiTargetRange;
            abilityList[i].MPCost = abilityData.abilities[i].MPCost;
            abilityList[i].isFriendly = abilityData.abilities[i].isFriendly;
            abilityList[i].isSingleTarget = abilityData.abilities[i].isSingleTarget;
            abilityList[i].isSelf = abilityData.abilities[i].isSelf;
            abilityList[i].minEffect = abilityData.abilities[i].minEffect;
            abilityList[i].maxEffect = abilityData.abilities[i].maxEffect;
            abilityList[i].effect = abilityData.abilities[i].effect;
        }

        // Do something with the characters array
        return abilityList;
    }



    public Character Load1Character(int characterNumber)
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(RetrieverConstants.JsonItem);
        CharacterData characterData = JsonUtility.FromJson<CharacterData>(jsonCharacterList.text);
        Character character = new Character();
        for (int i = 0; i < characterData.characters.Count; i++)
        {
            if(characterData.characters[i].id == characterNumber)
            {

                character.id = characterData.characters[i].id;
                character.characterName = RandomName();
                character.characterID = characterData.characters[i].id.ToString() + character.characterName +
                    (System.DateTime.UtcNow.Ticks * Random.Range(2, int.MaxValue)).ToString();
                character.characterClass = characterData.characters[i].characterClass;
                character.modelPath = characterData.characters[i].modelPath;
                character.scriptName = characterData.characters[i].scriptName;
                character.icon = characterData.characters[i].icon;
                character.characterImage = characterData.characters[i].characterImage;
                character.scriptPosition = characterData.characters[i].scriptPosition;
                character.level = characterData.characters[i].level;
                character.xp = characterData.characters[i].xp;
                character.maxActions = characterData.characters[i].maxActions;
                character.actionsRemaining = characterData.characters[i].actionsRemaining;
                character.statusEffect = characterData.characters[i].statusEffect;
                character.canCapture = characterData.characters[i].canCapture;
                character.maxHealth = characterData.characters[i].maxHealth;
                character.currentHealth = characterData.characters[i].currentHealth;
                character.maxMana = characterData.characters[i].maxMana;
                character.currentMana = characterData.characters[i].currentMana;
                character.attack = characterData.characters[i].attack;
                character.defense = characterData.characters[i].defense;
                character.speed = characterData.characters[i].speed;
                character.intelligence = characterData.characters[i].intelligence;

                character.weapon = characterData.characters[i].weapon;
                character.offHand = characterData.characters[i].offHand;
                character.helm = characterData.characters[i].helm;
                character.armor = characterData.characters[i].armor;
                character.boots = characterData.characters[i].boots;
                character.amulet = characterData.characters[i].amulet;

                character.item0 = characterData.characters[i].item0;
                character.item1 = characterData.characters[i].item1;
                character.item2 = characterData.characters[i].item2;

                character.Abilities = characterData.characters[i].Abilities;
                character.AquiredAbilites = characterData.characters[i].AquiredAbilites;
            }
        }
        // Do something with the characters array
        return character;
    }


    public Item Load1Item(int itemNumber)
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(RetrieverConstants.JsonItem);
        ItemData ItemData = JsonUtility.FromJson<ItemData>(jsonCharacterList.text);
        Item item = new Item();
        for (int i = 0; i < ItemData.items.Count; i++)
        {

            if(ItemData.items[i].id == itemNumber)
            {
                item.id = ItemData.items[i].id;
                item.itemName = ItemData.items[i].itemName;
                item.itemDiscription = ItemData.items[i].itemDiscription;
                item.icon = ItemData.items[i].icon;
                item.consumable = ItemData.items[i].consumable;
                item.type = ItemData.items[i].type;
                item.buyPrice = ItemData.items[i].buyPrice;
                item.sellPrice = ItemData.items[i].sellPrice;
                item.subType = ItemData.items[i].subType;
                item.effect0 = ItemData.items[i].effect0;
                item.effect1 = ItemData.items[i].effect1;
                item.effect2 = ItemData.items[i].effect2;
                item.effect3 = ItemData.items[i].effect3;
            }
        }
        // Do something with the characters array
        return item;
    }
    public Equipment Load1Equipment(int itemNumber)
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(RetrieverConstants.JsonEquipment);
        EquipmentData equipmentData = JsonUtility.FromJson<EquipmentData>(jsonCharacterList.text);
        Equipment equipment = new Equipment();

        for (int i = 0; i < equipmentData.equipment.Count; i++)
        {

            if(equipmentData.equipment[i].id == itemNumber)
            {

                equipment.id = equipmentData.equipment[i].id;
                equipment.equipmentName = equipmentData.equipment[i].equipmentName;
                equipment.equipmentDiscription = equipmentData.equipment[i].equipmentDiscription;
                equipment.icon = equipmentData.equipment[i].icon;
                equipment.type = equipmentData.equipment[i].type;
                equipment.subType = equipmentData.equipment[i].subType;
                equipment.buyPrice = equipmentData.equipment[i].buyPrice;
                equipment.sellPrice = equipmentData.equipment[i].sellPrice;
                equipment.health = equipmentData.equipment[i].health;
                equipment.mana = equipmentData.equipment[i].mana;
                equipment.attack = equipmentData.equipment[i].attack;
                equipment.defence = equipmentData.equipment[i].defence;
                equipment.speed = equipmentData.equipment[i].speed;
                equipment.intelligence = equipmentData.equipment[i].intelligence;
            }

        }



        // Do something with the characters array
        return equipment;
    }

    public Abilities Load1Ability(int itemNumber)
    {
        TextAsset jsonCharacterList = Resources.Load<TextAsset>(RetrieverConstants.JsonAbilities);
        AbilityData abilityData = JsonUtility.FromJson<AbilityData>(jsonCharacterList.text);
        Abilities ability = new Abilities();

        for (int i = 0; i < abilityData.abilities.Count; i++)
        {
            if (abilityData.abilities[i].id == itemNumber)
            {
                ability.id = abilityData.abilities[i].id;
                ability.abilityName = abilityData.abilities[i].abilityName;
                ability.abilityDiscription = abilityData.abilities[i].abilityDiscription;
                ability.animation = abilityData.abilities[i].animation;
                ability.animationType = abilityData.abilities[i].animationType;
                ability.icon = abilityData.abilities[i].icon;
                ability.buyPrice = abilityData.abilities[i].buyPrice;
                ability.requiredLevel = abilityData.abilities[i].requiredLevel;

                ability.shape = abilityData.abilities[i].shape;
                ability.phase = abilityData.abilities[i].phase;
                ability.minDistance = abilityData.abilities[i].minDistance;
                ability.maxDistance = abilityData.abilities[i].maxDistance;
                ability.multiTargetRange = abilityData.abilities[i].multiTargetRange;
                ability.MPCost = abilityData.abilities[i].MPCost;
                ability.isFriendly = abilityData.abilities[i].isFriendly;
                ability.isSingleTarget = abilityData.abilities[i].isSingleTarget;
                ability.isSelf = abilityData.abilities[i].isSelf;
                ability.minEffect = abilityData.abilities[i].minEffect;
                ability.maxEffect = abilityData.abilities[i].maxEffect;
                ability.effect = abilityData.abilities[i].effect;
            }
        }
        //Debug.Log(ability.id + " : " + itemNumber);
        return ability;
    }

    public string RandomName()
    {

        TextAsset nameText = Resources.Load<TextAsset>(RetrieverConstants.JsonNames);
        NameData characterData = JsonUtility.FromJson<NameData>(nameText.text);
        Name names = new Name();

        int first = Random.Range(0, characterData.names.Count-1);
        int last = Random.Range(0, characterData.names.Count-1);

        for (int i = 0; i < characterData.names.Count; i++)
        {
            if (i == first)
            {
                names.name = characterData.names[i].firstName;
            }

        }
        for (int i = 0; i < characterData.names.Count; i++)
        {
            if (i == last)
            {
                names.name = names.name + " " +characterData.names[i].lastName;
            }

        }

        return names.name;
    }
}


[System.Serializable]
public class CharacterData
{
    public List<Character> characters;
}

[System.Serializable]
public class ItemData
{
    public List<Item> items;
}


[System.Serializable]
public class EquipmentData
{
    public List<Equipment> equipment;
}

[System.Serializable]
public class NameData
{
    public List<Name> names;
}

[System.Serializable]
public class StoreOwnerData
{
    public List<StoreOwner> owners;
}

[System.Serializable]
public class AbilityData
{
    public List<Abilities> abilities;
}
