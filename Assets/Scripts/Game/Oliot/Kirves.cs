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
    private float liikkumisNopeus = 15f;

    // Lyönnin animaation aika
    private float lyonninAika = 1.1f;

    private Vector3 kohdeSijainti; // Tavoitesijainti liikkumista varten
    private bool lyontiKaynnissa = false; // Onko lyönti käynnissä

    private bool speedboost = false;



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

        if (speedboost == false)
        {
            if (GameLogic.score >= 200)
            {
                liikkumisNopeus = 20f;
                lyonninAika = 0.7f;
                speedboost = true;
            }
        }

    }

    // Liikuttaa kirvestä kohti tavoitesijaintia
    void LiikuKirvesta()
    {
        transform.position = Vector3.MoveTowards(transform.position, kohdeSijainti, liikkumisNopeus * Time.deltaTime);
    }

    // Nykyinen valittu kaista
    string nykyinenKaista = "B";

    void VaihdaKaistaa()
    {
        // Käsittele kosketustapahtumat
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                // Kosketus alkoi
                if (touch.position.x < Screen.width / 2)
                {
                    AsetaSeuraavaKaistaVasenmalle();
                }
                else
                {
                    AsetaSeuraavakaistaOikealle();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            AsetaSeuraavaKaistaVasenmalle();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            AsetaSeuraavakaistaOikealle();
        }
    }

    // Asettaa tavoitesijainnin valitulle kaistalle
    void AsetaKaista(string kaista)
    {
        if (kaista == "A")
        {
            kohdeSijainti = kaistaA;
            nykyinenKaista = "A";
        }
        else if (kaista == "B")
        {
            kohdeSijainti = kaistaB;
            nykyinenKaista = "B";
        }
        else if (kaista == "C")
        {
            kohdeSijainti = kaistaC;
            nykyinenKaista = "C";
        }
    }

    // Valitsee seuraavan kaistan järjestetyssä tilassa (A, B, C, C, B, A...)
    void AsetaSeuraavakaistaOikealle()
    {
        if (nykyinenKaista == "A")
        {
            AsetaKaista("B");
        }
        else if (nykyinenKaista == "B")
        {
            AsetaKaista("C");
        }
    }

    // Valitsee seuraavan kaistan järjestetyssä tilassa (C, B, A)
    void AsetaSeuraavaKaistaVasenmalle()
    {
        if (nykyinenKaista == "C")
        {
            AsetaKaista("B");
        }
        else if (nykyinenKaista == "B")
        {
            AsetaKaista("A");
        }
    }

    private Quaternion initialOrientation;

    // Alustetaan keskimmäinen kaista alkukaistaksi
    void Start()
    {
        AsetaKaista("B");

        if (Application.platform == RuntimePlatform.Android)
        {
            // Check if gyro is available
            if (SystemInfo.supportsGyroscope)
            {
                Input.gyro.enabled = true; // Enable the gyro
                                           // Store the initial orientation of the device
                initialOrientation = Input.gyro.attitude;
            }
            else
            {
                Debug.LogError("Gyro is not supported on this device.");
            }
        }
    }

    // Käynnistää lyöntianimaation välilyönnillä (Space) tai kun puhelin on palautettu alkuperäiseen asentoon
    void LyoKirveella()
    {
        
        if (Application.platform == RuntimePlatform.Android)
        {
            // Check if the phone is in the original orientation
            if (IsPhoneInOriginalOrientation())
            {
                StartCoroutine(LyoAnimaatio());
            }
        }
        else
        {
            // Check if the phone is in the original orientation
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(LyoAnimaatio());
            }
        }
    }

    // Tarkistaa, onko puhelin alkuperäisessä asennossa
    bool IsPhoneInOriginalOrientation()
    {
        // Use gyro data to check if the current orientation is close to the initial orientation
        Quaternion currentOrientation = Input.gyro.attitude;

        // Extract the Z angles for comparison
        float initialZAngle = initialOrientation.eulerAngles.z;
        float currentZAngle = currentOrientation.eulerAngles.z;

        // Calculate the absolute difference in Z angles
        float angleDifference = Mathf.Abs(Mathf.DeltaAngle(currentZAngle, initialZAngle));

        // Adjust the threshold based on your preference for how much Z angle change is required
        float zAngleThreshold = 10f;

        return angleDifference < zAngleThreshold;
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
