using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    public GameObject ball;
    private Rigidbody ballRigidBody;
    public GameObject pausePanel;
    public GameObject finishPanel;
    private GameStatus lastGameStatus;

    void Start()
    {
        GameStatics.ResetGame();
        pausePanel.SetActive(false);
        finishPanel.SetActive(false);
        ball.transform.position = GameStatics.ballPosition;
        ballRigidBody = ball.GetComponent<Rigidbody>();
        GameStatics.gameStatus = GameStatus.Run;
    }

    void Update()
    {
        GameStatusController();
        ReStartGame();
        PauseController();
        ComplateGame();
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
                    else
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
        if (GameStatics.gameStatus == GameStatus.Pending)
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

    private void PauseController()
    {
        if (Input.GetButtonDown("Pause") && !(GameStatics.gameStatus == GameStatus.Stoped))
        {
            PauseGame();
        }
        else if (Input.GetButtonDown("Pause") && GameStatics.gameStatus == GameStatus.Stoped)
        {
            ContinueGame();
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        lastGameStatus = GameStatics.gameStatus;
        GameStatics.gameStatus = GameStatus.Stoped;
        pausePanel.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = 1f;
        GameStatics.gameStatus = lastGameStatus;
        pausePanel.SetActive(false);
    }

    public void ComplateGame()
    {
        if (GameStatics.playerScore >= GameStatics.finishScore || GameStatics.agentScore >= GameStatics.finishScore)
        {
            finishPanel.SetActive(true);

            if (GameStatics.gameStatus == GameStatus.Pending)
                GameStatics.gameStatus = GameStatus.Complated;
        }
    }
}
