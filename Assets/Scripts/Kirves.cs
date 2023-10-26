using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Kirves : MonoBehaviour
{
    public float kaistanLeveys = 1.0f; // talla hetkella kaistan leveys on yksi yksikko (muutettava, kun mappi on modelattu)
    public float iskunKorkeus = 1.0f; // Korkeus, jossa isku tapahtuu

    private int nykyinenKaista = 1; // Alkuasetus kaistaan 1
    private float kaistanSijainti = 0.0f; // Aloitetaan keskimmaisesta kaistasta
    private float iskunAika = -1.0f; // Negatiivinen arvo tarkoittaa, ettei isku ole aktiivinen

    void Update()
    {
        LiikuKaistaa();
        IskeKirveella();
    }

    void LiikuKaistaa()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            VaihdaKaistaa(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            VaihdaKaistaa(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            VaihdaKaistaa(3);
        }
    }

    void VaihdaKaistaa(int uusiKaista)
    {
        nykyinenKaista = uusiKaista;
        kaistanSijainti = uusiKaista - 2;
    }

    void IskeKirveella()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (iskunAika < 0)
            {
                // Kaynnista isku, jos se ei ole jo kaynnissa
                iskunAika = Time.time;
            }
        }

        if (iskunAika > 0 && Time.time - iskunAika >= 0.1f)
        {
            // Tarkista, osuiko kirves tukkiin iskun aikana
            TarkistaKirveenOsumaTukkiin();
            iskunAika = -1.0f; // Nollaa iskun aika
        }
    }

    void TarkistaKirveenOsumaTukkiin()
    {
        // Tarkista, onko tukki kirveen korkeudella ja kaistalla
        Vector3 kirveenSijainti = new Vector3(kaistanSijainti, iskunKorkeus, transform.position.z);
        Collider[] osumat = Physics.OverlapSphere(kirveenSijainti, 0.5f);

        foreach (Collider osuma in osumat)
        {
            Tukki tukki = osuma.GetComponent<Tukki>();
            if (tukki != null)
            {
                // Tukki osui kirveeseen, tuhoa tukki
                Destroy(tukki.gameObject);
            }
        }
    }
}
