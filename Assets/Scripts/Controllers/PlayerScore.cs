using UnityEngine;

public class PlayerScore : MonoBehaviour {

	public int Score { get { return score; } }

    int score;

    public void Add(int amount)
    {
        score += amount;
    }

    public void Subtract(int amount)
    {
        score -= amount;
    }

    public void Reset()
    {
        score = 0;
    }

    void Awake()
    {
        Reset();
    }
}
