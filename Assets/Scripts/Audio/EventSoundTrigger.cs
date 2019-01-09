using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventSoundTrigger : MonoBehaviour
{
    //public AudioClip narratorSound;
    AudioClip clip2;
    public AudioSource EventSoundSource;
    AudioClip[] audioClipArray;
    Dictionary<string, AudioClip> audioClipDict;


    // Use this for initialization 
    void Start() {

        bool soundplayed = false;
        audioClipArray = Resources.LoadAll<AudioClip>("Audio/EventSounds");
        audioClipDict = new Dictionary<string, AudioClip>();

        foreach (AudioClip clip in audioClipArray) {

            audioClipDict.Add(clip.name, clip);

        }
    }

    void Update() {

        if (PauseMenu.GameIsPaused) {

            EventSoundSource.Pause();
        
        }

        else {

            EventSoundSource.Play();
        
        }
    
    }

    public void PlayClip(string clipName) {

        EventSoundSource.PlayOneShot(audioClipDict[clipName]);

    }

}