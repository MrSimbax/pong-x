using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{

    public enum Gamestate
    {
        NOT_STARTED,
        PLAYING,
        WIN
    };

    [HideInInspector]
    public Gamestate gamestate;

    public string startButton;

    public PlayerController playerLeft;
    public PlayerController playerRight;
    public BallController ball;

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
        if (gamestate == Gamestate.NOT_STARTED)
        {
            if (Input.GetButtonDown(startButton))
            {
                StartGame();
                gamestate = Gamestate.PLAYING;
            }
        }
    }

    void StartGame()
    {
        ball.InitVelocity();
        BallController.OnReachedEnd += Goal;
    }

    void Goal()
    {
        if (ball.transform.position.x < playerLeft.transform.position.x)
        {
            playerLeft.score += 1;
        }
        else
        {
            playerRight.score += 1;
        }
        if (playerLeft.score >= 10 || playerRight.score >= 10)
        {
            gamestate = Gamestate.WIN;
        }
        ball.Reset();
        gamestate = Gamestate.NOT_STARTED;
        BallController.OnReachedEnd -= Goal;
    }
}
