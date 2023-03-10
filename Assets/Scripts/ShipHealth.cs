using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShipHealth : MonoBehaviour
{
    [SerializeField] private Image[] healthUI;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    public GameObject damageTracker;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
        currentHealth--;
        damageTracker.GetComponent<HullState>().setDamage();
        if (currentHealth >= 0) healthUI[currentHealth].gameObject.SetActive(false);

        if(currentHealth <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
	}
}
