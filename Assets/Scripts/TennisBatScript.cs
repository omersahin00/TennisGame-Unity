using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TennisBatScript : MonoBehaviour
{
    private Timer timer;
    private new Animation animation;
    private GameObject ballObject;
    private Rigidbody ballRigidBody;

    void Start()
    {
        ballObject = GameObject.Find("Ball");
        ballRigidBody = ballObject.GetComponent<Rigidbody>();

        timer = GetComponent<Timer>();
        animation = GetComponent<Animation>();
    }

    void Update()
    {
        AttackManager();
    }

    private void AttackManager()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (GameStatics.forceAttack)
            {
                ForceAttack();
            }
            else
            {
                Attack();
            }
        }
    }

    private void Attack()
    {
        if (GameStatics.canAttack)
        {
            GameStatics.canAttack = false;

            float horizontalMoveForce = Input.GetAxis("Horizontal");

            RaycastHit hit;
            float distanceToGround = 0f;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                distanceToGround = hit.distance - 1f;
            }

            float netDistance = Mathf.Pow(Mathf.Abs(transform.position.x), 2) / 100;

            timer.StartTimer(.1f, CanAttack);

            Vector3 distance = transform.position - ballObject.transform.position;

            float shootForce = netDistance / 1.5f * (distanceToGround / 3f);
            float shootHeight = - ((3f - netDistance) / 2f - (distanceToGround / 2f)) / 4f;
            float shootAngle = (distance.z / 4f) + horizontalMoveForce / 2f;

            if (shootForce > 2f) shootForce = 2f;

            if ((distance.magnitude <= 3f || (distance.magnitude <= 3f && distance.y <= 4f)) && distance.x < 1f)
            {
                ballRigidBody.velocity *= -1;
                print("player normal: " + shootForce + "  --  " + shootHeight);
                ballRigidBody.AddForce(new Vector3(shootForce, shootHeight, shootAngle), ForceMode.Impulse);
            }

            PlayAnimation();
        }   
    }

    private void ForceAttack()
    {
        if (GameStatics.canAttack)
        {
            GameStatics.gameStatus = GameStatus.Run;
            GameStatics.canAttack = false;
            GameStatics.forceAttack = false;

            float verticalMoveForce = Input.GetAxis("Vertical");
            float horizontalMoveForce = Input.GetAxis("Horizontal");

            RaycastHit hit;
            float distanceToGround = 0f;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                distanceToGround = hit.distance - 1f;
            }

            float netDistance = Mathf.Pow(Mathf.Abs(transform.position.x), 2) / 100;

            Vector3 distance = transform.position - ballObject.transform.position;

            if ((distance.magnitude <= 3f || (distance.magnitude <= 3f && distance.y <= 4f)) && distance.x < 1f)
            {
                float shootForce = ((3f - distance.x) / 4) + (Mathf.Abs(verticalMoveForce) / 3) + (netDistance / 4);
                float shootHeight = (((3f - distance.y) / 6) - (distanceToGround / 2) + (netDistance / 3)) / 2;
                float shootAngle = (distance.z / 4) + horizontalMoveForce / 2;

                if (shootForce > 2f) shootForce = 2f;

                print(shootForce + "  -1-  " + shootHeight);

                ballRigidBody.velocity = Vector3.zero;
                ballRigidBody.AddForce(new Vector3(shootForce, shootHeight, shootAngle), ForceMode.Impulse);
            }
            else
            {
                GameStatics.gameStatus = GameStatus.NotStart;
            }

            timer.StartTimer(.1f, CanAttack);

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
