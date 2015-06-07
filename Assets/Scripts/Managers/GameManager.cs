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
    
    [HideInInspector] public Gamestate gamestate;

    public string startButton;
    public string resetButton;
    public string exitButton;
    public string fullscreenButton;

    public Referee referee;

    void Awake()
    {
        if (referee == null)
        {
            throw new MissingReferenceException();
        }
    }

    void Start()
    {
        referee.Reset();
        referee.OnEnd += EndGame;
        referee.OnGoal += DoAfterGoal;
        gamestate = Gamestate.NOT_STARTED;
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

    void StartGame()
    {
        referee.StartRound();
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
        Time.timeScale = 1.0f;
        referee.Reset();
        gamestate = Gamestate.NOT_STARTED;
    }

    void DoAfterGoal()
    {
        gamestate = Gamestate.NOT_STARTED;
    }

    void EndGame()
    {
        PauseGame();
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
