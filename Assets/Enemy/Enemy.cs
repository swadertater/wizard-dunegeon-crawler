using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Numerics;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject objToFollow;
    public float baseSpeed = 8f;
    public int killPoints = 50;

    private NavMeshAgent _enemyAgent;
    private int _health = 10;
    private AudioSource _walkingSoundEffect;

    void Start()
    {
        objToFollow = Wizard.S.gameObject;
        _enemyAgent = GetComponent<NavMeshAgent>();
        _enemyAgent.speed = CalculateEnemySpeed();
        _walkingSoundEffect = gameObject.GetComponent<AudioSource>();
        _walkingSoundEffect.Play();
    }

    void Update()
    {
        if (objToFollow)
        {
            _enemyAgent.destination = objToFollow.transform.position;
        }

    }

    private void OnCollisionEnter(Collision other)
    {
        
        if (other.gameObject.tag == "Spell")
        {
            Wizard.score += killPoints;
            Wizard.enemiesKilledInCurrentRound++;
            Destroy(gameObject);
        }
    }

    private float CalculateEnemySpeed()
    {
        return baseSpeed;
    }
}
