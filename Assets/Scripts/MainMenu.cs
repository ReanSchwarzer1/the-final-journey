using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private AudioSource audioSource;
    [SerializeField] private AudioSource audioSource2;

    [SerializeField] private AudioClip _buttonSFX;

    private void Start()
    {
        audioSource = this.gameObject.GetComponent<AudioSource>();
    }

    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        audioSource2.clip = _buttonSFX;
        audioSource2.Play();
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();

        audioSource2.clip = _buttonSFX;
        audioSource2.Play();
    }

}
