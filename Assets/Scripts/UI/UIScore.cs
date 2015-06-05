using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class UIScore : MonoBehaviour
{

    public PlayerScore score;

    Text text;
	
    void Start()
    {
        text = GetComponent<Text>();
        if (score == null)
        {
            throw new MissingReferenceException();
        }
    }

    void Update()
    {
        text.text = "" + score.Score;
    }
}
