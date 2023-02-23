using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class NarrativeInteraction : MonoBehaviour
{

    private int _gameState = 0;

    [SerializeField]private TextMeshProUGUI _playerChoiceText1, _playerChoiceText2;
    [SerializeField]private GameObject _choiceButton1UGUI, _choiceButton2UGUI;
    [SerializeField]private TextMeshProUGUI _narrativeText, _loreText;
    //public string _loreT;

    [SerializeField] private Button _playerChoice1, _playerChoice2;
    public string[] sentences;
    public int _angerCnt; //emotion counters
    public int _happyCnt; //currently thinking that the emotion counter can go through
                          //every choice in the every game state, and when the player
                          //chooses one choice, the pointer counts +1 and so on
                          // by using a foreach loop in each sentence
                          // public int _IDKCount;


    public float sentenceRate;
    public float sentenceDelay;
    private float timer;
    private int index = 0;
    public GameObject textObject;


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
        NarrativeTimer();

        Dictionary<int, (bool, bool, string, string, string, string)> _playerGameStateData = new Dictionary<int, (bool, bool, string, string, string, string)>()
        {
            {0, (true, false, "","Activating companion protocol. ", "[1] Continue", "")},
            {1, (true, false, "","Life support at 89% capacity. ", "[1] Continue", "")},
            {2, (true, false, "","In-Cryo communication online. ", "[1] Continue", "")},
            {3, (true, false, "","Hello Q, can you hear me?. ", "[1] Yes? Yes, but I cannot see you.", "")},
            {4, (true, false, "","That’s alright, can you tell me how you are feeling?. ", "[1] I feel a little strange, not quite here.", "")},
            {5, (true, false, "","I assure you, you are fine. You are currently in cryogenic sleep. Here are your vitals:. ", "[1] I see, who are you?", "[2] ")}
        };

        (bool _disableButtons1, bool _disableButtons2, string _lore, string _story, string _storyChoice1, string _storyChoice2) = _playerGameStateData[_gameState];
        _narrativeText.text = _story;
        _loreText.text = _lore;
        _playerChoiceText1.text = _storyChoice1;
        _playerChoiceText2.text = _storyChoice2;
        _choiceButton1UGUI.SetActive(_disableButtons1);
        _choiceButton2UGUI.SetActive(_disableButtons2);
    }


    void NarrativeTimer()
    {
        if (timer >= sentenceRate)
        {

            timer = 0;

            if (_narrativeText.text[index] != '.')
                textObject.GetComponent<TextMeshProUGUI>().text += _narrativeText.text[index];
            else
            {
                textObject.GetComponent<TextMeshProUGUI>().text = "";
                timer = -sentenceDelay;
            }

            index++;
        }

        timer += Time.deltaTime;
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
                }

                break;

            case 1:
                switch (playerchoice)
                {
                    case 1:
                        _gameState = 2;
                        Debug.Log("Typed 1 on Keyboard");
                        Debug.Log("Clicked button 1");

                        break;
                }
                break;

            case 2:
                switch (playerchoice)
                {
                    case 1:
                        _gameState = 3;
                        Debug.Log("Typed 1 on Keyboard");
                        Debug.Log("Clicked button 1");

                        break;
                }

                break;

            case 3:
                switch (playerchoice)
                {
                    case 1:
                        _gameState = 4;
                        Debug.Log("Typed 1 on Keyboard");
                        Debug.Log("Clicked button 1");

                        break;
                }
                break;

            case 4:
                switch (playerchoice)
                {
                    case 1:
                        _gameState = 5;
                        Debug.Log("Typed 1 on Keyboard");
                        Debug.Log("Clicked button 1");

                        break;
                }

                break;

            case 5:
                switch (playerchoice)
                {
                    case 1:
                        _gameState = 6;
                        Debug.Log("Typed 1 on Keyboard");
                        Debug.Log("Clicked button 1");

                        break;
                    case 2:
                        _gameState = 8;
                        Debug.Log("Typed 2 on Keyboard");
                        Debug.Log("Clicked button 2");

                        break;
                }

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
