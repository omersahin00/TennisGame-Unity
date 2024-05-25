using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AITennisBatScript : MonoBehaviour
{
    private Timer timer;
    private new Animation animation;
    private GameObject ballObject;
    private Rigidbody ballRigidBody;

    private void Start()
    {
        ballObject = GameObject.Find("Ball");
        ballRigidBody = ballObject.GetComponent<Rigidbody>();

        timer = GetComponent<Timer>();
        animation = GetComponent<Animation>();
    }

    public void Attack()
    {
        if (GameStatics.aiCanAttack)
        {
            GameStatics.aiCanAttack = false;
            GameStatics.lastShooter = LastShooter.Agent;
            GameStatics.ballJumped = false;

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
            float shootHeight = ((netDistance - 1f) / 2f - distanceToGround / 6f) / 8f;
            float shootAngle = distance.z / 4f;

            if (shootForce > 2f) shootForce = 2f;

            if ((distance.magnitude <= 4f || (distance.magnitude <= 4f && distance.y <= 5f)) && distance.x < 4f)
            {
                ballRigidBody.velocity *= -1;
                ballRigidBody.AddForce(new Vector3(shootForce, shootHeight, shootAngle) * -1, ForceMode.Impulse);
            }

            PlayAnimation();
        }
    }

    public void ForceAttack()
    {
        if (GameStatics.aiCanAttack)
        {
            GameStatics.gameStatus = GameStatus.Run;
            GameStatics.aiCanAttack = false;
            GameStatics.forceAttack = false;
            GameStatics.ballJumped = false;

            RaycastHit hit;
            float distanceToGround = 0f;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                distanceToGround = hit.distance - 1f;
            }

            float netDistance = Mathf.Pow(Mathf.Abs(transform.position.x), 2) / 100;

            Vector3 distance = ballObject.transform.position - transform.position;

            if ((distance.magnitude <= 4f || (distance.magnitude <= 4f && distance.y <= 5f)) && distance.x < 4f)
            {
                float shootForce = ((3f - distance.x) / 3f) + (netDistance / 3f);
                float shootHeight = (((3f - distance.y) / 5f) + (netDistance / 6f)) / 4f;
                float shootAngle = distance.z / 4f;

                if (shootForce > 2f) shootForce = 2f;

                if (GameStatics.lastShooter == LastShooter.Null)
                {
                    shootForce -= .5f;
                    shootHeight += .4f;
                }

                ballRigidBody.velocity = Vector3.zero;
                ballRigidBody.AddForce(new Vector3(shootForce, -shootHeight, shootAngle) * -1, ForceMode.Impulse);
            }
            else
            {
                GameStatics.gameStatus = GameStatus.NotStart;
            }

            GameStatics.lastShooter = LastShooter.Agent;

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
        GameStatics.aiCanAttack = true;
    }
}
