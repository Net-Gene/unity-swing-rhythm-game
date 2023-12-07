using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Kirveen toiminnallisuuden m��ritt�v� luokka
public class Kirves : MonoBehaviour
{
    // Kaistojen koordinaatit
    private Vector3 kaistaA = new Vector3(-3f, 1.1f, -7.55f);
    private Vector3 kaistaB = new Vector3(0f, 1.1f, -7.55f);
    private Vector3 kaistaC = new Vector3(3f, 1.1f, -7.55f);

    // Liikkumisnopeus
    private float liikkumisNopeus = 15f;

    // Ly�nnin animaation aika
    private float lyonninAika = 1.1f;

    private Vector3 kohdeSijainti; // Tavoitesijainti liikkumista varten
    private bool lyontiKaynnissa = false; // Onko ly�nti k�ynniss�

    private bool speedboost = false;

    // Alustetaan keskimm�inen kaista alkukaistaksi
    void Start()
    {
        AsetaKaista("B");
    }

    // P�ivitet��n joka frame
    void Update()
    {
        // Varmistetaan, ettei ly�nti ole k�ynniss� ennen liikkeiden tarkastelua
        if (!lyontiKaynnissa)
        {
            LiikuKirvesta();
            VaihdaKaistaa();
            LyoKirveella();
        }

        if(speedboost == false)
        {
            if (GameLogic.score >= 200)
            {
                liikkumisNopeus = 20f;
                lyonninAika = 0.7f;
                speedboost = true;
            }
        }
    }

    // Liikuttaa kirvest� kohti tavoitesijaintia
    void LiikuKirvesta()
    {
        transform.position = Vector3.MoveTowards(transform.position, kohdeSijainti, liikkumisNopeus * Time.deltaTime);
    }

    // Vaihtaa kaistaa numeron�pp�imill� 1-3
    void VaihdaKaistaa()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            AsetaKaista("A");
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            AsetaKaista("B");
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AsetaKaista("C");
        }
    }

    // Asettaa tavoitesijainnin valitulle kaistalle
    void AsetaKaista(string kaista)
    {
        if (kaista == "A")
        {
            kohdeSijainti = kaistaA;
        }
        else if (kaista == "B")
        {
            kohdeSijainti = kaistaB;
        }
        else if (kaista == "C")
        {
            kohdeSijainti = kaistaC;
        }
    }

    // K�ynnist�� ly�ntianimaation v�lily�nnill� (Space)
    void LyoKirveella()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LyoAnimaatio());
        }
    }

    // Suorittaa ly�ntianimaation Coroutine-na (animaation viiveet ym.)
    IEnumerator LyoAnimaatio()
    {
        lyontiKaynnissa = true;

        // Alustetaan ly�nnin animaatio
        Quaternion alkuperainenRotaatio = transform.rotation;
        Vector3 alkuperainenPosition = transform.position;

        Quaternion tavoiteRotaatio = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Vector3 tavoitePosition = new Vector3(transform.position.x, 0.5f, transform.position.z);

        float kulunutAika = 0f;

        transform.rotation = tavoiteRotaatio;
        transform.position = tavoitePosition;

        // Palautetaan kirveen alkuper�inen rotaatio ja sijainti
        while (kulunutAika < lyonninAika)
        {
            kulunutAika += Time.deltaTime;

            float osuus = kulunutAika / lyonninAika;
            transform.rotation = Quaternion.Slerp(tavoiteRotaatio, alkuperainenRotaatio, osuus);
            transform.position = Vector3.Lerp(tavoitePosition, alkuperainenPosition, osuus);

            yield return null;
        }

        // Palautetaan kirveen alkuper�inen rotaatio ja sijainti
        // transform.rotation = alkuperainenRotaatio;
        // transform.position = alkuperainenPosition;

        lyontiKaynnissa = false;
    }
}
