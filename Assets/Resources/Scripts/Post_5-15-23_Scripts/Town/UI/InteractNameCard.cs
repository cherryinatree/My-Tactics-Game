using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractNameCard : MonoBehaviour
{
    public InteractScroll interactScroll;
    Character character;
    public TextMeshProUGUI nameText;
    public Image characterIcon;

    private void OnEnable()
    {
        UpdateNameCard();
    }

    private void UpdateNameCard()
    {
        if (interactScroll.populator != null)
        {
            if (interactScroll.populator.SelectedCharacter != null)
            {
                character = interactScroll.populator.SelectedCharacter;
                nameText.text = interactScroll.populator.SelectedCharacter.characterName;
                characterIcon.sprite = Resources.Load<Sprite>(interactScroll.populator.SelectedCharacter.icon);
            }
        }
    }

    private void Update()
    {
        if (character != interactScroll.populator.SelectedCharacter)
        {
            UpdateNameCard();
        }
    }
}
