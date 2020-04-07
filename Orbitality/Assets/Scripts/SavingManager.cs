using System;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SavingManager
{
    public bool loadFromSave;

    private string saveDataPath = Application.persistentDataPath + "/savedGame.json";
    private static SavingManager instance;

    public static SavingManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SavingManager();
            }

            return instance;
        }
    }

    public void SaveGame(SavePlanetData savePlanetData)
    {
        string jsonData = JsonUtility.ToJson(savePlanetData, false);
        File.WriteAllText(saveDataPath, jsonData);
    }

    public SavePlanetData LoadGame()
    {
        try
        {
            return JsonUtility.FromJson<SavePlanetData>(File.ReadAllText(saveDataPath));
        }
        catch (Exception)
        {
            SceneManager.LoadScene("MainMenuScene");
            return null;
        }
    }
}
