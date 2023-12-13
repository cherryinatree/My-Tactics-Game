using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractMenus : MonoBehaviour
{
    public MenuItemSelector[] menus;
    private string menuFocus;
    private MenuItemSelector focusMenu;


    public void OpenMenu(string openMe)
    {
        OpenMe(openMe);
    }

    public void OpenAbilityList()
    {
        OpenMe("ActionPanel2");

        ListGeneratorUI.PopulateActionList(focusMenu.transform.GetChild(0).gameObject, 
            CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats, true);
       
    }

    public void OpenItemList()
    {
        OpenMe("ActionPanel2");

        ListGeneratorUI.PopulateActionList(focusMenu.transform.GetChild(0).gameObject,
            CombatSingleton.Instance.FocusCharacter.GetComponent<CombatCharacter>().myStats, false);
        
    }


    private void OpenMe(string openMe)
    {

        CombatPanelManipulator.ActivatePanel(openMe);
        PickFocus(openMe);
        CombatSingleton.Instance.isUiOn = true;
    }

    private void PickFocus(string focusMe)
    {
        foreach(MenuItemSelector menu in menus)
        {
            if(menu.gameObject.name == focusMe)
            {
                menuFocus = focusMe;
                focusMenu = menu;
            }
        }
    }

    public void Next()
    {
        focusMenu.Next();
    }

    public void Previous()
    {
        focusMenu.Previous();
    }

    public void Select()
    {
        focusMenu.SelectButton();
    }

    public void ResetPanels()
    {
        CombatSingleton.Instance.isUiOn = false;
        CombatPanelManipulator.ResetPanels();

        menuFocus = null;
        focusMenu = null;
    }

}
