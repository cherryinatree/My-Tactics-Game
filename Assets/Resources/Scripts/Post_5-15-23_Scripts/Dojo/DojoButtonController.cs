using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DojoButtonController : MonoBehaviour
{
    private GameObject NameCard;
    private GameObject AbilityCard;

    public TextMeshProUGUI characterLevelText;

    private void Start()
    {
        NameCard = Resources.Load<GameObject>("Prefabs/PrefabsUI/CardStoreCharacterName");
        AbilityCard = Resources.Load<GameObject>("Prefabs/PrefabsUI/CardAbility");
        ResetPanels();
    }

    public void ButtonTrain()
    {
        ActivatePanel(0);
        PopulateNamePanel();
    }

    public void ButtonGosip()
    {

    }

    public void ButtonExit()
    {

    }

    private void ActivatePanel(int panel)
    {
        DojoSingleton.Instance.Panels[panel].SetActive(true);
    }

    private void DeactivatePanel(int panel)
    {

        DojoSingleton.Instance.Panels[panel].SetActive(false);
    }

    private void ResetPanels()
    {
        foreach (GameObject item in DojoSingleton.Instance.Panels)
        {
            item.SetActive(false);
        }
    }

    private void PopulateNamePanel()
    {
        FillCards.ClearViewport(DojoSingleton.Instance.Content[0]);

        foreach (Character stats in DojoSingleton.Instance.CharacterInventories)
        {
            GameObject card = FillCards.NameCard(NameCard, DojoSingleton.Instance.Content[0], stats);
            card.GetComponent<Button>().onClick.AddListener(delegate { PopulateAbilityCards(stats); });
        }
    }

    private void PopulateAbilityCards(Character stats)
    {
        ActivatePanel(1);
        FillCards.ClearViewport(DojoSingleton.Instance.Content[1]);

        characterLevelText.text = "Character Lvl: " + stats.level.ToString();
        List<Abilities> abilities = CharacterInfoRetriever.GetAbilities(stats);
        for (int i = 0; i < abilities.Count; i++)
        {
            Abilities ability = abilities[i];
            GameObject card = FillCards.AbilityCard(AbilityCard, DojoSingleton.Instance.Content[1], ability, stats.AquiredAbilites[i]);
            card.GetComponent<Button>().onClick.AddListener(delegate { ConfirmPurchase(stats, ability); });
        }
    }

    private void ConfirmPurchase(Character stats, Abilities ability)
    {
        // checks if the character already has the ability
        bool isOwned = CharacterInfoRetriever.DoesCharacterOwnAbility(stats, ability);

        // if not, brings up the confirm purchase window
        if (!isOwned)
        {
            ActivatePanel(2);
            DojoSingleton.Instance.Panels[2].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "Tain " + stats.characterName + " to use " + ability.abilityName + "?"; 
            DojoSingleton.Instance.Panels[2].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
            DojoSingleton.Instance.Panels[2].transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<Button>().onClick.AddListener(
                delegate { BuyAbility(stats, ability); });
        }
    }

    

    private void BuyAbility(Character stats, Abilities ability)
    {
        if (stats.level >= ability.requiredLevel)
        {
            if (SaveData.Current.mainData.playerData.money >= ability.buyPrice)
            {
                CharacterDataManipulator.CharacterAbilityPurchace(stats, ability);
                SaveData.Current.mainData.playerData.money -= ability.buyPrice;
                PopulateAbilityCards(stats);
                DeactivatePanel(2);
            }
            else
            {
                DojoSingleton.Instance.Panels[2].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                    "Not Enough Gold";
            }
        }
        else
        {

            DojoSingleton.Instance.Panels[2].transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text =
                "Character Level Too Low";
        }
    }
}
