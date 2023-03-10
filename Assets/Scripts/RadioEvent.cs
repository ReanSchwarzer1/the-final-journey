using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class RadioEvent : MonoBehaviour
{
    // All the different game objects in the dictionary
    [SerializeField] GameObject baseOutline;
    [SerializeField] GameObject baseGO;
    [SerializeField] GameObject antennaOutline;
    [SerializeField] GameObject antennaGO;
    [SerializeField] GameObject batteryOutline;
    [SerializeField] GameObject batteryGO;
    [SerializeField] GameObject knobOutline;
    [SerializeField] GameObject knobGO;

    /// <summary>
    /// Dictionary that holds the objects being compared and if they've been aligned
    /// </summary>
    Dictionary<GameObject, (GameObject, bool)> outlineCheck;

    private int offset = 50;
    private int count = 0;

    // Start is called before the first frame update
    void Start()
    {
        // Setting the initial state of the dictionary with the game objects above
        outlineCheck = new Dictionary<GameObject, (GameObject, bool)>
        {
            { batteryGO, (batteryOutline, false) },
            { baseGO, (baseOutline, false) },
            { antennaGO, (antennaOutline, false) },
            { knobGO, (knobOutline, false) }
        };
    }

    // Update is called once per frame
    void Update()
    {
        // Looping through each key in the dictionary and comparing its location to the location of the gameobject item in the value
        foreach (GameObject key in outlineCheck.Keys)
        {
            if (key.transform.position.x > outlineCheck[key].Item1.transform.position.x - offset &&
            key.transform.position.x < outlineCheck[key].Item1.transform.position.x + offset &&
            key.transform.position.y > outlineCheck[key].Item1.transform.position.y - offset &&
            key.transform.position.y < outlineCheck[key].Item1.transform.position.y + offset)
            {
                if (!outlineCheck[key].Item2)
                {
                    // Setting the key and game object value positions and scales equal
                    key.transform.position = outlineCheck[key].Item1.transform.position;
                    key.transform.localScale = outlineCheck[key].Item1.transform.localScale;
                    Destroy(key.GetComponent<Draggable>());
                    count++;
                    outlineCheck[key] = (outlineCheck[key].Item1, true);
                    break;
                }
            }
        }

        if (count == 4)
        {
            SceneManager.LoadScene("MainScene Act 3");
        }
    }
}
