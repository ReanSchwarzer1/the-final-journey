using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event : MonoBehaviour
{
    // Fields used for displaying a warning message
    [SerializeField] private GameObject warningPrefab;
    protected bool isWarningPlaying = true;
	private GameObject warning;
	private Color warningColor;
	private float percentDone = 0.001f;
	private bool isFadingIn = true;
	private int numTimesFaded = 0;

	// Start is called before the first frame update
	protected virtual void Start()
    {
        // Initialize things needed for a warning
        warning = Instantiate(warningPrefab);
        warningColor = warning.GetComponent<SpriteRenderer>().color;

		StartCoroutine(DisplayWarning());
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Run the warning only when needed
        if (isWarningPlaying)
        {
            StartCoroutine(DisplayWarning());
        }
    }

    // Fade the warning in and out 3 times
    private IEnumerator DisplayWarning()
    {
		// Lerp the alpha of the warning
		warningColor.a = Mathf.Lerp(-0.25f, 1.25f, percentDone);
		warning.GetComponent<SpriteRenderer>().color = warningColor;
		percentDone += isFadingIn ? 0.001f : -0.001f;

        // Toggle between fading in and out
        if(percentDone <= 0f)
        {
            isFadingIn = true;
            numTimesFaded++;
        }
        else if(percentDone >= 1f)
        {
            isFadingIn = false;
        }

        // We are done warning the player after 3 times
        if (numTimesFaded == 3)
        {
            isWarningPlaying = false;
            Destroy(warning);
        }

        yield return null;
    }
}
