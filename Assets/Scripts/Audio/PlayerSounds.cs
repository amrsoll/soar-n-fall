using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour {

    public AudioClip jumpSound;
    public AudioClip jumpSound2;
    public float distToGround = 0.35f;

    private AudioSource source;

    void Start () {

        source = GetComponent<AudioSource>();

    }
	
    void Update () {
    
        // If the player is on the ground and at the same time walking, play the footsteps sound
        if ( isGrounded() && isWalking() ) {
            if ( !source.isPlaying ) {
                source.pitch = Random.Range(1.1f, 1.3f);
                source.volume = Random.Range(0.010f, 0.025f);
                source.Play();
            }
        }

        // If the player is on the ground and jumping, play jumpsound1
        if ( isGrounded() && isJumping() ) {
        
            source.PlayOneShot(jumpSound);

        }

        // If the player is NOT on the ground and jumping, play jumpsound2
        if ( !isGrounded() && isJumping()) {

            source.PlayOneShot(jumpSound2);

        }


    }

    private bool isGrounded() {

        return Physics.Raycast(transform.position, Vector3.down, distToGround + 0.1f);

    }

    private bool isWalking() {

        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        bool walk = Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || v > 0f || h > 0f;
        return walk
            ? true
            : false;

    }

    private bool isJumping() {

        bool jump = Input.GetKeyDown(KeyCode.JoystickButton0) || Input.GetKeyDown(KeyCode.Space);
        return jump
            ? true
            : false;

    }

}
