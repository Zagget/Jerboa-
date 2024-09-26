using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandParticleController : MonoBehaviour
{
    ParticleSystem sandParticles;
    PlayerController playerController;
    Rigidbody2D rb2D;


    void Start()
    {
        sandParticles = GetComponentInChildren<ParticleSystem>();

        playerController = GetComponentInParent<PlayerController>();

        if (playerController == null)
        {
            Debug.Log("No PlayerController found on parent object!");
        }
        else
        {
            rb2D = playerController.GetComponent<Rigidbody2D>();
        }
    }

    void Update()
    {
        if (playerController != null && rb2D != null)
        {
            if (playerController.groundCheck && rb2D.velocity.x > 0)
            {
                EnableParticleSystem();
            }
            if (!playerController.groundCheck && rb2D.velocity.x <= 0)
            {
                DisableParticleSystem();
            }
        }
    }

    private void EnableParticleSystem()
    {
        sandParticles.gameObject.SetActive(true);
    }

    private void DisableParticleSystem()
    {
        sandParticles.gameObject.SetActive(false);
    }

}
