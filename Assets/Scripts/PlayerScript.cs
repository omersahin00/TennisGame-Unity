using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject tennisBat;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        tennisBat = gameObject.transform.GetChild(0).gameObject;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        bool jumpInput = Input.GetButtonDown("Jump");

        if (!GameStatics.runFront && verticalInput > 0) verticalInput = 0;
        if (!GameStatics.runBack && verticalInput < 0) verticalInput = 0;
        if (!GameStatics.runRight && horizontalInput > 0) horizontalInput = 0;
        if (!GameStatics.runLeft && horizontalInput < 0) horizontalInput = 0;

        transform.Translate(new Vector3(verticalInput, 0f, -horizontalInput) * 10f * Time.deltaTime);
        if (GameStatics.canJump && jumpInput)
        {
            GameStatics.canJump = false;
            rb.AddForce(new Vector3(0f, 300f, 0f), ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            GameStatics.canJump = true;
        }
    }
}
