using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private GameObject tennisBat;
    private Animation animation;

    void Start()
    {
        tennisBat = gameObject.transform.GetChild(0).gameObject;
        animation = tennisBat.GetComponent<Animation>();
        PlayAnimation();
    }

    void Update()
    {
        Move();
        Attack();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(verticalInput, 0f, -horizontalInput) * 10f * Time.deltaTime);
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            PlayAnimation();
        }
    }


    private void PlayAnimation()
    {
        animation.Play("TennisBatAnimation");
    }
}
