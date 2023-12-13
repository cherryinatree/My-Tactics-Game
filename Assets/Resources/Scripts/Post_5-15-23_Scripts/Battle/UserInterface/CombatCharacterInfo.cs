using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CombatCharacterInfo : MonoBehaviour
{
    Character character;

    private void OnEnable()
    {
        FillThisCard();
    }

    private void Update()
    {
        if (CombatSingleton.Instance.InfoCharacter != null)
        {
            if (character != CombatSingleton.Instance.InfoCharacter.GetComponent<CombatCharacter>().myStats)
            {
                character = CombatSingleton.Instance.InfoCharacter.GetComponent<CombatCharacter>().myStats;
                FillThisCard();
            }
        }
    }

    public void UpdateCard()
    {
        FillThisCard();
    }

    private void FillThisCard()
    {
        if (CombatSingleton.Instance.InfoCharacter != null) 
        {
            character = CombatSingleton.Instance.InfoCharacter.GetComponent<CombatCharacter>().myStats;
            transform.Find("Health").GetComponent<Text>().text = "HP: "+character.currentHealth.ToString();
            transform.Find("Name").GetComponent<Text>().text = character.characterName;
            transform.Find("Class").GetComponent<Text>().text = character.characterClass;
            transform.Find("Magic").GetComponent<Text>().text = "MP: "+ character.currentMana.ToString();
            transform.Find("Level").GetComponent<Text>().text = "Level: " + character.level.ToString();
            if(character.statusEffect != -1)
            {

                //transform.FindChild("Status").GetComponent<Text>().text =
            }
            else
            {
                transform.Find("Status").GetComponent<Text>().text = "";
            }
            transform.Find("Actions").GetComponent<Text>().text = "Act: "+character.actionsRemaining.ToString();
            transform.Find("Picture").GetComponent<Image>().sprite = Resources.Load<Sprite>(character.icon);

            if(CombatSingleton.Instance.InfoCharacter.GetComponent<CombatCharacter>().team == 0)
            {
                gameObject.GetComponent<Image>().color = new Color(0, 0, 0.2f);
            }
            else
            {
                gameObject.GetComponent<Image>().color = new Color(0.2f, 0, 0);
            }
        }
    }
}
