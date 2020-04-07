using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuSceneManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void LoadGame()
    {
        SavingManager.Instance.loadFromSave = true;
        SceneManager.LoadScene("GameScene");
    }

    public void NewGame()
    {
        SavingManager.Instance.loadFromSave = false;
        SceneManager.LoadScene("GameScene");
    }
}
