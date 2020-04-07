using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetSpawner : MonoBehaviour
{
    [SerializeField] private List<Material> planetsMaterials;
    [SerializeField] private GameObject planetPrefab;
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float minRotationSpeed;
    [SerializeField] private float maxRotationSpeed;
    [SerializeField] private float minAngleSpeed;
    [SerializeField] private float maxAngleSpeed;

    private readonly List<Planet> planets = new List<Planet>();

    public List<Planet> SpawnPlanets(int planetsCount, float radius)
    {
        for (int i = 0; i < planetsCount; i++)
        {
            Planet planet = Create((i + 1) * radius);
            if (planet)
            {
                planets.Add(planet);
            }
        }

        return planets;
    }

    public List<Planet> SpawnPlanetsFromData(SavePlanetData savePlanetData)
    {
        for (int i = 0; i < savePlanetData.planets.Count; i++)
        {
            Planet planet = CreateFromData(savePlanetData.planets[i]);
            if (planet)
            {
                planets.Add(planet);
            }
        }

        return planets;
    }

    private Planet Create(float radius)
    {
        GameObject planetGameObject = Instantiate(planetPrefab, transform);
        Planet planet = planetGameObject.GetComponent<Planet>();
        if (planet)
        {
            float size = Random.Range(minSize, maxSize);
            float angleSpeed = Random.Range(minAngleSpeed, maxAngleSpeed);
            float rotationSpeed = Random.Range(minRotationSpeed, maxRotationSpeed);
            float startAngle = Random.Range(0, Mathf.PI * 2);
            int materialNumber = Random.Range(0, planetsMaterials.Count);
            Material material = planetsMaterials[materialNumber];
            planet.InitializePlanet(radius, size, startAngle, rotationSpeed, angleSpeed, material, materialNumber);
            return planet;
        }

        return null;
    }

    private Planet CreateFromData(PlanetData planetData)
    {
        GameObject planetGameObject = Instantiate(planetPrefab, transform);
        Planet planet = planetGameObject.GetComponent<Planet>();
        if (planet)
        {
            float size = planetData.size;
            float angleSpeed = planetData.angleSpeed;
            float rotationSpeed = planetData.rotationSpeed;
            float startAngle = planetData.currentAngle;
            Material material = planetsMaterials[planetData.materialNumber];
            planet.InitializePlanet(planetData.radius, size, startAngle, rotationSpeed, angleSpeed, material, planetData.materialNumber);
            return planet;
        }

        return null;
    }
}
