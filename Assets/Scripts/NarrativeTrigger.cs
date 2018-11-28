using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrativeTrigger : MonoBehaviour
{
    //public AudioClip narratorSound;
    AudioClip clip2;
    AudioSource narratorSource;
    AudioClip[] audioClipArray;
    private Dictionary<string, AudioClip> audioClipDic;

    // Use this for initialization 
    void Start()
    {
        audioClipArray = Resources.LoadAll<AudioClip>("Audio/Narration");

        audioClipDic = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in audioClipArray) {

            audioClipDic.Add(clip.name, clip);
            Debug.Log(clip.name);

        }

        narratorSource = GetComponent<AudioSource>();
    }


    public void PlayClip(string clipName)
    {

        narratorSource.PlayOneShot(audioClipDic[clipName], 1f);

    }
}