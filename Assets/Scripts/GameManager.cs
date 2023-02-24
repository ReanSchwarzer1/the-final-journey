using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class StoryBlock 
{
    public string _narrativeText;
    public string _choice1Text;
    public int _choice1States;

    public string _choice2Text;
    public int _choice2States;
    public bool _button2EnableBool;

    public StoryBlock(string _narrative, string _choice1 = "", string _choice2 = "",
        int _choice1StateID = -1, int _choice2StateID = -1, bool _choice2ButtonEnabled = true)
    {

        this._narrativeText = _narrative;
        this._choice1Text = _choice1;
        this._choice2Text = _choice2;
        this._choice1States = _choice1StateID;
        this._choice2States = _choice2StateID;
        this._button2EnableBool = _choice2ButtonEnabled;
    }
}

public class GameManager : MonoBehaviour 
{
    /*Reference: https://youtu.be/xmR07iBW7zk */

    public TextMeshProUGUI _narrativeTextObject;
    public Button _choice1Object;
    public Button _choice2Object;


    public float _narrationSpeed = 0.1f;

    public StoryBlock[] _narrativeBlocks = {
    new StoryBlock("Activating companion protocol...", "Continue", "", 1, -1, false), // tldr the bool at the end is for button 2 (whether it should be disabled or not)
    new StoryBlock("Life support at 89% capacity...", "Continue", "", 2, -1, false), // the numbers represent the new states the game should go to when the player clicks the button
    new StoryBlock("In-Cryo communication online...", "Continue", "", 3, -1, false), // for eg here, this is state 2 (from 0 to 1 to 2), and clicking button 1 would lead to the next state (state 3)
    new StoryBlock("Yes? Yes, but I cannot see you.","Hello Q, can you hear me?.","Continue", 4, -1, true), // in this case button 2 is enabled
    new StoryBlock("That’s alright, can you tell me how you are feeling?", "I feel a little strange, not quite here.", "Continue", 5, -1, true),
    new StoryBlock("I assure you, you are fine. You are currently in cryogenic sleep. Here are your vitals:", "I see, who are you?", "Continue", -6, -1, true),
    };

    StoryBlock currentBlock;

    void Start()
    {
        StartCoroutine(NarrativeWriter(_narrativeBlocks[0]._narrativeText));
        DisplayBlock(_narrativeBlocks[0]);

        if (currentBlock._choice2States < 3) // for the first 3 states, we do not need a button 2
        {
            _choice2Object.interactable = false;
        }

        if (currentBlock._choice2States >= 3) // for the first 3 states, we do not need a button 2
        {
            _choice2Object.interactable = true;
        }
    }

    void DisplayBlock(StoryBlock _state)
    {
        StopAllCoroutines();
        StartCoroutine(NarrativeWriter(_state._narrativeText));
       // _narrativeTextObject.text = _state._narrativeText;
        _choice1Object.GetComponentInChildren<TextMeshProUGUI>().text = _state._choice1Text;
        _choice2Object.GetComponentInChildren<TextMeshProUGUI>().text = _state._choice2Text;
        currentBlock = _state;
    }

    public void Button1Clicked()
    {
        DisplayBlock(_narrativeBlocks[currentBlock._choice1States]);

        if (currentBlock._choice2States < 3)
        {
            _choice2Object.interactable = false;
        }

        if (currentBlock._choice2States >= 3)
        {
            _choice2Object.interactable = true;
        }
    }

    public void Button2Clicked()
    {
        DisplayBlock(_narrativeBlocks[currentBlock._choice2States]);

        if (currentBlock._choice2States < 3)
        {
            _choice2Object.interactable = false;
        }

        if (currentBlock._choice2States >= 3)
        {
            _choice2Object.interactable = true;
        }
    }

    IEnumerator NarrativeWriter (string _mainNarrative)
    {
        _narrativeTextObject.text = "";
        foreach (char i in _mainNarrative.ToCharArray())
        {
            _narrativeTextObject.text += i;
            yield return new WaitForSeconds(_narrationSpeed);
        }
    }



}
