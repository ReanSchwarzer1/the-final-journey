using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullState : MonoBehaviour
{
    private bool isDamaged = false;
    private int trustScore = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public bool IsDamaged()
    {
        return isDamaged;
    }

    public void SetDamage()
    {
        isDamaged = true;
    }
    public void IncreaseTrust()
    {
        trustScore++;
        Debug.Log("Trust is now " + trustScore);
    }

    public int CheckTrust()
    {
        return trustScore;
    }
}
