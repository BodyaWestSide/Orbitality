using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionLauncher : MonoBehaviour
{
    [SerializeField] private GameObject explosion;

    private bool isQuiting;

    private void OnApplicationQuit()
    {
        isQuiting = true;
    }

    private void OnDestroy()
    {
        if (!isQuiting)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }  
    }
}
