using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Yarn.Unity;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Animator movementAnimation;

    [SerializeField] private Vector2 _distanceBeforeEncounter = new Vector2(30, 80);
    private static float passedDistance;
    private static float nextEncounterDistance = 0;

    void Start()
    {
        _distanceBeforeEncounter = new Vector2(20, 40);

        rb = GetComponent<Rigidbody>();
        if (nextEncounterDistance == 0)
        {
            GetNextEncounterDistance();
            passedDistance = 0;
        }
    }

    private void GetNextEncounterDistance()
    {
        nextEncounterDistance = UnityEngine.Random.Range(_distanceBeforeEncounter.x, _distanceBeforeEncounter.y);
        passedDistance = 0;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (PlayerInformation.Instance != null && PlayerInformation.Instance.isInCombat)
        {
            return;
        }


        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Get the camera's forward and right directions
        Vector3 forward = transform.up;
        Vector3 right = transform.right;

        // Flatten the directions to prevent unwanted vertical movement
        forward.y = 0;
        right.y = 0;
        forward.Normalize();
        right.Normalize();

        // Calculate movement direction relative to the camera
        Vector3 moveDirection = (right * moveX + forward * moveZ).normalized;

        // Stop movement when no input is given
        if (moveDirection.magnitude == 0)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            movementAnimation.Play("Stay");
        }
        else
        {
            rb.velocity = new Vector3(moveDirection.x * speed, rb.velocity.y, moveDirection.z * speed);

            passedDistance += rb.velocity.magnitude * Time.deltaTime;
            if (passedDistance > nextEncounterDistance)
            {
                PlayEncounter();
                GetNextEncounterDistance();
            }


            movementAnimation.Play("Walking");

            AudioManager.Instance.Play("Walking");
        }
    }

    private void PlayEncounter()
    {
        if (SceneManager.GetActiveScene().name == "PrisonCell" || SceneManager.GetActiveScene().name == "VaticanInt")
        {
            return;
        }
        else
        {
            DialogueRunner dr = FindObjectOfType<DialogueRunner>();
            if (dr.IsDialogueRunning) return;
            dr.StartDialogue("RandomEncounter");
        }
    }
}