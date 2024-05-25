using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject ball;
    private Rigidbody ballRigidBody;

    void Start()
    {
        ball.transform.position = GameStatics.ballPosition;
        //ball.transform.position = GameStatics.aiBallPosition;
        ballRigidBody = ball.GetComponent<Rigidbody>();
        GameStatics.gameStatus = GameStatus.Run;
    }

    void Update()
    {
        GameStatusController();
        ReStartGame();
    }

    private void GameStatusController()
    {
        if (GameStatics.gameStatus == GameStatus.Finished)
        {
            GameStatics.gameStatus = GameStatus.Paused;

            if (GameStatics.ballSurfaceItHit == BallSurfaceItHit.TennisNet
                || GameStatics.ballSurfaceItHit == BallSurfaceItHit.OtherPlane
                || GameStatics.ballSurfaceItHit == BallSurfaceItHit.Wall)
            {
                if (GameStatics.lastShooter == LastShooter.Player)
                {
                    GameStatics.agentScore++;
                    GameStatics.lastWinner = LastWinner.Agent;
                }
                else if (GameStatics.lastShooter == LastShooter.Agent)
                {
                    GameStatics.playerScore++;
                    GameStatics.lastWinner = LastWinner.Player;
                }
            }
            else if (GameStatics.ballSurfaceItHit == BallSurfaceItHit.Out)
            {
                print("BURAYA GİRDİ-1");

                if (GameStatics.ballJumped)
                {
                    if (GameStatics.lastShooter == LastShooter.Player)
                    {
                        GameStatics.playerScore++;
                        GameStatics.lastWinner = LastWinner.Player;
                    }
                    else if (GameStatics.lastShooter == LastShooter.Agent)
                    {
                        GameStatics.agentScore++;
                        GameStatics.lastWinner = LastWinner.Agent;
                    }
                }
                else
                {
                    if (GameStatics.lastShooter == LastShooter.Player)
                    {
                        GameStatics.agentScore++;
                        GameStatics.lastWinner = LastWinner.Agent;
                    }
                    else if (GameStatics.lastShooter == LastShooter.Agent)
                    {
                        GameStatics.playerScore++;
                        GameStatics.lastWinner = LastWinner.Player;
                    }
                }
                
            }
        }
    }

    private void ReStartGame()
    {
        if (GameStatics.gameStatus == GameStatus.Paused)
        {
            if (Input.GetButtonDown("Start"))
            {
                ballRigidBody.velocity = Vector3.zero;

                if (GameStatics.lastWinner == LastWinner.Player)
                {
                    ball.transform.position = GameStatics.ballPosition;
                }
                else if (GameStatics.lastWinner == LastWinner.Agent)
                {
                    ball.transform.position = GameStatics.aiBallPosition;
                }

                GameStatics.lastShooter = LastShooter.Null;
                GameStatics.lastWinner = LastWinner.Null;
                GameStatics.ballSurfaceItHit = BallSurfaceItHit.Null;

                GameStatics.gameStatus = GameStatus.Run;
            }
        }
    }
}
