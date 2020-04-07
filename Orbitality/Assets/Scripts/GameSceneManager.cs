using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private PlanetSpawner planetSpawner;
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject pauseScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public void PauseButton()
    {
        Pause(true);
        pauseScreen.SetActive(true);
    }

    public void HideStartScreen()
    {
        startScreen.SetActive(false);
        GameController.Instance.StartGame();
    }

    public void SaveAndQuit()
    {
        SavingManager.Instance.SaveGame(GameController.Instance.GetLevelSaveData());
        QuitGame();
    }

    public void QuitGame()
    {
        Pause(false);
        SceneManager.LoadScene("MainMenuScene", LoadSceneMode.Single);
    }

    private void Awake()
    {
        GameController.Instance.LaunchGame(planetSpawner);
        ActivateStartScreen();
        GameController.Instance.OnGameLose.AddListener(OnGameLose);
        GameController.Instance.OnGameWin.AddListener(OnGameWin);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pauseScreen.activeSelf)
            {
                Pause(false);
                pauseScreen.SetActive(false);
            }
            else
            {
                Pause(true);
                pauseScreen.SetActive(true);
            }
        }
    }

    private void ActivateStartScreen()
    {
        startScreen.SetActive(true);
    }

    private void OnGameLose()
    {
        Pause(true);
        loseScreen.SetActive(true);
    }

    private void OnGameWin()
    {
        Pause(true);
        winScreen.SetActive(true);
    }

    private void Pause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }
}
