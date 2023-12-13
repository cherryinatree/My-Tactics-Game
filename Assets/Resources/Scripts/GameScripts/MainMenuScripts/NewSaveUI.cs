using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NewSaveUI : MonoBehaviour
{

    public TextMeshProUGUI placeholder;
    public TextMeshProUGUI enteredText;



    // Start is called before the first frame update
    void Start()
    {
        StartGUI();
    }
    private void OnEnable()
    {
        StartGUI();
    }

    private void StartGUI()
    {

        placeholder.text = DateTime.Now.ToString();
    }

    public void JustSave()
    {
        if(enteredText.text != null && enteredText.text.ToCharArray().Length > 1)
        {
            Debug.Log(enteredText.text.ToCharArray().Length);
            SerializationManager.Save(enteredText.text, SaveData.Current.mainData);
        }
        else
        {
            string save = DateTime.Now.ToString();
            save = save.Replace("/", "-");
            save = save.Replace("\\", "-");
            save = save.Replace(":", "-");
            Debug.Log("placeholderText: " + save);
            SerializationManager.Save(save, SaveData.Current.mainData);
        }

        gameObject.SetActive(false);
    }


    public void BattleSave()
    {
        SaveManipulator.UpdateBattleSave();


        if (enteredText.text != null && enteredText.text.ToCharArray().Length > 1)
        {
            Debug.Log(enteredText.text.ToCharArray().Length);
            SerializationManager.Save(enteredText.text, SaveData.Current.mainData);
        }
        else
        {
            string save = DateTime.Now.ToString();
            save = save.Replace("/", "-");
            save = save.Replace("\\", "-");
            save = save.Replace(":", "-");
            Debug.Log("placeholderText: " + save);
            SerializationManager.Save(save, SaveData.Current.mainData);
        }

        gameObject.SetActive(false);
    }
}
