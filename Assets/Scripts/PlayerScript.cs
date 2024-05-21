using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameObject tennisBat;

    void Start()
    {
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
        transform.Translate(new Vector3(verticalInput, 0f, -horizontalInput) * 10f * Time.deltaTime);
    }
}
