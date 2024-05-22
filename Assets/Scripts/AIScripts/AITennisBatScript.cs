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
            Debug.LogError("ai normal attack");

            GameStatics.aiCanAttack = false;
            ballRigidBody.velocity *= -1;
            timer.StartTimer(.1f, CanAttack);

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

            Debug.LogError("ai force attack");

            ballRigidBody.velocity = Vector3.zero;

            float verticalMoveForce = Input.GetAxis("Vertical");

            RaycastHit hit;
            float distanceToGround = 0f;
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                distanceToGround = hit.distance - 1f;
            }

            float netDistance = Mathf.Pow(Mathf.Abs(transform.position.x), 2) / 100;

            Vector3 distance = ballObject.transform.position - transform.position;

            if ((distance.magnitude <= 4f || (distance.magnitude <= 4f && distance.y <= 5f)) && distance.x < 1f)
            {
                float shootForce = ((3f - distance.x) / 3) + (netDistance / 5);
                float shootHeight = (((3f - distance.y) / 5) + (netDistance / 7)) / 2;

                print(shootForce + "  -2-  " + shootHeight);

                ballRigidBody.AddForce(new Vector3(shootForce, -shootHeight, 0f) * -1, ForceMode.Impulse);
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
        GameStatics.aiCanAttack = true;
    }
}
