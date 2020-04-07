using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController
{
    public readonly UnityEvent OnGameWin = new UnityEvent();
    public readonly UnityEvent OnGameLose = new UnityEvent();

    private const int MaxPlayersCount = 4;
    private const int MinPlayersCount = 2;

    private const int Radius = 2;

    private PlanetSpawner planetSpawner;

    private List<Planet> planets;
    private Planet player;
    private SavePlanetData savePlanetData;

    private static GameController instance;

    public static GameController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameController();
            }

            return instance;
        }
    }

    public void LaunchGame(PlanetSpawner planetSpawner)
    {
        this.planetSpawner = planetSpawner;

        if (SavingManager.Instance.loadFromSave)
        {
            savePlanetData = SavingManager.Instance.LoadGame();
            planets = new List<Planet>(planetSpawner.SpawnPlanetsFromData(savePlanetData));
            for (int i = 0; i < savePlanetData.planets.Count; i++)
            {
                if (savePlanetData.planets[i].isPlayer)
                {
                    player = planets[i];
                }
            }
        }
        else
        {
            planets = new List<Planet>(planetSpawner.SpawnPlanets(Random.Range(MinPlayersCount, MaxPlayersCount), Radius));
            player = planets[Random.Range(0, planets.Count)];
        }

        player.IsPlayer = true;
    }

    public void StartGame()
    {
        for (int i = 0; i < planets.Count; i++)
        {
            Planet planet = planets[i];
            if (savePlanetData != null)
            {
                planet.CreatePlanet(savePlanetData.planets[i].planetType);
                planet.CurrentHealth = savePlanetData.planets[i].currentHealth;
            }
            else
            {
                planet.CreatePlanet(PrecreatedPlanets.GetRandomPlanet());
            }

            planet.OnPlanetDestroyed.AddListener(OnPlanetDestroyed);
        }
        
        AIController.Instance.LaunchAI(planets);
    }

    public SavePlanetData GetLevelSaveData()
    {
        SavePlanetData savePlanetData = new SavePlanetData();
        foreach (var planet in planets)
        {
            PlanetData planetData = planet.GetPlanetData();
            savePlanetData.planets.Add(planetData);
        }

        return savePlanetData;
    }

    private void OnPlanetDestroyed(Planet planet)
    {
        planets.Remove(planet);

        if (player == planet)
        {
            LoseGame();
        }
        else if (planets.Count == 1)
        {
            WinGame();
        }
    }

    private void WinGame()
    {
        OnGameWin.Invoke();
    }

    private void LoseGame()
    {
        OnGameLose.Invoke();
    }
}
