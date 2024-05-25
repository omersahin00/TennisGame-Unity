using Unity;
using UnityEngine;

public static class GameStatics
{
    public static GameStatus gameStatus = GameStatus.NotStart;

    public static bool runFront = true;
    public static bool runBack = true;
    public static bool runRight = true;
    public static bool runLeft = true;
    public static bool canJump = true;
    public static bool canAttack = true;

    public static bool forceAttack = true;

    public static bool aiRunFront = true;
    public static bool aiRunBack = true;
    public static bool aiRunRight = true;
    public static bool aiRunLeft = true;
    public static bool aiCanJump = true;
    public static bool aiCanAttack = true;

    public static Vector3 ballPosition = new Vector3(-13f, 2f, -2f);
    public static Vector3 aiBallPosition = new Vector3(6f, 5f, 0f);
    public static LastShooter lastShooter = LastShooter.Null;
    public static LastWinner lastWinner = LastWinner.Null;
    public static BallSurfaceItHit ballSurfaceItHit = BallSurfaceItHit.Null;

    public static int playerScore = 0;
    public static int agentScore = 0;

    public static bool ballJumped = false;
}
