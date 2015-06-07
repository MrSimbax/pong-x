using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Referee : MonoBehaviour {

	public PlayerController playerLeft;
    public PlayerController playerRight;
    public BallController ball;
    public AudioClip soundGoal;
    [Range(1.0f, 98.0f)] public int winScore;
    public PlayerScore scoreLeft;
    public PlayerScore scoreRight;

    private string winner;
    public string Winner { get { return winner; } }

    public delegate void VoidTrigger();
    public event VoidTrigger OnEnd;
    public event VoidTrigger OnGoal;

    AudioSource audioSource;

    public void StartRound()
    {
        ball.InitVelocity();
        ball.OnReachedEnd += Goal;
    }

    public void Reset()
    {
        scoreLeft.Reset();
        scoreRight.Reset();
        winner = "";
        ball.Reset();
        playerLeft.Reset();
        playerRight.Reset();
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (playerLeft == null ||
            playerRight == null ||
            ball == null ||
            scoreLeft == null ||
            scoreRight == null)
        {
            throw new MissingReferenceException();
        }
    }

    void Start()
    {
        Reset();
    }

    void Goal()
    {
        ball.OnReachedEnd -= Goal;
        
        audioSource.clip = soundGoal;
        audioSource.Play();

        if (ball.transform.position.x < playerLeft.transform.position.x)
        {
            scoreRight.Add(1);
        }
        else
        {
            scoreLeft.Add(1);
        }

        if (scoreLeft.Score >= winScore || scoreRight.Score >= winScore)
        {
            winner = ChooseWinner();
            if (OnEnd != null) OnEnd();
            return;
        }

        ball.Reset();
        if(OnGoal != null) OnGoal();
    }

    string ChooseWinner()
    {
        return (scoreLeft.Score > scoreRight.Score) ? "Left player" : "Right player";
    }
}
