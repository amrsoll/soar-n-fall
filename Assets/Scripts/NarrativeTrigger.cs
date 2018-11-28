using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    //public AudioClip narratorSound;
    AudioClip clip2;
    AudioSource narratorSource;
    public AudioClip[] audioClipArray;

    // Use this for initialization 
    void Start()
    {
        audioClipArray = Resources.LoadAll<AudioClip>("Audio/Narration");
        clip2 = Resources.Load("Audio/Narration/goodJob") as AudioClip;
        narratorSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayClip("goodJob");
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayClip("hello");
        }
    }

    public void PlayClip(string clipName)
    {
        foreach (AudioClip clip in audioClipArray)
        {
            if (clip.name == clipName)
            {
                narratorSource.PlayOneShot(clip, 1f);
            }
            Debug.Log(clip.name);
        }
    }
}


/*
 * Skapa en funktion som tar in P:s klass, som i sin tur kollar om två objekt har blivit kombinerade.
 * När detta sker ska ett ljud spelas.
 * Detta kan göras genom ett skript som kallar P:s klass och sedan triggar igång ljudet.
 * 
 * Frågor: eftersom skriptet inte är kopplat till ett speciellt gameobject, hur funkar det då?
 * Jag förstår ju att jag kan koppla in andra klasser och funktioner som i sin tur gör att jag kan anknyta till specifika gameobjects.
 * Jag behöver bara ta reda på om jag kan t.ex. anknyta en audiosource till, ja VILKET GAMEOBJECT?
 * Kanske till kameran, så att det låter för spelaren?
 * 
 * Annan fråga: Hur ska jag göra detta på bästa sätt? En trigger-funktion som kollar NÄR något har skett, ska då sätta igång ljudet EN gång osv.
 * DÅ måste jag göra ett antal olika cases där beroende på vilka element som har kombinerats eller beroende på om spelaren har gjort något
 * speciellt ska dessa ljud sättas igång.
 * 
 * Det A sa till mig:
 * Gör en funktion vars input är någon form av key. Beroende på vilken key det är, kör en till funktion som letar igenom en lista med olika cases,
 * alltså olika kombinationer av element som har kombinerats eller andra events som gör att ett visst ljud ska spelas. 
 * Sedan kan jag köra audioclip.playonce eller vad det nu är för att köra ljudet en gång. På så sätt behöver jag nog inte ha någon audiosource.
*/
