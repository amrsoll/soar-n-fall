using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    //public AudioClip narratorSound;
    AudioClip clip2;
    public AudioSource narratorSource;
    AudioClip[] audioClipArray;
    Dictionary<string, AudioClip> audioClipDic;


    // Use this for initialization 
    void Start()
    {
        bool soundplayed = false;

        audioClipArray = Resources.LoadAll<AudioClip>("Audio/Narration");

        audioClipDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in audioClipArray) {

            audioClipDic.Add(clip.name, clip);

        }

        narratorSource = GetComponent<AudioSource>();

        if (!soundplayed)
        {
            PlayClip("startNarration");
            soundplayed = true;
        }
    }


    public void PlayClip(string clipName)
    {

        narratorSource.PlayOneShot(audioClipDic[clipName], 10.0f);

    }
}