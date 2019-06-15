using UnityEngine;
using BoardF;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainScript : MonoBehaviour
{
    const int size = 4;
    Game game;
    Sound sound;
	
	public Text textMoves;
	
    public void OnStart()
    {
        game.Start(1000 + System.DateTime.Now.DayOfYear);
        ShowButtons();
		sound.PlayStart();
    }

    public void OnClick()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        if ( ! game.IsGameStarted || game.Solved())
			return;
		
		string buttonName = EventSystem.current.currentSelectedGameObject.name;
		int x = int.Parse(buttonName.Substring(0, 1));
        int y = int.Parse(buttonName.Substring(1, 1));
        if( game.PressAt(x,y) > 0 )
			sound.PlayMove();
			
		ShowButtons();
		if(game.Solved())
		{
			sound.PlaySolved();
			textMoves.text = "Game finished in " + game.currentStepsCount + "steps";
		}
    }

    void Start()
    {
        game = new Game(size);
		sound = GetComponent<Sound>();
        HideButtons();
    }

    void HideButtons()
    {
        for (int x = 0; x < size; x++ )
            for (int y = 0; y < size; y++ )
                ShowDigitAt(0, x, y);

        textMoves.text = "Welcome to Game F!";
    }

    void ShowButtons()
    {
        for (int x = 0; x < size; x++)
            for (int y = 0; y < size; y++)
                ShowDigitAt(game.GetDigitalAt(x, y), x, y);

        textMoves.text = game.currentStepsCount + " steps";
    }
    
    void ShowDigitAt(int digit, int x, int y)
    {
        string buttonName = x + "" + y;
        var button = GameObject.Find(buttonName);
        var text = button.GetComponentInChildren<Text>();
        text.text = DecToHex(digit);
        button.GetComponentInChildren<Image>().color = // setVisable
            (digit > 0) ? Color.white : Color.clear;
    }
    
    string DecToHex(int digit)
    {
        if (digit == 0)
            return "";
        if (digit < 10)
            return digit.ToString();
        return ( (char)('A' + digit - 10) ).ToString();
    }
    
}
