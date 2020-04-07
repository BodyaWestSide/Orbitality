using System.Collections.Generic;
using UnityEngine;

public static class PrecreatedPlanets
{
    public static List<PlanetType> planetTypes = new List<PlanetType>
    {
        new PlanetType{health = 1, cooldown = 2.5f, rocketDamage = 0.3f, rocketForce = 4200},
        new PlanetType{health = 1, cooldown = 1.5f, rocketDamage = 0.2f, rocketForce = 3500},
        new PlanetType{health = 1, cooldown = 3.5f, rocketDamage = 0.1f, rocketForce = 7000},
        new PlanetType{health = 1, cooldown = 2.5f, rocketDamage = 0.2f, rocketForce = 5000},
        new PlanetType{health = 1, cooldown = 4.5f, rocketDamage = 0.4f, rocketForce = 6000},
        new PlanetType{health = 1, cooldown = 3.5f, rocketDamage = 0.3f, rocketForce = 5000},
        new PlanetType{health = 1, cooldown = 2.5f, rocketDamage = 0.3f, rocketForce = 4000},
        new PlanetType{health = 1, cooldown = 1.5f, rocketDamage = 0.2f, rocketForce = 7000},
        new PlanetType{health = 1, cooldown = 5.5f, rocketDamage = 0.5f, rocketForce = 9000},
    };

    public static PlanetType GetRandomPlanet()
    {
        return planetTypes[Random.Range(0, planetTypes.Count)];
    }
}
