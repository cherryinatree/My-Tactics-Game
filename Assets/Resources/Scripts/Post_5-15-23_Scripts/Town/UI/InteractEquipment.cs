using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractEquipment : MonoBehaviour
{

    public InteractScroll scroll;
    private Character scrollCharacter;

    public Image[] equipmentDisplay;

    public Image weapon;
    public Image offhand;
    public Image helm;
    public Image armor;
    public Image boots;
    public Image amulet;


    public Image PlayerWeapon;
    public Image PlayerOffhand;
    public Image PlayerHelm;
    public Image PlayerArmor;
    public Image PlayerBoots;
    public Image PlayerAmulet;


    public Sprite defaultWeapon;
    public Sprite defaultOffhand;
    public Sprite defaultHelm;
    public Sprite defaultArmor;
    public Sprite defaultBoots;
    public Sprite defaultAmulet;

    public GameObject IconPanel;
    private int page;
    private string itemType;

    private GameObject iconPrefab;
    private Sprite defaultIcon;

    public Text pageText;


    private void OnEnable()
    {
        itemType = "all";
        scrollCharacter = scroll.SelectedCharacter;
        defaultIcon = Resources.Load<Sprite>("Images/Actions/Default");
        page = 0;
        UpdatePageText();
        UpdateUI();
        UpdatePlayerEquipmentUI();
    }

    public void EquipmentTransferUpdateUItoPlayer(Equipment icon)
    {
        switch (icon.type)
        {
            case "Weapon":
                if (scrollCharacter.weapon == -1)
                {
                    scrollCharacter.weapon = icon.id;
                    SaveData.Current.mainData.equipmentStorage.Remove(icon);
                }
                break;
            case "OffHand":

                if (scrollCharacter.offHand == -1)
                {
                    scrollCharacter.offHand = icon.id;
                    SaveData.Current.mainData.equipmentStorage.Remove(icon);
                }
                break;
            case "Helm":
                if (scrollCharacter.helm == -1)
                {
                    scrollCharacter.helm = icon.id;
                    SaveData.Current.mainData.equipmentStorage.Remove(icon);
                }
                break;
            case "Armor":
                if (scrollCharacter.armor == -1)
                {
                    scrollCharacter.armor = icon.id;
                    SaveData.Current.mainData.equipmentStorage.Remove(icon);
                }
                break;
            case "Boots":
                if (scrollCharacter.boots == -1)
                {
                    scrollCharacter.boots = icon.id;
                    SaveData.Current.mainData.equipmentStorage.Remove(icon);
                }
                break;
            case "Amulet":
                if (scrollCharacter.amulet == -1)
                {
                    scrollCharacter.amulet = icon.id;
                    SaveData.Current.mainData.equipmentStorage.Remove(icon);
                }
                break;
        }

        UpdateUI();
        UpdatePlayerEquipmentUI();
    }

    public void EquipmentTransferUpdateUItoStorage(Equipment icon)
    {
        Debug.Log("To Storage");
        switch (icon.type)
        {
            case "Weapon":

                Debug.Log(scrollCharacter.weapon);
                if (scrollCharacter.weapon != -1)
                {
                    scrollCharacter.weapon = -1;
                    SaveData.Current.mainData.equipmentStorage.Add(icon); 
                }
                break;
            case "OffHand":
                if (scrollCharacter.offHand != -1)
                {
                    scrollCharacter.offHand = -1;
                    SaveData.Current.mainData.equipmentStorage.Add(icon);
                }
                break;
            case "Helm":
                if (scrollCharacter.helm != -1)
                {
                    scrollCharacter.helm = -1;
                    SaveData.Current.mainData.equipmentStorage.Add(icon);
                }
                break;
            case "Armor":
                if (scrollCharacter.armor != -1)
                {
                    scrollCharacter.armor = -1;
                    SaveData.Current.mainData.equipmentStorage.Add(icon);
                }
                break;
            case "Boots":
                if (scrollCharacter.boots != -1)
                {
                    scrollCharacter.boots = -1;
                    SaveData.Current.mainData.equipmentStorage.Add(icon);
                }
                break;
            case "Amulet":
                if (scrollCharacter.amulet != -1)
                {
                    scrollCharacter.amulet = -1;
                    SaveData.Current.mainData.equipmentStorage.Add(icon);
                }
                break;
        }

        UpdateUI();
        UpdatePlayerEquipmentUI();
    }

    private void UpdatePlayerEquipmentUI()
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        if (scroll.SelectedCharacter != null)
        {
            if (scroll.SelectedCharacter.weapon != -1)
            {
                Equipment item = jsonRetriever.Load1Equipment(scroll.SelectedCharacter.weapon);
                PlayerWeapon.sprite = Resources.Load<Sprite>(item.icon);
                PlayerWeapon.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.id.ToString();
                PlayerWeapon.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerWeapon.GetComponentInChildren<Button>().onClick.AddListener(delegate
                { EquipmentTransferUpdateUItoStorage(item); });
            }
            else
            {
                PlayerWeapon.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerWeapon.sprite = defaultWeapon;
            }
            if (scroll.SelectedCharacter.offHand != -1)
            {
                Debug.Log(scroll.SelectedCharacter.offHand);
                Debug.Log(scroll.SelectedCharacter);
                Equipment item = jsonRetriever.Load1Equipment(scroll.SelectedCharacter.offHand);
                Debug.Log(item.icon);
                PlayerOffhand.sprite = Resources.Load<Image>(item.icon).sprite;
                PlayerOffhand.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.id.ToString();
                PlayerOffhand.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerOffhand.GetComponentInChildren<Button>().onClick.AddListener(delegate
                { EquipmentTransferUpdateUItoStorage(item); });
            }
            else
            {
                PlayerOffhand.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerOffhand.sprite = defaultOffhand;
            }
            if (scroll.SelectedCharacter.helm != -1)
            {
                Equipment item = jsonRetriever.Load1Equipment(scroll.SelectedCharacter.helm);
                PlayerHelm.sprite = Resources.Load<Image>(item.icon).sprite;
                PlayerHelm.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.id.ToString();
                PlayerHelm.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerHelm.GetComponentInChildren<Button>().onClick.AddListener(delegate
                { EquipmentTransferUpdateUItoStorage(item); });
            }
            else
            {
                PlayerHelm.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerHelm.sprite = defaultHelm;
            }
            if (scroll.SelectedCharacter.armor != -1)
            {
                Equipment item = jsonRetriever.Load1Equipment(scroll.SelectedCharacter.armor);
                PlayerArmor.sprite = Resources.Load<Image>(item.icon).sprite;
                PlayerArmor.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.id.ToString();
                PlayerArmor.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerArmor.GetComponentInChildren<Button>().onClick.AddListener(delegate
                { EquipmentTransferUpdateUItoStorage(item); });
            }
            else
            {
                PlayerArmor.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerArmor.sprite = defaultArmor;
            }
            if (scroll.SelectedCharacter.boots != -1)
            {
                Equipment item = jsonRetriever.Load1Equipment(scroll.SelectedCharacter.boots);
                PlayerBoots.sprite = Resources.Load<Image>(item.icon).sprite;
                PlayerBoots.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.id.ToString();
                PlayerBoots.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerBoots.GetComponentInChildren<Button>().onClick.AddListener(delegate
                { EquipmentTransferUpdateUItoStorage(item); });
            }
            else
            {
                PlayerBoots.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerBoots.sprite = defaultBoots;
            }
            if (scroll.SelectedCharacter.amulet != -1)
            {
                Equipment item = jsonRetriever.Load1Equipment(scroll.SelectedCharacter.amulet);
                PlayerAmulet.sprite = Resources.Load<Image>(item.icon).sprite;
                PlayerAmulet.gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = item.id.ToString();
                PlayerAmulet.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerAmulet.GetComponentInChildren<Button>().onClick.AddListener(delegate
                { EquipmentTransferUpdateUItoStorage(item); });
            }
            else
            {
                PlayerAmulet.GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                PlayerAmulet.sprite = defaultAmulet;
            }
        }
    }

    private void UpdatePageText()
    {
        int maxPage = getMaxPage();
        pageText.text = (page+1).ToString() + "/" + maxPage.ToString();

    }

    private int getMaxPage()
    {
        int maxPage = 0;
        int total = 0;
        if (itemType == "all")
        {
            maxPage = (int)Mathf.Floor(SaveData.Current.mainData.equipmentStorage.Count / 10) + 1;
        }
        else
        {
            for (int i = 0; i < SaveData.Current.mainData.equipmentStorage.Count; i++)
            {
                if (SaveData.Current.mainData.equipmentStorage[i].type == itemType)
                {
                    total++;
                }
            }
            maxPage = (int)Mathf.Floor(total / 10) + 1;
        }

        return maxPage;
    }

    private void UpdateUI()
    {

        iconPrefab = Resources.Load<GameObject>("Prefabs/PrefabsUI/ImageButton");
        ClearPanel();

        for (int i = 0; i < 10; i++)
        {

            GameObject icon = GameObject.Instantiate(iconPrefab);
            icon.transform.SetParent(IconPanel.transform, false);

            IconLoader(icon.transform.GetChild(0).transform.GetChild(0).gameObject, i, icon);
        }
    }

    private void IconLoader(GameObject icon, int position, GameObject iconDataDisplay)
    {
        int truePosition = position+(10*page);
        int step = 0;


        if (SaveData.Current.mainData.equipmentStorage.Count > truePosition)
        {

            if (itemType == "all")
            {
                icon.transform.Find("itemImage").GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                icon.transform.Find("itemImage").GetComponentInChildren<Button>().onClick.AddListener(delegate 
                { EquipmentTransferUpdateUItoPlayer(SaveData.Current.mainData.equipmentStorage[truePosition]); });

                icon.transform.Find("itemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(
                    SaveData.Current.mainData.equipmentStorage[truePosition].icon);

                icon.transform.Find("id").GetComponent<TextMeshProUGUI>().text = SaveData.Current.mainData.equipmentStorage[truePosition].id.ToString();

                return;
            }
            else
            {
                for (int i = 0; i < SaveData.Current.mainData.equipmentStorage.Count; i++)
                {
                    if(SaveData.Current.mainData.equipmentStorage[i].type == itemType)
                    {
                        if(step == truePosition)
                        {

                            icon.transform.Find("itemImage").GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                            iconDataDisplay.GetComponentInChildren<Button>().onClick.AddListener(delegate
                            { EquipmentTransferUpdateUItoPlayer(SaveData.Current.mainData.equipmentStorage[i]); });

                            icon.transform.Find("itemImage").GetComponent<Image>().sprite = Resources.Load<Sprite>(
                                SaveData.Current.mainData.equipmentStorage[i].icon);

                            icon.transform.Find("id").GetComponent<TextMeshProUGUI>().text = SaveData.Current.mainData.equipmentStorage[i].id.ToString();

                            return;
                        }
                        else
                        {
                            step++;
                        }
                    }
                }

                icon.transform.Find("itemImage").GetComponent<Image>().sprite = defaultIcon;
                icon.transform.Find("itemImage").GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                icon.transform.Find("id").GetComponent<TextMeshProUGUI>().text = "-1";
            }
        }
        else
        {
            icon.transform.Find("itemImage").GetComponent<Image>().sprite = defaultIcon;
            icon.transform.Find("itemImage").GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            icon.transform.Find("id").GetComponent<TextMeshProUGUI>().text = "-1";
        }
    }

    private void ClearPanel()
    {
        foreach (Transform child in IconPanel.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public void ButtonPageUp()
    {
        if (page + 1 < getMaxPage())
        {
            page++;
        }
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonPageDown()
    {
        if (page > 0)
        {
            page--;
        }
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonAll()
    {

        itemType = "all";
        page = 0;
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonWeapon()
    {
        itemType = "Weapon";
        page = 0;
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonOffHand()
    {

        itemType = "OffHand";
        page = 0;
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonHelm()
    {

        itemType = "Helm";
        page = 0;
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonArmor()
    {

        itemType = "Armor";
        page = 0;
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonBoots()
    {

        itemType = "Boots";
        page = 0;
        UpdateUI();
        UpdatePageText();
    }

    public void ButtonAmulet()
    {

        itemType = "Amulet";
        page = 0;
        UpdateUI();
        UpdatePageText();
    }

    private void Update()
    {
        if(scrollCharacter != scroll.SelectedCharacter)
        {
            scrollCharacter = scroll.SelectedCharacter;
            UpdatePlayerEquipmentUI();
        }
    }
}
