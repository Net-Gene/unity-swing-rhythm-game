using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;



public class Kirves : MonoBehaviour
{
    // Kaistojen koordinaatit
    private Vector3 kaistaA = new Vector3(-3f, 0f, 2.5f);
    private Vector3 kaistaB = new Vector3(0f, 0f, 2.5f);
    private Vector3 kaistaC = new Vector3(3f, 0f, 2.5f);


    // Liikkumisnopeus 
    public float liikkumisNopeus = 5f;

    // Lyönnin animaation aika 
    public float lyonninAika = 0.4f;

    private Vector3 kohdeSijainti; // Tavoitesijainti liikkumista varten 
    private bool lyontiKaynnissa = false; // Onko lyönti käynnissä 


    void Start()
    {
        // Asetetaan keskimmäinen kaista alkukaistaksi
        AsetaKaista("B");
    }


    void Update()
    {
        if (!lyontiKaynnissa)
        {
            LiikuKirvesta();
            VaihdaKaistaa();
            LyoKirveella();
        }
        // Outo ongelma liikuttaa kirvestä kokoajan Z = 12.55 kohti
        Vector3 zSijainti = transform.position;
        zSijainti.z = 2.5f;
        transform.position = zSijainti;
    }


    void LiikuKirvesta()
    {
        // Liikutetan kirvestä kohti tavoitesijaintia 
        transform.position = Vector3.MoveTowards(transform.position, kohdeSijainti, liikkumisNopeus * Time.deltaTime);
    }


    void VaihdaKaistaa()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AsetaKaista("A");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AsetaKaista("B");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AsetaKaista("C");
        }
    }


    void AsetaKaista(string kaista)
    {
        // Asetetaan kohdesijainti valitulle kaistalle 
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


    void LyoKirveella()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Käynnistetään lyönti 
            StartCoroutine(LyoAnimaatio());
        }
    }


    IEnumerator LyoAnimaatio()
    {
        lyontiKaynnissa = true;


        // Aloitetaan lyönnin animaatio
        Quaternion alkuperainenRotaatio = transform.rotation;
        Vector3 alkuperainenPosition = transform.position;


        Quaternion tavoiteRotaatio = Quaternion.Euler(100f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Vector3 tavoitePosition = new Vector3(transform.position.x, -0.5f, transform.position.z);


        float kulunutAika = 0f;
         
        transform.rotation = tavoiteRotaatio;
        // transform.position = tavoitePosition;

        // Palautetaan kirveen alkuperäinen rotaatio ja sijainti
        while (kulunutAika < lyonninAika)
        {
            kulunutAika += Time.deltaTime;

            float osuus = kulunutAika / lyonninAika;
            transform.rotation = Quaternion.Slerp(tavoiteRotaatio, alkuperainenRotaatio, osuus);
            // transform.position = Vector3.Lerp(tavoitePosition, alkuperainenPosition, osuus);


            yield return null;
        }


        // Palautetaan kirveen alkuperäinen rotaatio ja sijainti 
        // transform.rotation = alkuperainenRotaatio;
        // transform.position = alkuperainenPosition;


        lyontiKaynnissa = false;
    }
}

