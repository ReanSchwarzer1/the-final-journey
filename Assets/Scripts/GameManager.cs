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

    public TextMeshProUGUI mainText;
    public Button option1;
    public Button option2;

    public StoryBlock[] storyBlocks = {
    new StoryBlock("Activating companion protocol.../", "Continue", "", 1, -1, false),
    new StoryBlock("Life support at 89% capacity.../", "Continue", "", 2, -1, false),
    new StoryBlock("In-Cryo communication online.../", "Continue", "", 3, -1, false),
    new StoryBlock("Hello Q, can you hear me?.", "Yes? Yes, but I cannot see you.","Continue", 4, -1, true),
    new StoryBlock("That’s alright, can you tell me how you are feeling?", "I feel a little strange, not quite here.", "Continue", 5, -1, true),
    new StoryBlock("I assure you, you are fine. You are currently in cryogenic sleep. Here are your vitals:", "I see, who are you?", "Continue", -6, -1, true),
    };

    StoryBlock currentBlock;

    void Start()
    {
        DisplayBlock(storyBlocks[0]);

        if (currentBlock._choice2States < 3)
        {
            option2.interactable = false;
        }

        if (currentBlock._choice2States >= 3)
        {
            option2.interactable = true;
        }
    }

    void DisplayBlock(StoryBlock block)
    {
        mainText.text = block._narrativeText;
        option1.GetComponentInChildren<TextMeshProUGUI>().text = block._choice1Text;
        option2.GetComponentInChildren<TextMeshProUGUI>().text = block._choice2Text;
        currentBlock = block;
    }

    public void Button1Clicked()
    {
        DisplayBlock(storyBlocks[currentBlock._choice1States]);

        if (currentBlock._choice2States < 3)
        {
            option2.interactable = false;
        }

        if (currentBlock._choice2States >= 3)
        {
            option2.interactable = true;
        }
    }

    public void Button2Clicked()
    {
        DisplayBlock(storyBlocks[currentBlock._choice2States]);

        if (currentBlock._choice2States < 3)
        {
            option2.interactable = false;
        }

        if (currentBlock._choice2States >= 3)
        {
            option2.interactable = true;
        }
    }

}
