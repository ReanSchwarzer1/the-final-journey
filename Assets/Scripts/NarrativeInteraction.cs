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

    void StateList()
    {
        switch (_gameState)
        {
            case 0:
                _narrativeText.text = "Hello team, how are y'all?";
                _playerChoiceText1.text = "1. Pretty good";
                _playerChoiceText2.text = "2. Meh";
                _choiceButtonUGUI.SetActive(true);

                break;
            case 1:
                _narrativeText.text = "Oh I see, awesome then. Ready for 603?";
                _playerChoiceText1.text = "1. Yep";
                _playerChoiceText2.text = "2. Nope";
                _choiceButtonUGUI.SetActive(true);

                break;
            case 2:
                _narrativeText.text = "That's kinda sad, but I empathize";
                _playerChoiceText1.text = "";
                _playerChoiceText2.text = "";
                _choiceButtonUGUI.SetActive(false);

                break;
            case 3:
                _narrativeText.text = "LESSSSSS GOOOOOOO!";
                _playerChoiceText1.text = "";
                _playerChoiceText2.text = "";
                _choiceButtonUGUI.SetActive(false);

                break;
        }
    }

    void PlayerTextInputHandler()
    {
        KeypadNumInputHandler();
        AlphaNumInputHandler();


    }

    public void ChoiceInputHandler(int playerchoice) //player choices change the gamestates accordingly
    {
        switch (_gameState)
        {
            case 0:
                if (playerchoice == 1)
                {
                    _gameState = 1;
                    Debug.Log("Typed 1 on Keyboard");
                    Debug.Log("Clicked button 1");
                }
                else if (playerchoice == 2)
                {
                    _gameState = 2;
                    Debug.Log("Typed 2 on Keyboard");
                    Debug.Log("Clicked button 2");
                }

                break;

            case 1:
                if (playerchoice == 1)
                {
                    _gameState = 3;
                    Debug.Log("Typed 1 on Keyboard");
                    Debug.Log("Clicked button 1");
                }
                else if (playerchoice == 2)
                {
                    _gameState = 2;
                    Debug.Log("Typed 2 on Keyboard");
                    Debug.Log("Clicked button 2");
                }

                break;

            case 2:

                break;
        }
        StateShow();
    }

    void StateShow()
    {
        StateList();
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
