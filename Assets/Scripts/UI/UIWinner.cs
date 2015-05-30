using UnityEngine;
using UnityEngine.UI;

public class UIWinner : MonoBehaviour {

    public GameManager gameManager;

    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        if (gameManager == null)
        {
            throw new MissingReferenceException();
        }
    }

    void Update()
    {
        text.text = "";
        if (gameManager.gamestate == GameManager.Gamestate.WIN)
        {
            text.text = gameManager.winner.ToUpper() + " WINS!";
        }
    }
}
