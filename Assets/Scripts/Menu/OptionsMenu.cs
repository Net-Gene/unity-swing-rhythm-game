using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;  // ??nisekoitin, joka hallitsee ??nenvoimakkuutta

    public TMP_Dropdown resolutionDropdowns;  // Resoluution valintapudotusvalikko

    Resolution[] resolutions;  // Taulukko, joka sis?lt?? eri resoluutiot

    private void Start()
    {
        QualitySettings.SetQualityLevel(2);

        Screen.SetResolution(1920, 1080, Screen.fullScreen);

        /*
        // Haetaan laitteen tukemat resoluutiot
        resolutions = Screen.resolutions;

        // Tyhjennet??n resoluution valintapudotusvalikko
        resolutionDropdowns.ClearOptions();

        // Luodaan lista resoluutiovaihtoehdoista
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;  // Indeksi, joka osoittaa nykyisen resoluution

        for (int i = 0; i < resolutions.Length; i++)
        {
            // Luodaan resoluution vaihtoehto muodossa "leveys x korkeus"
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            // Tarkistetaan, onko t?m? resoluutio sama kuin nykyinen n?yt?n resoluutio
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        // Lis?t??n resoluution vaihtoehdot valintapudotusvalikkoon
        resolutionDropdowns.AddOptions(options);

        // Asetetaan valittu arvo valintapudotusvalikkoon nykyisen resoluution perusteella
        resolutionDropdowns.value = currentResolutionIndex;
        */

        // P?ivitet??n n?ytetty valinta
        resolutionDropdowns.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        // Asetetaan valittu resoluutio
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        // Asetetaan ??nenvoimakkuus ??nisekoittimessa
        audioMixer.SetFloat("volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        Debug.Log(QualitySettings.GetQualityLevel().ToString());
        // Asetetaan pelilaadun taso
        //        QualitySettings.SetQualityLevel(qualityIndex);

        Debug.Log(QualitySettings.GetQualityLevel().ToString());

    }


    public void SetFullscreen(bool isFullscreen)
    {
        // Toggle the fullscreen state
        Screen.fullScreen = !Screen.fullScreen;
    }
}
