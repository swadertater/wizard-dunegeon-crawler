using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Casting : MonoBehaviour
{
    public Transform castPoint;
    public GameObject spellPrefab;

    public float spellSpeed = 20f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Cast();
        }
    }

    void Cast()
    {
        GameObject spell = Instantiate(spellPrefab, castPoint.position, castPoint.rotation);
        Rigidbody rb = spell.GetComponent<Rigidbody>();
        rb.AddForce(castPoint.up * spellSpeed, ForceMode.Impulse);
    }
}
