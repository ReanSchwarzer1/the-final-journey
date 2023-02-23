using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NarrativeDelivery : MonoBehaviour
{
    public string NarrativeText;
    public GameObject textObject;
    public float sentenceRate;
    public float sentenceDelay;
    private float timer;
    private int index = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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

    }
}
