using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameTracker : MonoBehaviour
{
    [SerializeField] private List<string> playerChoices = new List<string>();
    [SerializeField] private GameObject choiceData;

    private bool hasRun = false;

    public List<string> PlayerChoices
    {
        get { return playerChoices; }
        set { playerChoices = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name == "ChoiceScene")
        {
            if (!hasRun)
            {
                choiceData = GameObject.Find("ChoiceData");
                choiceData.GetComponent<TMP_Text>().text = "Choices you made:\n";
                for (int i = 0; i < playerChoices.Count; i++)
                {
                    
                    choiceData.GetComponent<TMP_Text>().text += playerChoices[i] + "\n\n";

                    if (i == playerChoices.Count - 1)
                    {
                        hasRun = true;
                    }
                }
            }
            
        }
    }
}
