using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NarrativeInteraction : MonoBehaviour
{

    private int _gameState = 0;

    [SerializeField] private TextMeshProUGUI _narrativeText;
    [SerializeField] private TextMeshProUGUI _playerChoiceText1, _playerChoiceText2;

    public string[] sentences;
    public int _angerCnt; //emotion counters
    public int _happyCnt; //currently thinking that the emotion counter can go through
                          //every choice in the every game state, and when the player
                          //chooses one choice, the pointer counts +1 and so on
                          // by using a foreach loop in each sentence
    // public int _IDKCount;


    [SerializeField] private Button _playerChoice1, _playerChoice2;

    // Start is called before the first frame update
    void Start()
    {
        StateShow();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerInputHandler();
    }

    void StatesList()
    {
        switch (_gameState)
        {
            case 0:
                _narrativeText.text = "Hello team, how are y'all??";
                _playerChoiceText1.text = "1. Pretty good";
                _playerChoiceText2.text = "2. Meh";
                break;
            case 1:
                _narrativeText.text = "Oh I see, awesome then. Ready for 603?";
                _playerChoiceText1.text = "1. Yep";
                _playerChoiceText2.text = "2. Nope";
                break;
            case 2:
                _narrativeText.text = "That's kinda sad, but I empathize";
                _playerChoiceText1.text = "";
                _playerChoiceText2.text = "";
                break;
            case 3:
                _narrativeText.text = "LESSSSSS GOOOOOOO!";
                _playerChoiceText1.text = "";
                _playerChoiceText2.text = "";
                break;
        }
    }

    void PlayerInputHandler()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ChoiceHandler(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ChoiceHandler(2);
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChoiceHandler(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChoiceHandler(2);
        }
    }

    void StateShow()
    {
        StatesList();
    }

    public void ChoiceHandler(int playerChoice) //player choices changes the gamestates accordingly
    {
        switch (_gameState)
        {
            case 0:
                if (playerChoice == 1)
                {
                    _gameState = 1;
                    Debug.Log("Typed 1 on Keyboard");
                    Debug.Log("Clicked button 1");
                }
                else if (playerChoice == 2)
                {
                    _gameState = 2;
                    Debug.Log("Typed 2 on Keyboard");
                    Debug.Log("Clicked button 2");
                }
                break;
            case 1:
                if (playerChoice == 1)
                {
                    _gameState = 3;
                    Debug.Log("Typed 1 on Keyboard");
                    Debug.Log("Clicked button 1");
                }
                else if (playerChoice == 2)
                {
                    _gameState = 2;
                    Debug.Log("Typed 2 on Keyboard");
                    Debug.Log("Clicked button 2");
                }
                break;
            case 3:
                break;
        }
        StateShow();
    }

}


