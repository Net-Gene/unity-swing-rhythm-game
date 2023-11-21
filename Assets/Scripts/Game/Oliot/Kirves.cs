using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

// Kirveen toiminnallisuuden määrittävä luokka
public class Kirves : MonoBehaviour
{
    // Kaistojen koordinaatit
    private Vector3 kaistaA = new Vector3(-3f, 1.1f, -7.55f);
    private Vector3 kaistaB = new Vector3(0f, 1.1f, -7.55f);
    private Vector3 kaistaC = new Vector3(3f, 1.1f, -7.55f);

    // Liikkumisnopeus
    public float liikkumisNopeus = 15f;

    // Lyönnin animaation aika
    public float lyonninAika = 1.1f;

    private Vector3 kohdeSijainti; // Tavoitesijainti liikkumista varten
    private bool lyontiKaynnissa = false; // Onko lyönti käynnissä

    // Alustetaan keskimmäinen kaista alkukaistaksi
    void Start()
    {
        AsetaKaista("B");
    }

    // Päivitetään joka frame
    void Update()
    {
        // Varmistetaan, ettei lyönti ole käynnissä ennen liikkeiden tarkastelua
        if (!lyontiKaynnissa)
        {
            LiikuKirvesta();
            VaihdaKaistaa();
            LyoKirveella();
        }
    }

    // Liikuttaa kirvestä kohti tavoitesijaintia
    void LiikuKirvesta()
    {
        transform.position = Vector3.MoveTowards(transform.position, kohdeSijainti, liikkumisNopeus * Time.deltaTime);
    }

    // Vaihtaa kaistaa numeronäppäimillä 1-3
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

    // Käynnistää lyöntianimaation välilyönnillä (Space)
    void LyoKirveella()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(LyoAnimaatio());
        }
    }

    // Suorittaa lyöntianimaation Coroutine-na (animaation viiveet ym.)
    IEnumerator LyoAnimaatio()
    {
        lyontiKaynnissa = true;

        // Alustetaan lyönnin animaatio
        Quaternion alkuperainenRotaatio = transform.rotation;
        Vector3 alkuperainenPosition = transform.position;

        Quaternion tavoiteRotaatio = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        Vector3 tavoitePosition = new Vector3(transform.position.x, 0.5f, transform.position.z);

        float kulunutAika = 0f;

        transform.rotation = tavoiteRotaatio;
        transform.position = tavoitePosition;

        // Palautetaan kirveen alkuperäinen rotaatio ja sijainti
        while (kulunutAika < lyonninAika)
        {
            kulunutAika += Time.deltaTime;

            float osuus = kulunutAika / lyonninAika;
            transform.rotation = Quaternion.Slerp(tavoiteRotaatio, alkuperainenRotaatio, osuus);
            transform.position = Vector3.Lerp(tavoitePosition, alkuperainenPosition, osuus);

            yield return null;
        }

        // Palautetaan kirveen alkuperäinen rotaatio ja sijainti
        // transform.rotation = alkuperainenRotaatio;
        // transform.position = alkuperainenPosition;

        lyontiKaynnissa = false;
    }
}
