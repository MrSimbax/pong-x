using UnityEngine;

[RequireComponent(typeof(AudioSource))]
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
    
    public AudioClip soundWin;

    [Range(1.0f, 98.0f)] public int winScore;

    string _winner;
    public string Winner
    {
        get
        {
            return _winner;
        }
    }
    
    private AudioSource audioSource;

    void Awake()
    {
        gamestate = Gamestate.NOT_STARTED;
        if (playerLeft == null ||
            playerRight == null ||
            ball == null ||
            soundWin == null)
        {
            throw new MissingReferenceException();
        }
        audioSource = GetComponent<AudioSource>();
    }

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

    void Goal()
    {
        BallController.OnReachedEnd -= Goal;
        
        audioSource.clip = soundWin;
        audioSource.Play();

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

    void StartGame()
    {
        ball.InitVelocity();
        BallController.OnReachedEnd += Goal;
    }

    public void PauseGame()
    {
        Time.timeScale = 0.0f;
        gamestate = Gamestate.PAUSED;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;
        gamestate = Gamestate.PLAYING;
    }

    public void ResetGame()
    {
        ResumeGame();
        ball.Reset();
        playerLeft.Reset();
        playerRight.Reset();
        gamestate = Gamestate.NOT_STARTED;
    }

    void EndGame()
    {
        PauseGame();
        _winner = (playerLeft.score > playerRight.score) ? "Left player" : "Right player";
        gamestate = Gamestate.WIN;
    }

    void ExitGame()
    {
        Application.Quit();
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
