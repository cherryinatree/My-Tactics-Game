using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DojoController : MonoBehaviour
{

    public List<GameObject> Panels;
    public List<GameObject> Content;
    // Start is called before the first frame update
    void Awake()
    {
        SetUpSingleTons();
    }

    private void SetUpSingleTons()
    {
        SaveManipulator.LoadSceneChange();
        DojoSingleton.Instance.Panels = Panels;
        DojoSingleton.Instance.Content = Content;

        List<Character> characterList = new List<Character>();
        foreach (Character character in SaveData.Current.mainData.characters)
        {
            characterList.Add(character);
        }
        foreach (Character character in SaveData.Current.mainData.charactersStorage)
        {
            characterList.Add(character);
        }
        DojoSingleton.Instance.CharacterInventories = characterList;
    }
}
