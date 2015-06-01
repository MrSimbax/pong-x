using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UIStart : MonoBehaviour {
    
    public string text;
    public float blinkInterval;
    
    Text uiText;
    
	void Start() {
	   uiText = GetComponent<Text>();
       StartCoroutine(BlinkText());
	}
    
    void Update()
    {
        if (Input.GetButtonDown("Start"))
        {
            Application.LoadLevel("thegame");
        }
        
        if (Input.GetButtonDown("Fullscreen"))
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
        
        if (Input.GetButtonDown("Exit"))
        {
            Application.Quit();
        }
    }
	
	void HideText()
    {
        uiText.text = "";
    }
    
    void ShowText()
    {
        uiText.text = text;
    }
    
    IEnumerator BlinkText()
    {
        while (true)
        {
            HideText();
            yield return new WaitForSeconds(blinkInterval);
            ShowText();
            yield return new WaitForSeconds(blinkInterval);
        }
    }
}
