using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static string saveFilePath = Application.persistentDataPath + "/savefile.json";

    public static void SaveGame(SaveData data)
    {
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Game Saved at: " + saveFilePath);
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game Loaded from: " + saveFilePath);
            return data;
        }
        else
        {
            Debug.Log("No save file found, creating new data.");
            return new SaveData();
        }
    }
}
