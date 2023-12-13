using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveMenu : MonoBehaviour
{
    public RectTransform playerScrollView;
    public GameObject CardPrefab;

    public string SelectedString;
    private int characterListLength;

    public bool isStorage = false;

    public SaveListPopulator populator;
    string[] list;

    // Start is called before the first frame update
    void Start()
    {
        StartGUI();
    }

    private void OnEnable()
    {
        StartGUI();
    }

    public void StartGUI()
    {

        CardPrefab = Resources.Load<GameObject>("Prefabs/PrefabsUI/SaveCard");


        list = SaveManipulator.GetListOfSaveNames();
        if (list != null)
        {
            if (list.Length > 0)
            {
                list = RemoveSceneChangeSave(list);
                
                SelectedString = list[0];
            }
            else
            {
                SelectedString = null;
            }

            if (populator == null)
            {
                populator = new SaveListPopulator(playerScrollView, CardPrefab, SelectedString);
            }

            characterListLength = list.Length;

            // Update the UI with the current item lists
            populator.NewCharacter(SelectedString);
            populator.UpdateUI(list);
            populator.HighlightCard();
        }
    }

    private string[] RemoveSceneChangeSave(string[] list)
    {

        int SceneChange = -1;
        foreach (string str in list)
        {
            if (str == "SceneChange.save" || str == "SceneChange")
            {
                SceneChange++;
                break;
            }
            else
            {
                SceneChange++;
            }
        }

        if (SceneChange >= 0)
        {


            string[] newList = new string[list.Length - 1];
            int x = 0;
            for (int i = 0; i < list.Length; i++)
            {
                if (i != SceneChange)
                {
                    newList[x] = list[i];
                    x++;
                }
            }
            list = newList;
        }
        return list;
    }


    private void FixedUpdate()
    {
        
    }
}
