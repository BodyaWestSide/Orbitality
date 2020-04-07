using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public static AIController Instance;

    private List<Planet> planets;

    private const float EnemyFireRate = 0.001f;

    private void Awake()
    {
        Instance = this;
    }

    public void LaunchAI(List<Planet> planets)
    {
        this.planets = planets;
    }

    private void Update()
    {
        if(planets != null)
        {
            foreach(Planet planet in planets)
            {
                if (!planet.IsPlayer)
                {
                    float rate = Random.Range(0, 1);
                    if(rate <= EnemyFireRate)
                    {
                        planet.Shoot();
                    }
                }
            }
        }
    }
}
