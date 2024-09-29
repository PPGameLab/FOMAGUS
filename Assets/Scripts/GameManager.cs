using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SaveData playerData;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadGame();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SaveGame()
    {
        SaveSystem.SaveGame(playerData);
        Debug.Log("Game Saved");
    }

    public void LoadGame()
    {
        playerData = SaveSystem.LoadGame();
        Debug.Log("Game Loaded");
    }

    public void ModifyAttribute(string attributeName, int amount)
    {
        var type = typeof(SaveData);
        
        var field = type.GetField(attributeName);
        
        if (field != null && field.FieldType == typeof(int))
        {
            int currentValue = (int)field.GetValue(playerData);
            
            field.SetValue(playerData, currentValue + amount);
            
            Debug.Log($"{attributeName} change to {amount}. New Value: {(int)field.GetValue(playerData)}");
            SaveGame(); 
        }
        else
        {
            Debug.LogWarning($"Attribute '{attributeName}' not found");
        }
    }
}
