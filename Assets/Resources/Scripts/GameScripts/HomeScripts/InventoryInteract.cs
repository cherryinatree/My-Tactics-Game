using GamingTools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryInteract : MonoBehaviour
{
    public GameObject floatingImagePanel;
    GameObject image;

    string origionalInventory; 


    // Start is called before the first frame update
    void Start()
    {
        floatingImagePanel.SetActive(false);
        image = null;
    }

    // Update is called once per frame
    void Update()
    {
        GetImage();
        MoveImage();
    }

    private void GetImage()
    {
        if (Input.GetMouseButtonDown(0)&& image == null)
        {
            if (MouseSpecialActions.MouseListenerPanelTag("Panel") == "ItemImage")
            {
                if (MouseSpecialActions.MouseListenerPanelGameObject("Panel").name != "Default")
                {
                    image = MouseSpecialActions.MouseListenerPanelGameObject("Panel");
                    origionalInventory = image.transform.parent.tag;
                    floatingImagePanel.SetActive(true);
                    floatingImagePanel.GetComponent<Image>().sprite = image.GetComponent<Image>().sprite;
                    if (origionalInventory != "CharacterEquipment")
                    {
                        image.GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                    }
                    else
                    {
                        string catagory = image.transform.parent.transform.Find("Text").GetComponent<Text>().text;
                        if (catagory == "Weapon")
                        {
                            image.GetComponent<Image>().sprite =
                            ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[0]);
                        }else if (catagory == "Off-Hand")
                        {
                            image.GetComponent<Image>().sprite =
                            ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[1]);
                        }
                        else if (catagory == "Helm")
                        {
                            image.GetComponent<Image>().sprite =
                            ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[3]);
                        }
                        else if (catagory == "Armor")
                        {
                            image.GetComponent<Image>().sprite =
                            ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[2]);
                        }
                        else if (catagory == "Boots")
                        {
                            image.GetComponent<Image>().sprite =
                            ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[4]);
                        }
                        else if (catagory == "Amulet")
                        {
                            image.GetComponent<Image>().sprite =
                            ResourseLoader.GetSprite("Images/Icons/64 flat icons/png/128px/" + Constants.ImageList.equipment[5]);
                        }
                    }
                    image.tag = image.transform.parent.tag;
                }
            }
        }
        else if (Input.GetMouseButtonDown(0) && image != null)
        {
            if (MouseSpecialActions.MouseListenerPanelTag("ActiveInventory") == "ActiveInventory")
            {
                if (origionalInventory != "ActiveInventory")
                {
                    HomeSingleton.Instance.save.game.items.Add(floatingImagePanel.GetComponent<Image>().sprite.name);
                    if(origionalInventory == "CharacterInventory")
                    {
                        HomeSingleton.Instance.FocusOnCard.items.Remove(floatingImagePanel.GetComponent<Image>().sprite.name);
                    }
                    else if (origionalInventory == "BuffInventory")
                    {
                        HomeSingleton.Instance.save.game.equipedBuffs.Remove(floatingImagePanel.GetComponent<Image>().sprite.name);
                    }
                    else if (origionalInventory == "CharacterEquipment")
                    {
                        HomeSingleton.Instance.FocusOnCard.equipment.Remove(floatingImagePanel.GetComponent<Image>().sprite.name);
                    }
                }
                MouseSpecialActions.MouseListenerPanelGameObject("ActiveInventory").GetComponent<Image>().sprite = floatingImagePanel.GetComponent<Image>().sprite;
                MouseSpecialActions.MouseListenerPanelGameObject("ActiveInventory").tag = "ItemImage";
             
                floatingImagePanel.GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                floatingImagePanel.SetActive(false);
                image = null;
            }else if (MouseSpecialActions.MouseListenerPanelTag("BuffInventory") == "BuffInventory")
            {
                if (origionalInventory != "BuffInventory")
                {
                    HomeSingleton.Instance.save.game.equipedBuffs.Add(floatingImagePanel.GetComponent<Image>().sprite.name);
                    if (origionalInventory == "ActiveInventory")
                    {
                        HomeSingleton.Instance.save.game.items.Remove(floatingImagePanel.GetComponent<Image>().sprite.name);
                    }
                }

                MouseSpecialActions.MouseListenerPanelGameObject("BuffInventory").GetComponent<Image>().sprite = floatingImagePanel.GetComponent<Image>().sprite;
                MouseSpecialActions.MouseListenerPanelGameObject("BuffInventory").tag = "ItemImage";
                floatingImagePanel.GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                floatingImagePanel.SetActive(false);
                image = null;
            }
            else if (MouseSpecialActions.MouseListenerPanelTag("CharacterInventory") == "CharacterInventory")
            {
                if (origionalInventory != "CharacterInventory")
                {
                    HomeSingleton.Instance.FocusOnCard.items.Add(floatingImagePanel.GetComponent<Image>().sprite.name);
                    if (origionalInventory == "ActiveInventory")
                    {
                        HomeSingleton.Instance.save.game.items.Remove(floatingImagePanel.GetComponent<Image>().sprite.name);
                    }
                }
                MouseSpecialActions.MouseListenerPanelGameObject("CharacterInventory").GetComponent<Image>().sprite = floatingImagePanel.GetComponent<Image>().sprite;
                MouseSpecialActions.MouseListenerPanelGameObject("CharacterInventory").tag = "ItemImage";
                floatingImagePanel.GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                floatingImagePanel.SetActive(false);
                image = null;
            }
            else if (MouseSpecialActions.MouseListenerPanelTag("CharacterEquipment") == "CharacterEquipment")
            {

                string theItem = floatingImagePanel.GetComponent<Image>().sprite.name;
                string catagory = null;

                if (ResourseLoader.GetSprite("Images/Items/Weapons/" + theItem)) catagory = "Weapon";
                if (ResourseLoader.GetSprite("Images/Items/Armor/" + theItem))
                {
                    for (int i = 0; i < Constants.EquipmentList.Armor.GetLength(0); i++)
                    {
                        if (Constants.EquipmentList.Armor[i, 0] == theItem)
                        {
                            if (Constants.EquipmentList.Armor[i, 1] == "OffHand")
                            {
                                catagory = "Off-Hand";
                            }
                            else if (Constants.EquipmentList.Armor[i, 1] == "Helm")
                            {
                                catagory = "Helm";
                            }
                            else if (Constants.EquipmentList.Armor[i, 1] == "Armor")
                            {
                                catagory = "Armor";
                            }
                            else if (Constants.EquipmentList.Armor[i, 1] == "Boots")
                            {
                                catagory = "Boots";
                            }
                        }
                    }
                }
                if (ResourseLoader.GetSprite("Images/Items/Misc/" + theItem))
                {
                    for (int i = 0; i < Constants.EquipmentList.Misc.GetLength(0); i++)
                    {
                        if (Constants.EquipmentList.Misc[i, 0] == theItem)
                        {
                            if (Constants.EquipmentList.Misc[i, 1] == "OffHand")
                            {
                                catagory = "Off-Hand";
                            }
                            else if (Constants.EquipmentList.Misc[i, 1] == "Amulet")
                            {
                                catagory = "Amulet";
                            }
                        }
                    }
                }

                GameObject EquipmentPanel = MouseSpecialActions.MouseListenerPanelGameObject("CharacterEquipment");
                if (EquipmentPanel.transform.parent.transform.Find("Text").GetComponent<Text>().text == catagory)
                {
                    if (origionalInventory != "CharacterEquipment")
                    {
                        HomeSingleton.Instance.FocusOnCard.equipment.Add(theItem);
                        if (origionalInventory == "ActiveInventory")
                        {
                            HomeSingleton.Instance.save.game.items.Remove(theItem);
                        }
                    }
                    MouseSpecialActions.MouseListenerPanelGameObject("CharacterEquipment").GetComponent<Image>().sprite = floatingImagePanel.GetComponent<Image>().sprite;
                    MouseSpecialActions.MouseListenerPanelGameObject("CharacterEquipment").tag = "ItemImage";
                    floatingImagePanel.GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                    floatingImagePanel.SetActive(false);
                    image = null;
                }
                else
                {
                    image.GetComponent<Image>().sprite = floatingImagePanel.GetComponent<Image>().sprite;
                    floatingImagePanel.GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                    floatingImagePanel.SetActive(false);
                    image.tag = "ItemImage";
                    image = null;
                }
            }
            else
            {
                image.GetComponent<Image>().sprite = floatingImagePanel.GetComponent<Image>().sprite;
                floatingImagePanel.GetComponent<Image>().sprite = ResourseLoader.GetSprite("Images/Actions/Default");
                floatingImagePanel.SetActive(false);
                image.tag = "ItemImage";
                image = null;
            }
        }
    }

    private void MoveImage()
    {
        if (image != null)
        {
            floatingImagePanel.transform.position = Input.mousePosition;
        }
    }
}
