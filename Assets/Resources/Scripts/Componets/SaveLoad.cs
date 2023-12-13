using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveLoad
{
    private static string _lastLoadedFile;

    public static MainSaveData LoadGameData(string whichFile)
    {
        string path = "C:\\Users\\tbgoods\\Desktop\\my_game\\my_game_2_Alpha_0_3\\saves\\" + whichFile + ".fun";
        if (File.Exists(path))
        {
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);

                MainSaveData data = formatter.Deserialize(stream) as MainSaveData;

                stream.Close();
                //Debug.Log(whichFile);
                return data;
            }
            catch (FileNotFoundException e)
            {
                // Debug.Log(e);
                MainSaveData newData = new MainSaveData();
                return newData;
            }

        }
        else
        {
            Debug.Log("Save file not found in " + path);
            MainSaveData newData = new MainSaveData();
            //Debug.Log("newData " + newData);
            return newData;
        }
    }

    public static void SaveGame(string whichFile, MainSaveData data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "C:\\Users\\tbgoods\\Desktop\\my_game\\my_game_2_Alpha_0_3\\saves\\" + whichFile + ".fun";
        FileStream stream;
        try
        {
            stream = new FileStream(path, FileMode.Create);
        }
        catch (FileLoadException e)
        {

            stream = new FileStream(path, FileMode.CreateNew);
        }
        MainSaveData theData = data;

        _lastLoadedFile = whichFile;

        formatter.Serialize(stream, theData);
        stream.Close();
    }

    public static void SaveGameTest(string whichFile, TestSave data)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = "C:\\Users\\tbgoods\\Desktop\\my_game\\my_game_2_Alpha_0_3\\saves\\" + whichFile + ".fun";
        FileStream stream;
        try
        {
            stream = new FileStream(path, FileMode.Create);
        }
        catch (FileLoadException e)
        {

            stream = new FileStream(path, FileMode.CreateNew);
        }
        TestSave theData = data;
        Debug.Log(data);
        _lastLoadedFile = whichFile;

        formatter.Serialize(stream, theData);
        stream.Close();
        Debug.Log("save successful");
    }
}
