using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleShip : MonoBehaviour
{
    // Fields for ship movement

    /// <summary>
    /// Ship position
    /// </summary>
    private Vector2 position;

    /// <summary>
    /// Height of sin curve movement
    /// </summary>
    [SerializeField] private float amplitude;

    /// <summary>
    /// Speed multipler for sin curve movement
    /// </summary>
    [SerializeField] private float period;

    // Floats for a changing x value to make the sin curve move
    private float xVariant = 0.0f;
    private bool decreasing = true;

    // Start is called before the first frame update
    void Start()
    {
        // Setting to position zero
        position = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Setting the y value of the position to the sin eqaution and updating the position
        position.y = amplitude * Mathf.Sin(period * xVariant);
        transform.position = position;

        // Checks for if the ship should be moving up or down
        if (decreasing)
        {
            xVariant -= Time.deltaTime;
        }
        else
        {
            xVariant += Time.deltaTime;
        }

        if (xVariant <= -1.0f)
        {
            decreasing = false;
        }
        if (xVariant >= 1.0f)
        {
            decreasing = true;
        }
    }
}
