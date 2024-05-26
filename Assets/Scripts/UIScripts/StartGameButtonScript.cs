using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartGameButtonScript : MonoBehaviour
{
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    private void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            StartGame();
        }
    }
}
