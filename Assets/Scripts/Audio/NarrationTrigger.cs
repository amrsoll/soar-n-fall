using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrationTrigger : MonoBehaviour
{
    //public AudioClip narratorSound;
    AudioClip clip2;
    public AudioSource narratorSource;
    AudioClip[] audioClipArray;
    Dictionary<string, AudioClip> audioClipDict;


    // Use this for initialization 
    void Start()
    {
        bool soundplayed = false;

        audioClipArray = Resources.LoadAll<AudioClip>("Audio/Narration");

        audioClipDict = new Dictionary<string, AudioClip>();
        foreach (AudioClip clip in audioClipArray)
        {

            audioClipDict.Add(clip.name, clip);

        }
        
        if (!soundplayed)
        {
            PlayClip("startNarration");
            soundplayed = true;
        }
    }

    private void Update()
    {
        if (PauseMenu.GameIsPaused)
        {
            narratorSource.Pause();
        }
        else {
            narratorSource.Play();
        }
    }


    public void PlayClip(string clipName)
    {
        if (narratorSource.isPlaying) {
            narratorSource.Stop();
            narratorSource.PlayOneShot(audioClipDict[clipName]);
        }
        else {
            narratorSource.PlayOneShot(audioClipDict[clipName]);
        }

    }

}