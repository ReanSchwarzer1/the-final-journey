using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NarrativeInteraction : MonoBehaviour
{

    private int _gameState = 0;

    [SerializeField]private TextMeshProUGUI _playerChoiceText1, _playerChoiceText2;
    [SerializeField]private GameObject _choiceButtonUGUI;
    [SerializeField]private TextMeshProUGUI _narrativeText, _loreText;

    [SerializeField] private Button _playerChoice1, _playerChoice2;
    public string[] sentences;
    public int _angerCnt; //emotion counters
    public int _happyCnt; //currently thinking that the emotion counter can go through
                          //every choice in the every game state, and when the player
                          //chooses one choice, the pointer counts +1 and so on
                          // by using a foreach loop in each sentence
                          // public int _IDKCount;


    // Start is called before the first frame update
    void Start()
    {
        StateShow();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerTextInputHandler();
    }

    void StateShow()
    {
        StateDictionary();
    }

    void StateDictionary()
    {
        Dictionary<int, (bool, string, string, string)> _playerGameStateData = new Dictionary<int, (bool, string, string, string)>()
        {
            {0, (true, "Hello team, how are y'all?", "[1] Pretty good", "[2] Meh")},
            {1, (true, "Oh I see, awesome then. Ready for 603?", "[1] Yep", "[2] Nope")},
            {2, (false, "That's kinda sad, but I empathize", "", "")},
            {3, (false, "LESSSSSS GOOOOOOO!", "", "")}
        };

        (bool _disableButtons, string _story, string _storyChoice1, string _storyChoice2) = _playerGameStateData[_gameState];
        _narrativeText.text = _story;
        _playerChoiceText1.text = _storyChoice1;
        _playerChoiceText2.text = _storyChoice2;
        _choiceButtonUGUI.SetActive(_disableButtons);
    }


    public void ChoiceInputHandler(int playerchoice) //player choices change the gamestates accordingly
    {
        switch (_gameState)
        {
            case 0:
                switch (playerchoice)
                {
                    case 1:
                        _gameState = 1;
                        Debug.Log("Typed 1 on Keyboard");
                        Debug.Log("Clicked button 1");

                        break;
                    case 2:
                        _gameState = 2;
                        Debug.Log("Typed 2 on Keyboard");
                        Debug.Log("Clicked button 2");

                        break;
                }

                break;

            case 1:
                switch (playerchoice)
                {
                    case 1:
                        _gameState = 3;
                        Debug.Log("Typed 1 on Keyboard");
                        Debug.Log("Clicked button 1");

                        break;
                    case 2:
                        _gameState = 2;
                        Debug.Log("Typed 2 on Keyboard");
                        Debug.Log("Clicked button 2");

                        break;
                }

                break;

            case 2:

                break;
        }
        StateShow();
    }



    void PlayerTextInputHandler()
    {
        KeypadNumInputHandler();
        AlphaNumInputHandler();


    }

   

    /*
   void ButtonInputHandler()
   {
       if (_playerChoice1.onClick = true)
       {
           ChoiceHandler(1);
       }

       else if (_playerChoice2.onClick = true)
       {
           ChoiceHandler(2);
       }
   }

*/

    void KeypadNumInputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChoiceInputHandler(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChoiceInputHandler(2);
        }
    }

    void AlphaNumInputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChoiceInputHandler(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChoiceInputHandler(2);
        }
    }



}
