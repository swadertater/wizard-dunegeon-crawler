using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wizard : MonoBehaviour
{
    public static Wizard S;
    public static int health = 3;
    public static int score = 0;
    public static int enemiesKilledInCurrentRound;
    public static GameObject enemyKilledBy;

    public CharacterController controller;
    public float speed = 6f;
    public float turnSmoothTime = 0.1f;

    private float _turnSmoothVelocity;
    private Camera _mainCamera;
    private static AudioSource _takeDamageSoundEffect;


    void Awake()
    {
        if (S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempted to assign second Hero.S!");
        }
        health = 3;
        score = 0;
    }
    void Start()
    {
        _mainCamera = FindObjectOfType<Camera>();
        _takeDamageSoundEffect = gameObject.GetComponent<AudioSource>();
        _takeDamageSoundEffect.Stop();
    }
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler( 0f, angle, 0f);
            controller.Move(direction * speed * Time.deltaTime);
        }
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.red);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        CheckHealth();

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            if (health <= 1)
            {
                Follow.poi = other.gameObject;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

    public static void TakeDamage(int damage)
    {
        _takeDamageSoundEffect.Play();
        health -= damage;
    }

    private void CheckHealth()
    {
        if (health <= 1)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene("GameOver");
        Destroy(gameObject);
    }
}
