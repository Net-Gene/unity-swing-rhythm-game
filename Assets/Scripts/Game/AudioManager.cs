using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Assign an AudioSource component in the Inspector
    public AudioClip soundToPlay;  // Assign the audio clip to play in the Inspector

    private bool isPaused = false; // A flag to track if the game is paused

    public event Action GameResumed; // Event to trigger when the game is resumed

    // Start is called before the first frame update
    void Start()
    {
        if (audioSource != null && soundToPlay != null)
        {
            PlaySound();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check for the "Escape" key to toggle the pause state
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused; // Toggle the pause state

        if (isPaused)
        {
            Time.timeScale = 0.0f; // Pause the game time
            audioSource.pitch = 0.5f; // Adjust the pitch when paused
        }
        else
        {
            Time.timeScale = 1.0f; // Resume normal game time
            audioSource.pitch = 1.0f; // Reset pitch to normal

            // Trigger the GameResumed event
            GameResumed?.Invoke();
        }
    }

    void PlaySound()
    {
        audioSource.clip = soundToPlay;
        audioSource.Play();
    }
}
