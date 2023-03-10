using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameTracker : MonoBehaviour
{
    [SerializeField] private List<string> playerChoices = new List<string>();

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
        
    }
}
