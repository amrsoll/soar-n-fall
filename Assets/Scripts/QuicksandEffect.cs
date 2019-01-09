using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class QuicksandEffect : MonoBehaviour
{
    public float slowFactor = 0.8f;
    public float fallingSpeed = 0.2f;

    private void OnTriggerStay(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            Vector3 v = rb.velocity;
            if (v.y > 0)
            {
                v.y *= slowFactor;
            }
            else
            {
                v.y = -fallingSpeed;
            }
            v.x *= slowFactor;
            v.z *= slowFactor;
            rb.velocity = v;
        }
    }
}
