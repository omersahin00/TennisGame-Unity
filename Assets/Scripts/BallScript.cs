using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            GameStatics.forceAttack = true;
            Vector3 normal = collision.contacts[0].normal;
            rb.AddForce(normal * .4f, ForceMode.Impulse);
        }
    }
}
