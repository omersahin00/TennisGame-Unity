using System.Collections;
using UnityEngine;
using TMPro;

public class PointTextScript : MonoBehaviour
{
    private TextMeshProUGUI pointText;
    private int tempPlayerScore = 0, tempAgentScore = 0;
    private GameStatus textPos;

    void Start()
    {
        pointText = GetComponent<TextMeshProUGUI>();
        if (gameObject.name.Contains("Run")) textPos = GameStatus.Run;
        else if (gameObject.name.Contains("Stoped")) textPos = GameStatus.Stoped;

        PointRender();
    }

    private void LateUpdate()
    {
        VisibilityCheck();
        PointCheck();
    }

    private void PointCheck()
    {
        if (GameStatics.playerScore != tempPlayerScore || GameStatics.agentScore != tempAgentScore)
        {
            PointRender();
        }
    }

    private void VisibilityCheck()
    {
        if (textPos == GameStatus.Run)
        {
            if (GameStatics.gameStatus == GameStatus.Stoped)
            {
                pointText.text = "";
            }
            else
            {
                PointRender();
            }
        }
    }

    private void PointRender()
    {
        tempPlayerScore = GameStatics.playerScore;
        tempAgentScore = GameStatics.agentScore;

        if (textPos == GameStatus.Run)
            pointText.text = GameStatics.playerScore + " - " + GameStatics.agentScore;
        else if (textPos == GameStatus.Stoped)
            pointText.text = "Point: " + GameStatics.playerScore + " - " + GameStatics.agentScore;
    }
}
