using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximitySounds : MonoBehaviour {

    //public AudioClip soundToPlay;
    public AudioSource audioSource;

    void OnTriggerEnter(Collider player) {
        if (player.gameObject.tag == "Player") {
            audioSource.Play();
        }
    }

    void OnTriggerExit(Collider player) {
        if (player.gameObject.tag == "Player") {
            audioSource.Stop();
        }
    }
}
