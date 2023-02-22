using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NarrativeInteraction : MonoBehaviour
{

    private int _gameState = 0;

    [SerializeField] private TextMeshProUGUI _narrativeText;
    [SerializeField] private TextMeshProUGUI _playerChoiceText;

    public string[] sentences;
    public int _angerCnt; //emotion counters
    public int _happyCnt;
    // public int _IDKCount;


    //[SerializeField] private Button _playerChoice1, _playerChoice2, _playerChoice3;

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
                _playerChoiceText.text = "1. Pretty good\n2. Meh";
                break;
            case 1:
                _narrativeText.text = "Oh I see, awesome then. Ready for 603?";
                _playerChoiceText.text = "1. Yep\n2. Nope";
                break;
            case 2:
                _narrativeText.text = "That's kinda sad, but I empathize";
                _playerChoiceText.text = "";
                break;
            case 3:
                _narrativeText.text = "LESSSSSS GOOOOOOO!";
                _playerChoiceText.text = "";
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
    }

    void StateShow()
    {
        StatesList();
    }

    // Handles a player choice
    public void ChoiceHandler(int choice)
    {
        switch (_gameState)
        {
            case 0:
                if (choice == 1)
                {
                    _gameState = 1;
                    Debug.Log("Typed 1 on Keyboard");
                }
                else if (choice == 2)
                {
                    _gameState = 2;
                    Debug.Log("Typed 2 on Keyboard");
                }
                break;
            case 1:
                if (choice == 1)
                {
                    _gameState = 3;
                    Debug.Log("Typed 1 on Keyboard");
                }
                else if (choice == 2)
                {
                    _gameState = 2;
                    Debug.Log("Typed 2 on Keyboard");
                }
                break;
            case 3:
                break;
        }
        StateShow();
    }

}


