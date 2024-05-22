using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TennisBatScript : MonoBehaviour
{
    private Timer timer;
    private new Animation animation;
    private Rigidbody rb;
    private GameObject ballObject;
    private GameObject playerObject;
    private Rigidbody ballRigidBody;

    void Start()
    {
        ballObject = GameObject.Find("Ball");
        ballRigidBody = ballObject.GetComponent<Rigidbody>();

        playerObject = GameObject.Find("Player");

        rb = GetComponent<Rigidbody>();
        timer = GetComponent<Timer>();
        animation = GetComponent<Animation>();
    }

    void Update()
    {
        Attack();
    }

    private void Attack()
    {
        if (Input.GetButtonDown("Fire1") && GameStatics.canAttack)
        {
            GameStatics.canAttack = false;

            Vector3 distance = transform.position - ballObject.transform.position;

            float verticalMoveForce = Input.GetAxis("Vertical");
            float horizontalMoveForce = Input.GetAxis("Horizontal");

            RaycastHit hit;
            float distanceToGround = 0f;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                distanceToGround = hit.distance - 1f;
            }

            if ((distance.magnitude <= 2f || distance.y <= 3f) && distance.x < .5f)
            {
                float shootForce = (3f - distance.x) / 3 + (Mathf.Abs(verticalMoveForce) / 2);
                float shootHeight = (3f - distance.y) / 5 - distanceToGround;

                print(shootForce + "  " + shootHeight);

                ballRigidBody.AddForce(new Vector3(shootForce, shootHeight, 0f), ForceMode.Impulse);
            }

            timer.StartTimer(.09f, CanAttack);
            PlayAnimation();
        }
    }

    private void PlayAnimation()
    {
        animation.Play("TennisBatAnimationRight");
    }

    public void CanAttack()
    {
        GameStatics.canAttack = true;
    }
}
