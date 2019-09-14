using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InactiveUser : MonoBehaviour {

	// Use this for initialization
    public int time = 0;

    //Use fixed update beacuase its called every fixed framerate frame
    void FixedUpdate()
    {

        if (!Input.anyKey)
        {

            //Starts counting when no button is being pressed
            time = time + 1;
        }
        else
        {

            // If a button is being pressed restart counter to Zero
            time = 0;
        }

        //Now after 100 frames of nothing being pressed it will do activate this if statement
        if (time == 1500)
        {
            Debug.Log("1500 frames passed with no input");

            //Now you could set time too zero so this happens every 100 frames
            SceneManager.LoadScene(0);
        }

    }
}
