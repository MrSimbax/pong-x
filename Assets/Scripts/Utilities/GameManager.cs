using UnityEngine;

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
    public string resetButton;
    public string exitButton;
    public string fullscreenButton;

    public PlayerController playerLeft;
    public PlayerController playerRight;
    public BallController ball;

    [Range(1.0f, 98.0f)] public int winScore;

    private string _winner;

    public string winner
    {
        get
        {
            return _winner;
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
            SwitchFullScreen();

        if (gamestate == Gamestate.NOT_STARTED)
            WaitForStartGame();

        if (Input.GetButtonDown(resetButton))
            ResetGame();

        if (Input.GetButtonDown(exitButton))
            ExitGame();
    }

    void WaitForStartGame()
    {
        if (Input.GetButtonDown(startButton))
        {
            StartGame();
            gamestate = Gamestate.PLAYING;
        }
    }

    void StartGame()
    {
        ball.InitVelocity();
        BallController.OnReachedEnd += Goal;
    }

    void ResetGame()
    {
        ball.Reset();
        playerLeft.Reset();
        playerRight.Reset();
        gamestate = Gamestate.NOT_STARTED;
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
            playerRight.score += 1;
        }
        else
        {
            playerLeft.score += 1;
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
