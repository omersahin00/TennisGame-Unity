using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtonScript : MonoBehaviour
{
    private Button button;
    private GameController gameController;

    void Start()
    {
        button = GetComponent<Button>();
        gameController = FindAnyObjectByType<GameController>();
        button.onClick.AddListener(gameController.ContinueGame);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            gameController.ContinueGame();
        }
    }
}
