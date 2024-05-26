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

    private void LateUpdate()
    {
        LocationController();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (GameStatics.gameStatus == GameStatus.Run)
        {
            if (collision.gameObject.CompareTag("Plane"))
            {
                GameStatics.ballSurfaceItHit = BallSurfaceItHit.Plane;
                GameStatics.forceAttack = true;
                GameStatics.ballJumped = true;

                Vector3 normal = collision.contacts[0].normal;
                rb.AddForce(normal * .4f, ForceMode.Impulse);
                return;
            }
            else if (collision.gameObject.CompareTag("OtherPlane"))
            {
                GameStatics.ballSurfaceItHit = BallSurfaceItHit.OtherPlane;
                GameStatics.gameStatus = GameStatus.Finished;
            }
            else if (collision.gameObject.CompareTag("Wall"))
            {
                GameStatics.ballSurfaceItHit = BallSurfaceItHit.Wall;
                GameStatics.gameStatus = GameStatus.Finished;
            }
            else if (collision.gameObject.CompareTag("TennisNet"))
            {
                GameStatics.ballSurfaceItHit = BallSurfaceItHit.TennisNet;
                GameStatics.gameStatus = GameStatus.Finished;
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                GameStatics.ballSurfaceItHit = BallSurfaceItHit.Out;
                GameStatics.ballJumped = true;
                GameStatics.gameStatus = GameStatus.Finished;
            }
        }
    }

    private void LocationController()
    {
        if (GameStatics.gameStatus == GameStatus.Run)
        {
            if (transform.position.x > 18 || transform.position.x < -18
                || transform.position.z > 10 || transform.position.z < -10)
            {
                GameStatics.ballSurfaceItHit = BallSurfaceItHit.Out;
                GameStatics.gameStatus = GameStatus.Finished;
            }
        }
    }
}
