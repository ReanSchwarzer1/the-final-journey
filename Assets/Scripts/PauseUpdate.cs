using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseUpdate : MonoBehaviour
{
    public GameObject _pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _pauseMenu.SetActive(!_pauseMenu.activeSelf);
            Time.timeScale = (Time.timeScale > 0.0f ? 0.0f : 1.0f);
        }
    }
}
