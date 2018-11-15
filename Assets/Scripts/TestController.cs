using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestController : MonoBehaviour {
    public BiomeManager Manager;
    public Camera Camera;

    void Start()
    {
        ConfigHandler c = new ConfigHandler();
        c["a"] = 1;
        c["b"] = Camera;
        c.Save();
    }
    
	void Update () {
		/*if (Input.GetMouseButtonUp(0))
        {
            RaycastHit hit;
            Ray ray = Camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                BiomeController biome = hit.transform.parent.GetComponent<BiomeController>();
                biome.RemoveBlock(hit.transform.name);
            }
        }*/
	}
}
