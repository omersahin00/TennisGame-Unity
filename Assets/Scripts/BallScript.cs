using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(new Vector3(.5f, .5f, 0f), ForceMode.Impulse);
    }

    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal;
        rb.AddForce(normal * .4f, ForceMode.Impulse);
    }

}
