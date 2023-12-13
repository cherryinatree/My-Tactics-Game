using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class ListGeneratorUI
{
    
    public static void PopulateActionList(GameObject content, Character character, bool isAbility)
    {
        GameObject CardPrefab = Resources.Load<GameObject>("Prefabs/PrefabsUI/ActionButton");
        FillCards.ClearViewport(content);

        FillActionList(content, CardPrefab, GenerateCardInfo(character, isAbility));
    }

    private static void FillActionList(GameObject content, GameObject CardPrefab, List<string[]> cardInfo)
    {

        foreach (string[] item in cardInfo)
        {
            GameObject card = FillCards.CombatActionCard(CardPrefab, content, item);
            card.GetComponent<Button>().onClick.AddListener(() => InvokeDisplay(item));
        }
    }

    private static void InvokeDisplay(string[] cardInfo)
    {
        int id = -1;
        if (cardInfo[4] == "ability")
        {
            if (int.TryParse(cardInfo[3], out id))
            {
                GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewAbility(id);
                GameObject.Find("GameMaster").GetComponent<InteractMenus>().ResetPanels();
            }
        }else if(cardInfo[4] == "item")
        {

            GameObject.Find("ActionsMaster").GetComponent<Actions>().PreviewItem(id);
            GameObject.Find("GameMaster").GetComponent<InteractMenus>().ResetPanels();
        }
    }

    private static List<string[]> GenerateCardInfo(Character character, bool isAbility)
    {
        if (isAbility)
        {

            return GenerateAbilityList(character);
        }
        else
        {

            return GenerateItemList(character);
        }
    }

    private static List<string[]> GenerateAbilityList(Character character)
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        List<string[]> result = new List<string[]>();
        for (int i = 0; i < character.Abilities.Length; i++)
        {
            if (character.AquiredAbilites[i])
            {
                Abilities ability = jsonRetriever.Load1Ability(character.Abilities[i]);
                string[] abilityInfo = new string[5];
                abilityInfo[0] = ability.icon;
                abilityInfo[1] = ability.abilityName;
                abilityInfo[2] = ability.MPCost.ToString();
                abilityInfo[3] = ability.id.ToString();
                abilityInfo[4] = "ability";

                result.Add(abilityInfo);
            }
        }

        return result;
    }
    private static List<string[]> GenerateItemList(Character character)
    {

        JsonRetriever jsonRetriever = new JsonRetriever();
        List<string[]> result = new List<string[]>();
        Item[] items = new Item[3];
        items[0] = jsonRetriever.Load1Item(character.item0);
        items[1] = jsonRetriever.Load1Item(character.item1);
        items[2] = jsonRetriever.Load1Item(character.item2);
        for (int i = 0; i < items.Length; i++)
        {
            string[] itemInfo = new string[5];
            itemInfo[0] = items[i].icon;
            itemInfo[1] = items[i].itemName;
            itemInfo[2] = "0";
            itemInfo[3] = items[i].id.ToString();
            itemInfo[4] = "item";

            result.Add(itemInfo);
        }

        return result;
    }

}
