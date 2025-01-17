﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public int actNumber;
    public GameObject warningUI;
    public GameObject pingUI;
    public bool devmode;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;

    [SerializeField] private AudioClip _buttonSFX;

    public float _BGSpeed;
    private float offs;
    [SerializeField] private Material _bgMat;

    [SerializeField] private Image _humanSprite;
    [SerializeField] private Image _aiSprite;
    public float _alphaOpacity = 1f;

    public float _narrationSpeed = 0.1f;

    private bool narrationCheck = false;
    private int trustState = 0;
    private bool hullDamaged = false;
    private string endingText;
    private GameObject hullDamage;
    [SerializeField] private GameObject _pauseMenu;

    public GameObject blackScreen;
    public GameObject endingDataText;
    public GameObject choiceDataText;
    public GameObject choiceButton;
    public GameObject loreText;

    public GameObject[] endings;
    public StoryBlock[] _narrativeBlocks = {
    new StoryBlock("Activating companion protocol...", "Continue", "", 1, -1, false), // tldr the bool at the end is for button 2 (whether it should be disabled or not)
    new StoryBlock("Life support at 89% capacity...", "Continue", "", 2, -1, false), // the numbers represent the new states the game should go to when the player clicks the button
    new StoryBlock("In-Cryo communication online...", "Continue", "", 3, -1, false), // for eg here, this is state 2 (from 0 to 1 to 2), and clicking button 1 would lead to the next state (state 3)
    new StoryBlock("Yes? Yes, but I cannot see you.","Hello Q, can you hear me?.","Continue", 4, -1, true), // in this case button 2 is enabled
    new StoryBlock("That’s alright, can you tell me how you are feeling?", "I feel a little strange, not quite here.", "Continue", 5, -1, true),
    new StoryBlock("I assure you, you are fine. You are currently in cryogenic sleep. Here are your vitals:", "I see, who are you?", "Continue", -6, -1, true),
    };

    StoryBlock currentBlock;

    [SerializeField] private InGameTracker choiceTracker;

    void Start()
    {
        choiceTracker = GameObject.Find("ChoiceTracker").GetComponent<InGameTracker>();
        hullDamage = GameObject.Find("DamageTracker");
        StartCoroutine(NarrativeWriter(_narrativeBlocks[0]));

        audioSource = this.gameObject.GetComponent<AudioSource>();

        if(devmode)
        { _narrationSpeed = 0; }
        if (actNumber == 2)
        {
            hullDamaged = GameObject.Find("DamageTracker").GetComponent<HullState>().IsDamaged();
            if (hullDamaged)
                _narrativeBlocks[0]._choice1States = 2;
            else
                _narrativeBlocks[0]._choice1States = 1;
        }
        else if(actNumber == 3)
        {
            if (hullDamage.GetComponent<HullState>().CheckTrust() > 2)
                _narrativeBlocks[3]._choice1States = 4;
            else
                _narrativeBlocks[3]._choice1States = 6;
        }
        DisplayBlock(_narrativeBlocks[0]);

        // for the first 3 states, we do not need a button 2
        switch (currentBlock._choice2States)
        {
            case < 3:
                _choice2Object.interactable = false;
                break;

            case >= 3:
                _choice2Object.interactable = true;
                break;
        }
    }

    private void Update()
    {
        GamePauser();
    }

    public void GamePauser()
    {
        // Reference to this pause code (603 game 2)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            Time.timeScale = (Time.timeScale > 0.0f ? 0.0f : 1.0f);
        }
    }

    public void ButtonSound()
    {
        audioSource2.clip = _buttonSFX;
        audioSource2.Play();
    }

    public void BGLooper()
    {
        float timer = Time.deltaTime;
        offs += (timer * _BGSpeed) / 10f;
        _bgMat.SetTextureOffset("_MainTex", new Vector2(offs, 0));
    }

    void DisplayBlock(StoryBlock _state)
    {
        StopAllCoroutines();
        //SpriteAlphaChanger();
        StartCoroutine(NarrativeWriter(_state));
       // _narrativeTextObject.text = _state._narrativeText;
        _choice1Object.GetComponentInChildren<TextMeshProUGUI>().text = _state._choice1Text;
        _choice2Object.GetComponentInChildren<TextMeshProUGUI>().text = _state._choice2Text;
        currentBlock = _state;


        if (actNumber == 1)
        {
            if (currentBlock == _narrativeBlocks[13])
                hullDamage.GetComponent<HullState>().IncreaseTrust();
            switch (_state._choice1States)
            {

                

                case 16:
                    warningUI.SetActive(true);
                    break;
                case 17:
                    warningUI.SetActive(false);
                    SceneManager.LoadScene("John Scene");
                    break;
            }
        }
        else if(actNumber == 2)
        {
            if (currentBlock == _narrativeBlocks[3])
                hullDamage.GetComponent<HullState>().IncreaseTrust();
            else if(currentBlock == _narrativeBlocks[4])
            {
                //HULL REPAIR EVENT
            }
            switch (_state._choice1States)
            {



                case 8:
                    pingUI.SetActive(true);
                    break;
                case 9:
                    pingUI.SetActive(false);
                    break;
                case 16:
                    hullDamage.GetComponent<HullState>().IncreaseTrust();
                    break;

                case 24:
                    SceneManager.LoadScene("Mike Scene");
                    break;
            }
        }
        else
        {

            switch (_state._choice1States)
            {



                
                
                case 18:
                    {
                        TriggerEnding(1);
                        Debug.Log("Ending 1 triggered");
                    }
                    break;
                case 19:
                    TriggerEnding(2);
                    break;
                case 500:
                    TriggerEnding(3);
                    break;
            }
        }

        switch (_state._choice2States)
        {
            case 1:
                break;
        }

    }

    /*
    void SpriteAlphaChanger()
    {

    }

    
       if (timer >= sentenceRate)
       {
           timer = 0;
           if (NarrativeText[index] != '/')
               textObject.GetComponent<TextMeshProUGUI>().text += NarrativeText[index];
           else
           {
               textObject.GetComponent<TextMeshProUGUI>().text = "";
               timer = -sentenceDelay;
           }
           index++;
       }
       timer += Time.deltaTime;
    */

    public void Button1Clicked()
    {
        if (currentBlock._choice2States != -1)
        {
            choiceTracker.PlayerChoices.Add(currentBlock._choice1Text);
        }

        DisplayBlock(_narrativeBlocks[currentBlock._choice1States]);
        ButtonSound();

        // for the first 3 states, we do not need a button 2
        switch (currentBlock._choice2States)
        {
            case < 3:
                _choice2Object.interactable = false;
                break;

            case >= 3:
                _choice2Object.interactable = true;
                break;
        }
    }

    public void Button2Clicked()
    {
        if (currentBlock._choice2States != -1)
        {
            choiceTracker.PlayerChoices.Add(currentBlock._choice2Text);
        }

        DisplayBlock(_narrativeBlocks[currentBlock._choice2States]);
        ButtonSound();

        // for the first 3 states, we do not need a button 2
        switch (currentBlock._choice2States)
        {
            case < 3:
                _choice2Object.interactable = false;
                break;

            case >= 3:
                _choice2Object.interactable = true;
                break;
        }
    }

    IEnumerator NarrativeWriter (StoryBlock block)
    {
        _narrativeTextObject.text = "";
        string _mainNarrative = block._narrativeText;
        Debug.Log("Choice 1 is " + block._choice1Text);
        _choice1Object.interactable = false;
        _choice2Object.interactable = false;
        if (!narrationCheck)
        {
            _choice1Object.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localPosition += new Vector3(50000, 0, 0);
            _choice2Object.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localPosition += new Vector3(50000, 0, 0);
        }
            narrationCheck = true;
        if (block._choice1Text == "Continue")
        {
            
            
            _aiSprite.GetComponent<Animator>().SetBool("Speaking", true);
            _humanSprite.GetComponent<Animator>().SetBool("Speaking", false);
        }
        else
        {
            _aiSprite.GetComponent<Animator>().SetBool("Speaking", false);
            _humanSprite.GetComponent<Animator>().SetBool("Speaking", true);

            
        }
        if (block._choice2Text == "Continue")
        {
            _choice2Object.gameObject.SetActive(false);
        }
        else
        {
            _choice2Object.gameObject.SetActive(true);
        }
        foreach (char i in _mainNarrative.ToCharArray())
        {
            
            _narrativeTextObject.text += i;
            yield return new WaitForSeconds(_narrationSpeed);
        }
        _aiSprite.GetComponent<Animator>().SetBool("Speaking", true);
        _humanSprite.GetComponent<Animator>().SetBool("Speaking", false);
        _choice1Object.interactable = true;
        if(currentBlock._choice2States>=3)
        _choice2Object.interactable = true;
        _choice1Object.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localPosition -= new Vector3(50000, 0, 0);
        _choice2Object.transform.GetChild(0).gameObject.GetComponent<RectTransform>().localPosition -= new Vector3(50000, 0, 0);
        narrationCheck = false;
    }

    public void TriggerEnding(int ending)
    {
        //SHOW DATA WITH A DELAY AFTER THIS
        blackScreen.SetActive(true);
        switch(ending)
        {
            case 1:
                endings[0].SetActive(true);
                break;
            case 2:
                endings[1].SetActive(true);
                break;
            case 3:
                endings[2].SetActive(true);
                break;
        }

        // Load data
        StartCoroutine(gameObject.GetComponent<DataTracking>().LoadData(ending));
    }

    public void UpdateUIAndData(int ending)
    {
        DataTracking dataTracking = gameObject.GetComponent<DataTracking>();

        // Parse data
        string[] dataString = dataTracking.data.Split(',');
        int[] data = new int[dataString.Length];
        float totalPlays = 0f;
        for (int i = 0; i < dataString.Length; i++)
        {
            data[i] = int.Parse(dataString[i]);
            totalPlays += data[i];
        }

        // Update data and UI
        data[ending - 1]++;
        totalPlays++;
        float percent = 100f * data[ending - 1] / totalPlays;
        endingDataText.GetComponent<TMP_Text>().text = percent.ToString() + "% of players arrived at this ending.";
        endingDataText.SetActive(true);

        // Save data
        dataString[ending - 1] = data[ending - 1].ToString();
        string newData = "";
        for (int i = 0; i < dataString.Length; i++)
        {
            newData += dataString[i] + ",";
        }
        newData = newData.Substring(0, newData.Length - 1);

        choiceButton.SetActive(true);

		StartCoroutine(dataTracking.SaveData(newData));
	}

    public void DisplayChoices()
    {
        SceneManager.LoadScene("ChoiceScene");
    }
}
