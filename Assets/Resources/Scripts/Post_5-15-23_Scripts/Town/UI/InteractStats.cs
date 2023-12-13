using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractStats : MonoBehaviour
{
    public InteractScroll scroll;

    public TextMeshProUGUI stat0;
    public TextMeshProUGUI stat1;
    public TextMeshProUGUI stat2;
    public TextMeshProUGUI stat3;
    public TextMeshProUGUI stat4;
    public TextMeshProUGUI stat5;

    private int referenceStat0;
    private int referenceStat1;
    private int referenceStat2;
    private int referenceStat3;
    private int referenceStat4;
    private int referenceStat5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {

        if (scroll != null)
        {
            if (scroll.SelectedCharacter != null)
            {

                stat0.text = "Health: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxHealth).ToString();
                stat1.text = "Mana: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxMana).ToString();
                stat2.text = "Attack: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Attack).ToString();
                stat3.text = "Defense: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Defense).ToString();
                stat4.text = "Speed: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Speed).ToString();
                stat5.text = "Intelligence: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Intelligence).ToString();

                referenceStat0 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxHealth);
                referenceStat1 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxMana);
                referenceStat2 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Attack);
                referenceStat3 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Defense);
                referenceStat4 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Speed);
                referenceStat5 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Intelligence);
            }
        }
    }


    private void Update()
    {
        if(referenceStat0 != EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxHealth))
        {
            stat0.text = "Health: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxHealth).ToString();
            referenceStat0 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxHealth);
        }
        else if(referenceStat1 != EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxMana))
        {
            stat1.text = "Mana: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxMana).ToString();
            referenceStat1 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.MaxMana);
        }
        else if(referenceStat2 != EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Attack))
        {
            stat2.text = "Attack: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Attack).ToString();
            referenceStat2 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Attack);
        }
        else if(referenceStat3 != EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Defense))
        {
            stat3.text = "Defense: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Defense).ToString();
            referenceStat3 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Defense);
        }
        else if(referenceStat4 != EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Speed))
        {
            stat4.text = "Speed: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Speed).ToString();
            referenceStat4 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Speed);
        }
        else if(referenceStat5 != EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Intelligence))
        {
            stat5.text = "Intelligence: " + EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Intelligence).ToString();
            referenceStat5 = EquipmentCalculator.FullStat(scroll.SelectedCharacter, EquipmentCalculator.STATTYPE.Intelligence);
        }
    }


}
