using UnityEngine;
using UnityEngine.UI;

[RequireComponent (typeof(Text))]
public class UIScore : MonoBehaviour
{

    public PlayerController player;

    private Text text;
	
    void Start()
    {
        text = GetComponent<Text>();
        if (player == null)
        {
            throw new MissingReferenceException();
        }
    }

    void Update()
    {
        text.text = "" + player.score;
    }
}
