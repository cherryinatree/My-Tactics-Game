using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Runtime.Serialization.Formatters.Binary;


namespace GamingTools
{
    public static class SaveSystem
    {
        public static string _lastLoadedFile;

        public static SaveFile LoadGameData(string whichFile)
        {
            _lastLoadedFile = whichFile;
            string path = Application.persistentDataPath + whichFile + ".fun";
            if (File.Exists(path))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    FileStream stream = new FileStream(path, FileMode.Open);

                    SaveFile data = formatter.Deserialize(stream) as SaveFile;

                    stream.Close();
                    //Debug.Log(whichFile);
                    return data;
                }
                catch (FileNotFoundException e)
                {
                    // Debug.Log(e);
                    SaveFile newData = new SaveFile();
                    return newData;
                }

            }
            else
            {
                Debug.Log("Save file not found in " + path);
                SaveFile newData = new SaveFile();
                //Debug.Log("newData " + newData);
                return newData;
            }
        }

        public static void SaveGame(string whichFile, SaveFile data)
        {
            _lastLoadedFile = whichFile;
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + whichFile + ".fun";
            FileStream stream;
            try
            {
                stream = new FileStream(path, FileMode.Create);
            }
            catch (FileLoadException e)
            {

                stream = new FileStream(path, FileMode.CreateNew);
            }
            SaveFile theData = data;


            formatter.Serialize(stream, theData);
            stream.Close();
            _lastLoadedFile = whichFile;
        }

        public static void SaveGameTest(string whichFile, SaveFile data)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + whichFile + ".fun";
            FileStream stream;
            try
            {
                stream = new FileStream(path, FileMode.Create);
            }
            catch (FileLoadException e)
            {

                stream = new FileStream(path, FileMode.CreateNew);
            }
            SaveFile theData = data;
            Debug.Log(data);
            _lastLoadedFile = whichFile;

            formatter.Serialize(stream, theData);
            stream.Close();
            Debug.Log("save successful");
        }

        public static class SaveNamesFinder
        {
            public static SaveNames LoadTheNames(string whichFile)
            {
                string path = Application.persistentDataPath + whichFile + ".fun";
                if (File.Exists(path))
                {
                    try
                    {
                        BinaryFormatter formatter = new BinaryFormatter();
                        FileStream stream = new FileStream(path, FileMode.Open);

                        SaveNames data = formatter.Deserialize(stream) as SaveNames;

                        stream.Close();
                        //Debug.Log(whichFile);
                        return data;
                    }
                    catch (FileNotFoundException e)
                    {
                        // Debug.Log(e);
                        SaveNames newData = new SaveNames();
                        return newData;
                    }

                }
                else
                {
                    Debug.Log("Save file not found in " + path);
                    SaveNames newData = new SaveNames();
                    //Debug.Log("newData " + newData);
                    return newData;
                }
            }

            public static void SaveTheNames(string whichFile, SaveNames data)
            {
                BinaryFormatter formatter = new BinaryFormatter();
                string path = Application.persistentDataPath + whichFile + ".fun";
                FileStream stream;
                try
                {
                    stream = new FileStream(path, FileMode.Create);
                }
                catch (FileLoadException e)
                {

                    stream = new FileStream(path, FileMode.CreateNew);
                }
                SaveNames theData = data;


                formatter.Serialize(stream, theData);
                stream.Close();
            }

        }
    }
}
