using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildMaster : MonoBehaviour
{

    List<GameObject> Panels;
    List<GameObject> Content;
    List<Character> Recruits;
    List<Character> AllPossibleRecuits;

    // Start is called before the first frame update
    void Start()
    {
        SaveManipulator.TestNewSaveAddAllCharacters();
        SetUpRecruits();
        SetUpSingleton();
    }

    private void SetUpRecruits()
    {
        JsonRetriever jsonRetriever = new JsonRetriever();
        AllPossibleRecuits = jsonRetriever.LoadAllCharacters(RetrieverConstants.JsonCharacter);

        if(SaveData.Current.mainData.recruits.recruits == null)
        {
            RandomRecruits();
        }else if(SaveData.Current.mainData.playerData.day != SaveData.Current.mainData.recruits.LastDayRefreshed)
        {
            RandomRecruits();
        }
        else
        {
            Recruits = SaveData.Current.mainData.recruits.recruits;
        }

        GuildSingleton.Instance.RecruitableCharacters = Recruits;
    }

    private void RandomRecruits()
    {

        JsonRetriever jsonRetriever = new JsonRetriever();
        Recruits = new List<Character>();
        for (int i = 0; i < SaveData.Current.mainData.recruits.MaxNewRecruits; i++)
        {
            Recruits.Add(jsonRetriever.Load1Character(AllPossibleRecuits[Random.Range(0, AllPossibleRecuits.Count)].id));
        }
        SaveData.Current.mainData.recruits.recruits = Recruits;
    }

    private void SetUpSingleton()
    {
        GuildSingleton.Instance.Panels = Panels;
        GuildSingleton.Instance.Content = Content;
    }

}
