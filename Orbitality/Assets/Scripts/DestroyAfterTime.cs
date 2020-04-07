using System;
using System.Collections;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    [SerializeField] private float destroyTime;

    private void Start()
    {
        StartCoroutine(DestroyObject());
    }

    private IEnumerator DestroyObject()
    {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}
