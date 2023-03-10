using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HullState : MonoBehaviour
{
    private bool isDamaged = false;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public bool IsDamaged()
    {
        return isDamaged;
    }

    public void setDamage()
    {
        isDamaged = true;
    }
    
}
