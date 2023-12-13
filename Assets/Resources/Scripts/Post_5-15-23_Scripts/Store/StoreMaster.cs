using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMaster : MonoBehaviour
{

    public int storeID = 0;
    public GameObject[] Panels;
    public GameObject[] Content;

    // Start is called before the first frame update
    void Awake()
    {

        SaveManipulator.LoadSceneChange(); 
        LoadJSON();
        CreateLists();
        SetUpSingleton();
    }


    private void LoadJSON()
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        List<StoreOwner> storeOwnerList = jsonRetriever.LoadAllStoreOwners();
        StoreSingleton.Instance.storeOwner = storeOwnerList[storeID];
    }


    private void CreateLists()
    {
        List<Item> itemList = new List<Item>();
        List<Equipment> equipmentList = new List<Equipment>();
        JsonRetriever jsonRetriever = new JsonRetriever();

        for (int i = 0; i < StoreSingleton.Instance.storeOwner.items.Length; i++)
        {
            itemList.Add(jsonRetriever.Load1Item(StoreSingleton.Instance.storeOwner.items[i]));
        }
        for (int i = 0; i < StoreSingleton.Instance.storeOwner.equipment.Length; i++)
        {
            equipmentList.Add(jsonRetriever.Load1Equipment(StoreSingleton.Instance.storeOwner.equipment[i]));
        }

        StoreSingleton.Instance.equipmentList = equipmentList;
        StoreSingleton.Instance.itemList = itemList;

    }

    private void SetUpSingleton()
    {
        StoreSingleton.Instance.Panels = Panels;
        StoreSingleton.Instance.Content = Content;

        StoreSingleton.Instance.stats = SaveData.Current.mainData.characters;
        foreach(Character character in SaveData.Current.mainData.charactersStorage)
        {
            StoreSingleton.Instance.stats.Add(character);
        }
    }
}
