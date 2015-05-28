using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public enum Gamestate
    {
        NOT_STARTED,
        PAUSED,
        PLAYING,
        WIN
    };

    [HideInInspector]
    public Gamestate gamestate;

    public string startButton;
    public string exitButton;
    public string fullscreenButton;

    public PlayerController playerLeft;
    public PlayerController playerRight;
    public BallController ball;

    public int winScore = 10;

    private string _winner;

    public string winner
    {
        get
        {
            return winner;
        }
    }

    // Use this for initialization
    void Start()
    {
        gamestate = Gamestate.NOT_STARTED;
        if (playerLeft == null || playerRight == null || ball == null)
        {
            throw new MissingReferenceException();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(fullscreenButton))
        {
            SwitchFullScreen();
        }

        if (gamestate == Gamestate.NOT_STARTED)
        {
            if (Input.GetButtonDown(startButton))
            {
                StartGame();
                gamestate = Gamestate.PLAYING;
            }
        }

        if (Input.GetButtonDown(exitButton))
        {
            ExitGame();
        }
    }

    void StartGame()
    {
        ball.InitVelocity();
        BallController.OnReachedEnd += Goal;
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void Goal()
    {
        BallController.OnReachedEnd -= Goal;
        if (ball.transform.position.x < playerLeft.transform.position.x)
        {
            playerLeft.score += 1;
        }
        else
        {
            playerRight.score += 1;
        }
        if (playerLeft.score >= winScore || playerRight.score >= winScore)
        {
            EndGame();
            return;
        }
        ball.Reset();
        gamestate = Gamestate.NOT_STARTED;
    }

    public void PauseGame()
    {
        ball.Pause();
        playerLeft.Pause();
        playerRight.Pause();
        gamestate = Gamestate.PAUSED;
    }

    public void ResumeGame()
    {
        ball.Resume();
        playerLeft.Resume();
        playerRight.Resume();
        gamestate = Gamestate.PLAYING;
    }

    public void RestartGame()
    {
        ball.Reset();
        playerLeft.Reset();
        playerRight.Reset();
        gamestate = Gamestate.NOT_STARTED;
    }

    void EndGame()
    {
        PauseGame();
        if (playerLeft.score > playerRight.score)
        {
            _winner = "Left player";
        }
        else
        {
            _winner = "Right player";
        }
        gamestate = Gamestate.WIN;
    }

    void SwitchFullScreen()
    {
        if (Screen.fullScreen)
        {
            Screen.SetResolution(1280, 720, false);
        }
        else
        {
            Screen.fullScreen = true;
        }
    }
}
