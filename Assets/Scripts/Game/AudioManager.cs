using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Aseta AudioSource-komponentti Inspectorissa
    public AudioClip soundToPlay;  // Aseta ‰‰nitiedosto soitettavaksi Inspectorissa

    private bool isPaused = false; // Lippu, joka seuraa, onko peli keskeytetty

    public event Action GameResumed; // Tapahtuma, joka laukaistaan, kun peli jatkuu

    // Start kutsutaan ennen ensimm‰ist‰ ruutup‰ivityst‰
    void Start()
    {
        if (audioSource != null && soundToPlay != null)
        {
            PlaySound();
        }
    }

    // Update kutsutaan kerran jokaisella ruudunp‰ivityksell‰
    void Update()
    {
        // Tarkista "Escape" -n‰pp‰in keskeytystilan k‰‰nt‰miseksi
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused; // K‰‰nn‰ keskeytystila

        if (isPaused)
        {
            Time.timeScale = 0.0f; // Keskeyt‰ peliaika
            audioSource.pitch = 0.5f; // S‰‰d‰ ‰‰nen korkeutta keskeytett‰ess‰
        }
        else
        {
            Time.timeScale = 1.0f; // Jatka normaalia peliaikaa
            audioSource.pitch = 1.0f; // Palauta ‰‰nen korkeus normaaliksi

            // Laukaise GameResumed-tapahtuma
            GameResumed?.Invoke();
        }
    }

    void PlaySound()
    {
        audioSource.clip = soundToPlay;
        audioSource.Play();
    }
}

