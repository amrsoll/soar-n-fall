using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerAnimation : MonoBehaviour {

	Animator anim;
	Collider m_Collider;
 
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("Active");
            m_Collider = GetComponent<Collider>();
            m_Collider.enabled = true;
        }
    }
}
