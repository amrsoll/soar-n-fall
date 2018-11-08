using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {
    public BiomeManager Manager;
    public Transform Camera;

	void Update () {
		if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.position, Camera.TransformDirection(Vector3.forward), out hit))
            {
                Debug.DrawRay(Camera.position, Camera.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                BiomeController biome = hit.transform.parent.GetComponent<BiomeController>();
                Debug.Log(hit.transform.name);
                biome.RemoveBlock(hit.transform.name);
            }
        }
	}
}
