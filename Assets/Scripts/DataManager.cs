using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataManager : MonoBehaviour
{
    public PlayerData data;
    public string file = "player.txt";

    public void save()
    {
        string json = JsonUtility.ToJson(data);
        writeFile(file, json);
    }

    public void load()
    {
        data = new PlayerData();
        string json = readFromFile(file);
        JsonUtility.FromJsonOverwrite(json, data);
    }

    private void writeFile(string fileName, string json)
    {
        string path = getFilePath(fileName);
        FileStream stream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(stream))
        {
            writer.Write(json);
        }
    }

    private string readFromFile(string fileName)
    {
        string path = getFilePath(fileName);
        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("No save found");
            return "";
        }
    }

    private string getFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
