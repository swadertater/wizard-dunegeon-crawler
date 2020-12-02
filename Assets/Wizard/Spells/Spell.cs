using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    [Header("Set in Unity")]
    public float time = 1f;
    void Start()
    {
        StartCoroutine(SelfDestruct(time));
    }
    private void OnCollisionEnter(Collision other)
    {
        Destroy(gameObject);
    }

    IEnumerator SelfDestruct(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }
}
