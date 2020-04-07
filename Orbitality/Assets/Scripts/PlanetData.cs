using System;

[Serializable]
public class PlanetData
{
    public float radius;
    public float currentAngle;
    public float rotationSpeed;
    public float angleSpeed;
    public bool isPlayer;
    public float size;
    public float currentHealth;
    public PlanetType planetType = new PlanetType();
    public int materialNumber;
}
