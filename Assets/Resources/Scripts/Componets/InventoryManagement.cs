using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManagement : MonoBehaviour
{
    List<Image> images;
    public static Image dragMe;
    private static Vector3 origionalPosition;
    private static Transform UIorder;
    private static GameObject EquipScreen;
    private static Inventory Inventory;
    public static Sprite ImageHover;
    // Start is called before the first frame update
    void Start()
    {
        images = new List<Image>();
        ListOfImages();
        EquipScreen = GameObject.Find("Equip");
       // Inventory = new Inventory();
    }

    private void Update()
    {
        if (dragMe != null)
        {
            dragMe.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                dragMe.transform.position.z);
        }
    }

    private void ListOfImages()
    {
        images.Clear();
        for (int x = 0; x < gameObject.transform.childCount; x++)
        {
            for (int i = 0; i < gameObject.transform.GetChild(x).childCount - 1; i++)
            {
                //Debug.Log(gameObject.transform.GetChild(x).GetChild(0).GetComponent<Image>().ToString());
                if (gameObject.transform.GetChild(x).GetChild(i).GetComponent<Image>() != null)
                {
                    if (gameObject.transform.GetChild(x).GetChild(i).name != "Tab")
                    {
                        try
                        {
                            images.Add(gameObject.transform.GetChild(x).GetChild(i).GetChild(0).GetComponent<Image>());
                            gameObject.transform.GetChild(x).GetChild(i).GetChild(0).gameObject.AddComponent<InventoryClickEvents>();
                            //Debug.Log("image");
                        }catch(Exception e)
                        {

                        }
                    }
                }
            }
        }
    }

    public static void ImageToDrag(Image clicked)
    {
        dragMe = clicked;
        origionalPosition = dragMe.transform.position;
        UIorder = dragMe.transform.parent;
        dragMe.transform.parent = GameObject.Find("Canvas").transform;
        //List<Item> items = Inventory1.RetrieveItems();
    }

    public static void UnclickDragImage()
    {
        List<Item> items = Inventory.RetrieveItems();
        for(int x = 0; x< EquipScreen.transform.childCount-3; x++)
        {
            foreach(Item item in items)
            {
                if (dragMe.name == item.id.ToString())
                {
                    if (EquipScreen.transform.GetChild(x).name != "Image")
                    {
                        if (EquipScreen.transform.GetChild(x).name == item.subType.ToString())
                        {
                            item.type = EquipScreen.transform.Find("Text").GetComponent<Text>().text;
                            EquipScreen.transform.GetChild(x).GetChild(0).GetComponent<Image>().sprite = dragMe.sprite;
                            dragMe.sprite = null;
                            dragMe.sprite = ImageHover;
                            Debug.Log(item.type);
                            Inventory.ReplaceAllItems(items);
                            break;

                        }
                    }
                }
            }
        }
            dragMe.transform.position = origionalPosition;
            dragMe.transform.parent = UIorder;
            dragMe = null;
        }
    }




