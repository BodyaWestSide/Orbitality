using UnityEngine;

public class Rocket : MonoBehaviour
{
    public float damage;

    public void InitializeRocket(float damage)
    {
        this.damage = damage;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
